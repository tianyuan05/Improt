using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jly.Utility.Models
{
    public class OparkAuth : BindableBase
    {
        private long? id;
        private string name;
        private bool? granted;

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
        /// 是否具有此权限
        /// </summary>
        [JsonProperty("granted")]
        public bool? Granted
        {
            get { return granted; }
            set { SetProperty(ref granted, value); }
        }
    }
}
