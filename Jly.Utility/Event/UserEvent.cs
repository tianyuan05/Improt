using Jly.Utility.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jly.Utility.Event
{
    /// <summary>
    /// 登陆事件
    /// </summary>
    public class UserEvent : PubSubEvent<User>
    {
    }
}
