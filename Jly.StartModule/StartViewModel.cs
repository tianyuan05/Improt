using Jly.Utility.Event;
using Jly.Utility.Extension;
using Jly.Utility.Http;
using Jly.Utility.Models;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Jly.Start
{
    public class StartViewModel : BindableBase
    {

        #region 字段

        private string searchKey; //query condition key 

        private List<MemberSimpleInfo> simpleMembers = new List<MemberSimpleInfo>();

        private MemberDetailInfo detailMember = new MemberDetailInfo();

        private ICollectionView memberCollection;

        #endregion

        #region 属性

        public string SearchKey { get { return searchKey; } set { SetProperty(ref searchKey, value); } }

        public List<MemberSimpleInfo> SimpleMembers { get { return simpleMembers; } set { SetProperty(ref simpleMembers, value); } }

        public MemberDetailInfo DetailMember { get { return detailMember; } set { SetProperty(ref detailMember, value); } }

        public ICollectionView MemberCollection { get => memberCollection; set => SetProperty(ref memberCollection, value); }


        #endregion

        #region 构造器

        public StartViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<OparkChangedEvent>().Subscribe(OnOparkChanged);
            QueryCommand = new DelegateCommand(OnQuery);
        }

        #endregion

        #region DelegateCommand

        public DelegateCommand QueryCommand { get; private set; }

        #endregion

        #region private method

        async void OnQuery()
        {
            await QueryMmeberDetailAsync();
        }


        async Task InitSimpleMembersAsync()
        {
            try
            {
                var data = await MemberHttp.GetMemberSimpleListAsync();
                if (data != null)
                {
                    SimpleMembers = data;
                    MemberCollection = CollectionViewSource.GetDefaultView(SimpleMembers);
                }
            }
            catch (Exception exc)
            {
                System.Windows.MessageBox.Show(exc.Message);
            }
        }


        async Task QueryMmeberDetailAsync()
        {
            if (ConditionIsNull())
                return;

            try
            {
                var data = await MemberHttp.GetMemberDetailAsync(SearchKey.Trim());
                if (data != null)
                    DetailMember = data;
            }
            catch (Exception exc)
            {
                System.Windows.MessageBox.Show(exc.Message);
            }
        }

        async void OnOparkChanged(Opark opark)
        {
            if (opark != null)
            {
                SearchKey = string.Empty;
                await InitSimpleMembersAsync();
                DetailMember = new MemberDetailInfo();

                //TODO:会员套餐列表清空

            }
        }

        /// <summary>
        /// 检测是否为空
        /// </summary>
        /// <returns>true-null</returns>
        bool ConditionIsNull()
        {
            if (SearchKey.IsNullOrWhiteSpace())
            {
                System.Windows.MessageBox.Show("搜索条件不能为空");
                return true;
            }
            else
                return false;
        }


        #endregion

        #region event

        public async void OnLoad(object sender, System.Windows.RoutedEventArgs e)
        {
            await InitSimpleMembersAsync();
        }

        #endregion

    }
}
