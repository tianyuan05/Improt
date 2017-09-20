using Jly.MemberImprot.Event;
using Jly.Utility.Core;
using Jly.Utility.Http;
using Jly.Utility.Models;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Jly.MemberImprot
{
    public class LoginViewModel : BindableBase
    {
        #region 字段

        Setting setting;


        SecureString password;

        private string account;

        // 登录过的用户
        private IEnumerable<string> users;

        #endregion

        #region 属性

        [Dependency]
        public IEventAggregator EventAggregator { get; set; }

        public IEnumerable<string> Users
        {
            get { return users; }
            set { SetProperty(ref users, value); }
        }
        public string Account
        {
            get { return account; }
            set { SetProperty(ref account, value); }
        }

        public SecureString Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        #endregion

        public LoginViewModel()
        {
            LoginCommand = new DelegateCommand(OnLogin);
            CancelCommand = new DelegateCommand(OnCancel);

            ReadSetting();
        }


        #region DelegateComamnd

        public DelegateCommand LoginCommand { get; private set; }

        public DelegateCommand CancelCommand { get; private set; }

        #endregion

        #region private method

        void ReadSetting()
        {
            setting = Setting.Load();

            if (setting.Managers != null)
                Users = setting.Managers;

            if (setting.ManagerLastTime != null)
                Account = setting.ManagerLastTime;
        }

        void SaveSetting()
        {
            setting.ManagerLastTime = Account;
            setting.OparkId = Session.OparkId;
            if (!setting.Managers.Contains(Account))
                setting.Managers.Add(Account);
            setting.Save();
        }

        /// <summary>
        /// login
        /// </summary>
        async void OnLogin()
        {
            IntPtr passwordBSTR = default(IntPtr);
            string insecurePassword = "";

            try
            {
                passwordBSTR = Marshal.SecureStringToBSTR(password);
                insecurePassword = Marshal.PtrToStringBSTR(passwordBSTR);
            }
            catch
            {
                insecurePassword = "";
            }


            try
            {
                User user = await LoginHttp.LoginAsync(Account, /*insecurePassword*/"123456");

                SaveSetting(); //保存配置信息

                LoginView login = Application.Current.MainWindow as LoginView;

                EventAggregator.GetEvent<UserEvent>().Publish(user);
                login.DialogResult = true;
                login.Close();

            }
            catch (Exception exc)
            {
                Console.WriteLine(JsonConvert.SerializeObject(exc));
            }

        }

        void OnCancel()
        {
            LoginView login = Application.Current.MainWindow as LoginView;
            login.DialogResult = false;
            login.Close();

        }

        #endregion
    }
}
