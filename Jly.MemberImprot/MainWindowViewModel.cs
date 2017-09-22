using Jly.Utility.Event;
using Jly.Utility.Models;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jly.MemberImprot
{
    public class MainWindowViewModel : BindableBase
    {
        #region 字段

        private User currentUser = new User();

        private int selectOparkIndex;

        #endregion

        #region 属性

        private IEventAggregator _eventAggregator { get; set; }

        /// <summary>
        /// 当前登陆账户
        /// </summary>
        public User CurrentUser { get { return currentUser; } set { SetProperty(ref currentUser, value); } }

        /// <summary>
        /// 当前选择的乐园
        /// </summary>
        public int SelectOparkIndex { get { return selectOparkIndex; } set { SetProperty(ref selectOparkIndex, value); } }

        #endregion

        #region 构造器

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<UserEvent>().Subscribe(GetUserInfo);
        }

        #endregion

        #region private method

        void GetUserInfo(User user)
        {
            if (user != null)
            {
                CurrentUser = user;
                Session.Opark = CurrentUser.UserInfo.CurrentOpark;
                Session.User = CurrentUser.UserInfo;
                SelectOparkIndex = CurrentUser.UserInfo.Oparks.FindIndex(x => x.Id == CurrentUser.UserInfo.CurrentOpark.Id);
            }
        }

        #endregion


        #region 事件

        public void OparkSelectionChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (e.OriginalSource is System.Windows.Controls.ComboBox cmb)
            {
                if (cmb.SelectedItem is Opark opark)
                {
                    Session.Opark = opark;
                    _eventAggregator.GetEvent<OparkChangedEvent>().Publish(opark);
                }
            }
        }

        #endregion

    }
}
