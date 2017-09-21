using Jly.Utility.Util;
using Microsoft.Practices.Unity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Jly.MemberImprot
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        [Dependency]
        public IRegionManager RegionManager { get; set; }


        private void Start_Click(object sender, RoutedEventArgs e)
        {
            RegionManager.RequestNavigate(RegionNames.MainRegionContent, ViewNames.StartView);
        }

        private void Member_Click(object sender, RoutedEventArgs e)
        {
            RegionManager.RequestNavigate(RegionNames.MainRegionContent, ViewNames.MemberView);
        }
    }
}
