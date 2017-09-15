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
        SecureString password;

        public SecureString Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

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
            System.Windows.MessageBox.Show(insecurePassword);
        }

    }
}
