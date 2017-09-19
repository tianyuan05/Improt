using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jly.Utility.Models
{
    /// <summary>
    /// Rest API 返回数据格式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonResult<T>
    {
        /// <summary>
        /// 返回代码 0-正常值；其他-接口错误
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }

        /// <summary>
        /// Rest API 返回数据
        /// </summary>
        [JsonProperty("data")]
        public T Data { get; set; }

        /// <summary>
        /// 错误信息 Code!=0时有意义
        /// </summary>
        [JsonProperty("msg")]
        public string Message { get; set; }

    }
}
