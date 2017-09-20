using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Jly.MemberImprot
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            LoginView login = new LoginView();

            login.ShowDialog();

            base.OnStartup(e);

            if (login.ShowDialog().GetValueOrDefault())
            {
                MainWindow window = Application.Current.MainWindow as MainWindow;
                window.Show();
            }

        }
    }
}
