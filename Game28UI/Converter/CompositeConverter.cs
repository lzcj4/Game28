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
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
