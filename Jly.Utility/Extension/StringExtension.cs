using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jly.Utility.Extension
{
    public static class StringExtension
    {
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
