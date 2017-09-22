using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jly.Utility.Models
{
    public class UserInfo : BindableBase
    {
        private string iconUrl;

        /// <summary>
        /// 头像路径
        /// </summary>
        [JsonProperty("icon")]
        public string IconUrl
        {
            get { return iconUrl; }
            set { SetProperty(ref iconUrl, value); }
        }

        private string roleId;

        /// <summary>
        /// 用户角色Id
        /// </summary>
        [JsonProperty("opark_role_id")]
        public string RoleId { get { return roleId; } set { SetProperty(ref roleId, value); } }

        private string roleName;

        /// <summary>
        /// 用户角色名称
        /// </summary>
        [JsonProperty("opark_role_name")]
        public string RoleName { get { return roleName; } set { SetProperty(ref roleName, value); } }

        private string id;

        /// <summary>
        /// 编号
        /// </summary>
        [JsonProperty("id")]
        public string Id { get { return id; } set { SetProperty(ref id, value); } }


        private string nickName;

        /// <summary>
        /// 昵称
        /// </summary>
        [JsonProperty("nickname")]
        public string NickName { get { return nickName; } set { SetProperty(ref nickName, value); } }

        private string phone;

        /// <summary>
        /// 手机号(登录用户名)
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get { return phone; } set { SetProperty(ref phone, value); } }


        private string state;

        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty("state")]
        public string State { get { return state; } set { SetProperty(ref state, value); } }

        private List<Opark> oparks = new List<Opark>();


        /// <summary>
        /// 账号关联的乐园Id集合
        /// <para>乐园老板有可能会有多个Id,默认选取第一个登录，登录后可根据实际情况切换乐园</para>
        /// <para>乐园员工有且只有一个乐园Id</para>
        /// </summary>
        [JsonProperty("parks")]
        public List<Opark> Oparks { get { return oparks; } set { SetProperty(ref oparks, value); } }

        [JsonIgnore]
        public Opark CurrentOpark
        {
            get
            {
                if (Oparks?.Count > 0)
                    return Oparks.FirstOrDefault();
                else
                    return null;
            }
        }


    }
}
