using Jly.Utility.Core;
using Jly.Utility.Event;
using Jly.Utility.Extension;
using Jly.Utility.Http;
using Jly.Utility.Models;
using Jly.Utility.Util;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Jly.Start
{
    public class MemberViewModel : BindableBase
    {
        #region 字段

        private string name;

        private string phone;

        private string log = "错误日志";

        private List<Member> members = new List<Member>();

        private int pageIndex;
        private int pageSize;
        private int totalCount;

        private bool hasHeader;

        #endregion

        #region 属性

        public bool HasHeader { get => hasHeader; set => SetProperty(ref hasHeader, value); }

        public string Name { get { return name; } set { SetProperty(ref name, value); } }

        public string Phone { get => phone; set => SetProperty(ref phone, value); }

        public string Log { get => log; set => SetProperty(ref log, value); }

        public List<Member> Members { get => members; set => SetProperty(ref members, value); }

        public int PageIndex { get => pageIndex; set => SetProperty(ref pageIndex, value); }
        public int PageSize { get => pageSize; set => SetProperty(ref pageSize, value); }
        public int TotalCount { get => totalCount; set => SetProperty(ref totalCount, value); }


        #endregion


        #region 构造器

        public MemberViewModel(IEventAggregator aggregator)
        {
            aggregator.GetEvent<OparkChangedEvent>().Subscribe(OnOparkChanged);
            ImportCommand = new DelegateCommand(OnImport);
            QueryCommnad = new DelegateCommand(OnQuery);
            PageChangedCommand = new DelegateCommand(OnPageChanged);
            SaveLogCommand = new DelegateCommand(OnSaveLog);
            ExportCommand = new DelegateCommand(OnExport);
        }

        #endregion

        #region DelegateCommand

        public DelegateCommand ImportCommand { get; private set; }

        /// <summary>
        /// 导出
        /// </summary>
        public DelegateCommand ExportCommand { get; private set; }

        public DelegateCommand QueryCommnad { get; private set; }

        public DelegateCommand PageChangedCommand { get; private set; }

        public DelegateCommand SaveLogCommand { get; private set; }

        #endregion


        #region 私有方法

        async void OnQuery()
        {
            await GetMemberListAsync();
        }

        async void OnSaveLog()
        {
            string path = Functions.SetFilePath($"{Session.Opark.Name}_{DateTime.Now.ToString("yyyyMMddHHmmss")}");
            if (path.IsNullOrWhiteSpace()) return;

            Application.Current.MainWindow.IsEnabled = false;
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs, new UnicodeEncoding()))
                    {
                        await sw.WriteLineAsync(Log);
                    }
                }
            }
            catch (Exception exc)
            {
                System.Windows.MessageBox.Show(exc.Message);
            }
            finally
            {
                Application.Current.MainWindow.IsEnabled = true;
            }

        }

        void OnExport()
        {
            if (DisableInfo.Count <= 0)
            {
                MessageBox.Show("不存在异常数据");
                return;
            }

            try
            {
                string path = Functions.SetFilePath($"{Session.Opark.Name}_{DateTime.Now.ToString("yyyyMMddHHmmss")}");
                if (path.IsNullOrWhiteSpace()) return;

                ExcelHelper.Writer<OriginalMemberInfo>(DisableInfo,DisableInfo.Count, path);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }

        async void OnPageChanged()
        {
            await GetMemberListAsync(pageIndex, pageSize);
        }

        void OnImport()
        {
            string path = Functions.GetFilePath();
            if (path.IsNullOrWhiteSpace()) return;

            Application.Current.MainWindow.IsEnabled = false;
            try
            {
                List<OriginalMemberInfo> memberList = ExcelHelper.Reader<OriginalMemberInfo>(path, HasHeader);

                if (IsIegal(memberList))
                {
                    Application.Current.MainWindow.IsEnabled = true;
                    return; //不合法数据禁止保存入库
                }

                //TODO: 批量上传数据到服务端 (rest api)

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                Application.Current.MainWindow.IsEnabled = true;
            }
        }

        List<OriginalMemberInfo> DisableInfo { get; set; } = new List<OriginalMemberInfo>();

        /// <summary>
        /// 检测读取数据是否合法
        /// </summary>
        /// <param name="memberList"></param>
        /// <returns>true-合法，可用；false-不合法</returns>
        bool IsIegal(List<OriginalMemberInfo> memberList)
        {
            bool resut = false;

            foreach (var member in memberList)
            {
                if (member.Name.IsNullOrWhiteSpace())
                {
                    Log += $"\n姓名为空{JsonConvert.SerializeObject(member)}";
                    DisableInfo.Add(member);
                    resut = true;
                }
                if (member.Phone.IsNullOrWhiteSpace())
                {
                    Log += $"\n手机号为空：{JsonConvert.SerializeObject(member)}";
                    DisableInfo.Add(member);
                    resut = true;
                }
                if (member.Gender.IsNullOrWhiteSpace())
                {
                    Log += $"\n性别为空：{JsonConvert.SerializeObject(member)}";
                    DisableInfo.Add(member);
                    resut = true;
                }
                if (!member.CTime.IsConverterDateTime())
                {
                    Log += $"\n加入时间格式错误：{JsonConvert.SerializeObject(member)}";
                    DisableInfo.Add(member);
                    return true;
                }
                if (!member.DeadlineTime.IsConverterDateTime())
                {
                    Log += $"\n截止时间格式错误：{JsonConvert.SerializeObject(member)}";
                    DisableInfo.Add(member);
                    resut = true;
                }
            }

            return resut;
        }


        async Task GetMemberListAsync(int index = 1, int size = 15)
        {
            Application.Current.MainWindow.IsEnabled = false;
            try
            {
                var data = await MemberHttp.GetMemberAsync(name, phone, null, index, size);
                if (data != null)
                {
                    Members = data.Result;
                    PageIndex = data.PageNo;
                    PageSize = data.PageSize;
                    TotalCount = data.TotalCount;
                }
            }
            catch (Exception exc)
            {
                System.Windows.MessageBox.Show(exc.Message);
            }
            finally
            {
                Application.Current.MainWindow.IsEnabled = true;
            }
        }


        async void OnOparkChanged(Opark opark)
        {
            if (opark == null) return;
            //TODO:切换乐园 清空当前信息

            Phone = string.Empty;
            Name = string.Empty;
            Log = string.Empty;

            await GetMemberListAsync();

        }

        #endregion


        #region event

        public async void OnLoad(object sender, System.Windows.RoutedEventArgs e)
        {
            await GetMemberListAsync();
        }

        #endregion

    }
}
