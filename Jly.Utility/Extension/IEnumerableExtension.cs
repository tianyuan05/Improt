using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    public static class IEnumerableExtension
    {
        /// <summary>
        /// (值类型)集合转换为指定连接符连接的字符串
        /// </summary>
        /// <typeparam name="T">只限于值类型</typeparam>
        /// <param name="values">值类型集合</param>
        /// <param name="separator">连接字符</param>
        /// <returns></returns>
        public static string Join<T>(this IEnumerable<T> values, string separator) where T : struct
        {
            return String.Join(separator, values);
        }

        #region Sort

        /// <summary>
        /// 返回默认方式排序后的集合列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public static IEnumerable<T> Sort<T>(this IEnumerable<T> values)
        {
            values.ToList().Sort();
            return values;
        }

        public static IEnumerable<T> Sort<T>(this IEnumerable<T> values, IComparer<T> comparer)
        {
            values.ToList().Sort(comparer);
            return values;
        }

        public static IEnumerable<T> Sort<T>(this IEnumerable<T> values, Comparison<T> comparison)
        {
            values.ToList().Sort(comparison);
            return values;
        }

        #endregion

    }
}
