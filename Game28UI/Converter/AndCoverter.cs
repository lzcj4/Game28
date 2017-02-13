using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Game28UI.Converter
{
    public class AndConverter : List<IValueConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = true;
            foreach (var v in this)
            {
                result &= (bool)v.Convert(value, targetType, parameter, culture);
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
