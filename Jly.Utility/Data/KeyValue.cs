using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jly.Utility.Data
{
    public class KeyValue<T>
    {
        public T Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 性别（key-value）列表
        /// </summary>
        public static List<KeyValue<int?>> GenderList { get; set; } = new List<KeyValue<int?>>
        {
            new KeyValue<int?>{Id=null,Name="全部"},
            new KeyValue<int?>{Id=0,Name="未知"},
            new KeyValue<int?>{Id=1,Name="男"},
            new KeyValue<int?>{Id=2,Name="女"},
        };

        /// <summary>
        /// 套餐类型（Key-Value）列表
        /// </summary>
        public static List<KeyValue<int?>> CouponTypeList { get; set; } = new List<KeyValue<int?>>
        {
            new KeyValue<int?>{Id=null,Name="全部"},
            new KeyValue<int?>{Id=1,Name="单次卡"},
            new KeyValue<int?>{Id=2,Name="次卡"},
            new KeyValue<int?>{Id=3,Name="期限卡"},
            new KeyValue<int?>{Id=4,Name="电玩卡"},
            new KeyValue<int?>{Id=5,Name="体验卡"},
        };


    }
}
