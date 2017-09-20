using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using System.Windows;
using Prism.Modularity;
using Jly.Start;

namespace Jly.MemberImprot
{
    public class BootStrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        bool exit = false;

        protected override void InitializeShell()
        {
            //base.InitializeShell();
            LoginView login = Container.Resolve<LoginView>();
            Application.Current.MainWindow = login;
            if (login.ShowDialog().GetValueOrDefault())
            {
                exit = true;
            }
        }

        protected override void ConfigureModuleCatalog()
        {
            //base.ConfigureModuleCatalog();
            ModuleCatalog catalog = (ModuleCatalog)ModuleCatalog;
            catalog.AddModule(typeof(StartModule));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
        }

        protected override void InitializeModules()
        {
            if (!exit)
                Application.Current.Shutdown();
            else
            {
                base.InitializeModules();

                Application.Current.MainWindow = (Window)this.Shell;
                Application.Current.MainWindow.Show();
            }
        }

    }
}
