using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jly.Utility.Extension
{
    public static class StringExtension
    {
        /// <summary>
        /// 测试指定的字符串是 null、空还是仅由空白字符组成
        /// </summary>
        /// <param name="value">要测试的字符串</param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 能否转换成DateTime类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsConverterDateTime(this string value)
        {
            if (DateTime.TryParse(value, out DateTime dt))
                return true;
            else
                return false;
        }
    }
}
