using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jly.Utility.Data
{
    public static class Dictionarys
    {
        /// <summary>
        /// 性别
        /// </summary>
        public static Dictionary<object, string> GenderDic { get; set; } = new Dictionary<object, string>
        {
            { 0 ,"未知" },
            { 1 , "男" },
            { 2 , "女" }
        };

        /// <summary>
        /// 会员状态
        /// </summary>
        public static Dictionary<object, string> MemberStateDic { get; set; } = new Dictionary<object, string>
        {
            { 0 ,"正常"},
            { 1 ,"挂失"},
            { 2 ,"注销"},
        };

        public static Dictionary<object, string> CouponTypeDic { get; set; } = new Dictionary<object, string>
        {
            { 1 , "单次卡" },
            { 2 , "次卡" },
            { 3 , "期限卡" },
            { 4 , "电玩卡" },
            { 5 , "体验卡" },
        };


    }
}
