using Jly.Utility.Util;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jly.Start
{
    public class StartModule : IModule
    {
        [Dependency]
        public IRegionManager RegionManager { get; set; }

        [Dependency]
        public IUnityContainer Container { get; set; }

        public void Initialize()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.MainRegionContent, typeof(StartView));

            Container.RegisterType<object, MemberView>(ViewNames.MemberView);
            Container.RegisterType<object, StartView>(ViewNames.StartView);
        }
    }
}
