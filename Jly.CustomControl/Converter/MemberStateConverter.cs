using Jly.Utility.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Jly.CustomControl.Converter
{
    /// <summary>
    /// 会员状态转换
    /// </summary>
    public class MemberStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "-";
            if (Dictionarys.MemberStateDic.Keys.Contains(value))
                return Dictionarys.MemberStateDic[value];
            else
                return "-";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
