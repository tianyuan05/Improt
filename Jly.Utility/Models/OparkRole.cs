using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jly.Utility.Models
{
    public class OparkRole : BindableBase
    {
        private long? id;

        private string name;

        List<OparkAuth> authList;


        /// <summary>
        /// 主键
        /// </summary>
        [JsonProperty("id")]
        public long? Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        [JsonProperty("name")]
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        /// <summary>
        /// 权限列表
        /// </summary>
        [JsonProperty("authList")]
        public List<OparkAuth> AuthList
        {
            get { return authList; }
            set { SetProperty(ref authList, value); }
        }
    }
}
