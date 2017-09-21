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
        private User currentUser = new User();


        #region 属性

     
        /// <summary>
        /// 当前登陆账户
        /// </summary>
        public User CurrentUser { get { return currentUser; } set { SetProperty(ref currentUser, value); } }

        #endregion

        #region 构造器

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<UserEvent>().Subscribe(GetUserInfo);
        }

        #endregion

        #region private method

        void GetUserInfo(User user)
        {
            Console.WriteLine("UserEvent");
            if (user != null)
            {
                CurrentUser = user;
                Console.WriteLine(JsonConvert.SerializeObject(CurrentUser));
            }
        }

        #endregion


    }
}
