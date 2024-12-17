using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using FontAwesome6;

namespace UBIOCClass.Converters
{
    internal class WindowStateIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WindowState state = (WindowState)value;
            if (state == WindowState.Normal)
            {
                return EFontAwesomeIcon.Regular_Square;
            }
            else
            {
                return EFontAwesomeIcon.Solid_DownLeftAndUpRightToCenter;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
