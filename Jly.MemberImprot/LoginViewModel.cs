using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Jly.MemberImprot
{
    public class LoginViewModel : BindableBase
    {
        #region 字段

        SecureString password;

        private string account;

        // 登录过的用户
        private IEnumerable<string> users;

        #endregion

        #region 属性

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
        }

        public DelegateCommand LoginCommand { get; private set; }

        void OnLogin()
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

            }
            catch 
            {

                throw;
            }

        }

    }
}
