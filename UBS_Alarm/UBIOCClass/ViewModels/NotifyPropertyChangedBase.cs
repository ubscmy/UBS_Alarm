using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using UBIOCClass.Commands;
using UBIOCClass.Models;


namespace UBIOCClass.ViewModels
{
    public class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        public HistoryCommand HistoryDBCommand = new HistoryCommand();
        public RegisterCommand RegisterDBCommand = new RegisterCommand();
        public event PropertyChangedEventHandler PropertyChanged;
        public NotifyPropertyChangedBase()
        {

        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            NotifyPropertyChanged(propertyName);
            return true;
        }

    }

    //Convert: ViewModel의 값을 View에서 표시할 값으로 변환하는 메서드입니다.
    //ConvertBack: View에서 ViewModel로 값을 변환하는 메서드로, 이 메서드는 양방향 바인딩에서 필요합니다.
    class BkColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string colorCode = value.ToString();
            if (colorCode == null)
            {
                return Brushes.Transparent;
            }
            // ColorConverter를 사용하여 문자열 색상 코드를 Color로 변환
            Color color = (Color)ColorConverter.ConvertFromString(colorCode);
            // SolidBrush를 사용하여 Color를 Brush로 변환
            Brush brush = new SolidColorBrush(color);
            return brush;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
