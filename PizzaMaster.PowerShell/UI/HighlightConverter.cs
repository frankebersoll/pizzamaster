using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace PizzaMaster.PowerShell.UI
{
    public class HighlightConverter : IMultiValueConverter
    {
        public static readonly HighlightConverter Instance = new HighlightConverter();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values[1].ToString().Substring(((string) values[0]).Length);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}