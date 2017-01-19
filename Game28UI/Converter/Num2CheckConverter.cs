using System;
using System.Globalization;
using System.Windows.Data;

namespace Game28UI
{
    class Num2CheckConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value as string;
            int i = 0;
            bool b = int.TryParse(str, out i);
            return b && i > 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
