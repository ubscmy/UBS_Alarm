using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace UBIOCClass.Converters
{
    public class WindowStateMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WindowState state = (WindowState)value;
            if (state == WindowState.Normal)
            {
                return new Thickness(0);
            }
            else
            {
                var param = (string)parameter;
                var rigth = param == "Exit" ? 7 : 0;
                return new Thickness(0, 7, rigth, 0);
                //return new Thickness(0, 7, 0, 0);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
