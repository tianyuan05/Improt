using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jly.Utility.Models
{
    /// <summary>
    /// 乐园类
    /// </summary>
    public class Opark
    {
        /// <summary>
        /// 游乐园的地址
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// 游乐园的面积大小
        /// </summary>
        [JsonProperty("area")]
        public string Area { get; set; }

        /// <summary>
        /// 乐园Id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 乐园名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 是否合作乐园
        /// </summary>
        [JsonProperty("video_game_park")]
        public byte? VideoGamePark { get; set; }
    }
}
