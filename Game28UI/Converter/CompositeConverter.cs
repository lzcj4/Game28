﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Game28UI
{
    public class CompositeConverter : List<IValueConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object result = value;
            foreach (var v in this)
            {
                result = v.Convert(result, targetType, parameter, culture);
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
