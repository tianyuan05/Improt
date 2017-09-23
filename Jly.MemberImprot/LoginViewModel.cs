using Jly.Utility.Core;
using Jly.Utility.Event;
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
    /// <summary>
    /// the view-model of login view 
    /// </summary>
    public class LoginViewModel : BindableBase
    {
        #region fields

        Setting setting;


        SecureString password;

        private string account;

        // 登录过的用户
        private IEnumerable<string> users;

        #endregion

        #region property

        [Dependency]
        public IEventAggregator EventAggregator { get; set; }

        /// <summary>
        /// the list of account who been sigined in system
        /// </summary>
        public IEnumerable<string> Users
        {
            get { return users; }
            set { SetProperty(ref users, value); }
        }

        /// <summary>
        /// the name of current login account
        /// </summary>
        public string Account
        {
            get { return account; }
            set { SetProperty(ref account, value); }
        }

        /// <summary>
        /// the password of current login account
        /// </summary>
        public SecureString Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        #endregion

        #region constructor

        public LoginViewModel()
        {
            LoginCommand = new DelegateCommand(OnLogin);
            CancelCommand = new DelegateCommand(OnCancel);

            ReadSetting();
        }

        #endregion

        #region DelegateComamnd

        /// <summary>
        /// login command
        /// </summary>
        public DelegateCommand LoginCommand { get; private set; }

        /// <summary>
        /// cancel login command
        /// </summary>
        public DelegateCommand CancelCommand { get; private set; }

        #endregion

        #region private method

        /// <summary>
        /// get system setting info from local file
        /// </summary>
        void ReadSetting()
        {
            setting = Setting.Load();

            if (setting.Managers != null)
                Users = setting.Managers;

            if (setting.ManagerLastTime != null)
                Account = setting.ManagerLastTime;
        }

        /// <summary>
        /// save system setting info to local file
        /// </summary>
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

        /// <summary>
        /// cancel sigin in
        /// </summary>
        void OnCancel()
        {
            LoginView login = Application.Current.MainWindow as LoginView;
            login.DialogResult = false;
            login.Close();

        }

        #endregion
    }
}
