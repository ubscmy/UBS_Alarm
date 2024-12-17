using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
//using UBIOCClass.Defines;
//using UBIOCClass.ViewModels.MainViewTabs;

namespace UBIOCClass.Converters
{
    public class BoolStringConverter : IValueConverter
    {
        public object TrueValue { get; set; } = true;
        public object FalseValue { get; set; } = false;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue == true ? TrueValue : FalseValue;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class EqpOpForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color blueColor = (Color)ColorConverter.ConvertFromString("#4374D9");
            SolidColorBrush BlueColor = new SolidColorBrush(blueColor);

            Color blackColor = (Color)ColorConverter.ConvertFromString("#000000");
            SolidColorBrush BlackColor = new SolidColorBrush(blackColor);

            Color greenColor = (Color)ColorConverter.ConvertFromString("#77D970");
            SolidColorBrush GreenColor = new SolidColorBrush(greenColor);

            Color yellowColor = (Color)ColorConverter.ConvertFromString("#FFE400");
            SolidColorBrush YellowColor = new SolidColorBrush(yellowColor);

            // EQP Mode에 따라 폰트 색상 분리
            bool op = (bool)value;
            if (op == true) { return GreenColor; }
            if (op == false) { return YellowColor; }

            return blackColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class EQPModeFontColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color redColor = (Color)ColorConverter.ConvertFromString("#FF0075");
            SolidColorBrush RedColor = new SolidColorBrush(redColor);

            Color blueColor = (Color)ColorConverter.ConvertFromString("#0023F5");
            SolidColorBrush BlueColor = new SolidColorBrush(blueColor);

            Color greenColor = (Color)ColorConverter.ConvertFromString("#77D970");
            SolidColorBrush GreenColor = new SolidColorBrush(greenColor);

            Color orangeColor = (Color)ColorConverter.ConvertFromString("#F7BB6D");
            SolidColorBrush OrangeColor = new SolidColorBrush(orangeColor);

            Color yellowColor = (Color)ColorConverter.ConvertFromString("#FFE400");
            SolidColorBrush YellowColor = new SolidColorBrush(yellowColor);

            Color blackColor = (Color)ColorConverter.ConvertFromString("#000000");
            SolidColorBrush BlackColor = new SolidColorBrush(blackColor);

            // EQP Mode에 따라 폰트 색상 분리
            //eEQPMode mode = (eEQPMode)value;
            //if (mode == eEQPMode.IDLE) { return OrangeColor; }
            //if (mode == eEQPMode.WAIT) { return BlueColor; }
            //if (mode == eEQPMode.DOWN) { return RedColor; }
            //if (mode == eEQPMode.RUN) { return GreenColor; }
            //if (mode == eEQPMode.BUSY) { return YellowColor; }

            return BlackColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BorderColorMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool isMouseOver = (bool)values[0];
            bool isCurSel = (bool)values[1];

            Color redColor = (Color)ColorConverter.ConvertFromString("#FF0075");
            SolidColorBrush RedColor = new SolidColorBrush(redColor);

            Color blueColor = (Color)ColorConverter.ConvertFromString("#0023F5");
            SolidColorBrush BlueColor = new SolidColorBrush(blueColor);

            Color blackColor = (Color)ColorConverter.ConvertFromString("#000000");
            SolidColorBrush BlackColor = new SolidColorBrush(blackColor);

            if (isCurSel) 
            { 
                return RedColor; 
            }
            else {
                if (isMouseOver)
                {
                    return BlueColor;
                }
                else
                {
                    return BlackColor;
                }
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StatusToBottleImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 상태에 따라 이미지를 변경
            //eBottleStatus status = (eBottleStatus)value;
            string imagePath = "";

            //switch (status)
            //{
            //    case eBottleStatus.In:
            //        imagePath = "../../Resources/Image/Bottle.png";
            //        break;
            //    case eBottleStatus.Out:
            //        imagePath = "../../Resources/Image/No_Bottle.png";
            //        break;
            //    default:
            //        imagePath = "../../Resources/Image/No_Bottle.png";
            //        break;
            //}

            return imagePath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // ConvertBack은 사용하지 않음
            throw new NotImplementedException();
        }
    }
    public class StatusToButtonColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color beigeColor = (Color)ColorConverter.ConvertFromString("#F5F5DC");
            SolidColorBrush BeigeColor = new SolidColorBrush(beigeColor);

            Color yellowColor = (Color)ColorConverter.ConvertFromString("#FFE400");
            SolidColorBrush YellowColor = new SolidColorBrush(yellowColor);

            Color greenColor = (Color)ColorConverter.ConvertFromString("#1DDB16");
            SolidColorBrush GreenColor = new SolidColorBrush(greenColor);

            Color redColor = (Color)ColorConverter.ConvertFromString("#FF0075");
            SolidColorBrush RedColor = new SolidColorBrush(redColor);

            return BeigeColor;

            //eProgress val = (eProgress)value;
            //switch (val)
            //{
            //    case eProgress.IDLE:
            //        return BeigeColor;
            //    case eProgress.RUNNING:
            //        return YellowColor;
            //    case eProgress.COMPLETE:
            //        return GreenColor;
            //    case eProgress.ERROR:
            //        return RedColor;
            //    default:
            //        return BeigeColor;

            //};
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class BottleProgressColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color beigeColor = (Color)ColorConverter.ConvertFromString("#F5F5DC");
            SolidColorBrush BeigeColor = new SolidColorBrush(beigeColor);

            Color yellowColor = (Color)ColorConverter.ConvertFromString("#FFE400");
            SolidColorBrush YellowColor = new SolidColorBrush(yellowColor);

            Color greenColor = (Color)ColorConverter.ConvertFromString("#1DDB16");
            SolidColorBrush GreenColor = new SolidColorBrush(greenColor);

            Color redColor = (Color)ColorConverter.ConvertFromString("#FF0075");
            SolidColorBrush RedColor = new SolidColorBrush(redColor);

            return BeigeColor;

            //eProgress val = (eProgress)value;
            //switch (val)
            //{
            //    case eProgress.IDLE:
            //        return BeigeColor;
            //    case eProgress.RUNNING:
            //        return YellowColor;
            //    case eProgress.COMPLETE:
            //        return GreenColor;
            //    case eProgress.ERROR:
            //        return RedColor;
            //    default:
            //        return BeigeColor;
            //};
        }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StockerBottleToButtonColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color whiteColor = (Color)ColorConverter.ConvertFromString("#FFFFFF");
            SolidColorBrush WhiteColor = new SolidColorBrush(whiteColor);

            Color grayColor = (Color)ColorConverter.ConvertFromString("#5D5D5D");
            SolidColorBrush GrayColor = new SolidColorBrush(grayColor);

            Color redColor = (Color)ColorConverter.ConvertFromString("#FF0075");
            SolidColorBrush RedColor = new SolidColorBrush(redColor);

            Color greenColor = (Color)ColorConverter.ConvertFromString("#1DDB16");
            SolidColorBrush GreenColor = new SolidColorBrush(greenColor);

            Color blueColor = (Color)ColorConverter.ConvertFromString("#4374D9");
            SolidColorBrush BlueColor = new SolidColorBrush(blueColor);

            return WhiteColor;
            //eStockerBottleStatus val = (eStockerBottleStatus)value;
            //switch (val)
            //{
            //    case eStockerBottleStatus.EMPTY_HOLE:
            //        return WhiteColor;
            //    case eStockerBottleStatus.EMPTY_BOTTLE:
            //        return GrayColor;
            //    case eStockerBottleStatus.REAL_BOTTLE_BEFORE_ANALYSIS:
            //        return RedColor;
            //    case eStockerBottleStatus.REAL_BOTTLE_AFTER_ANALYSIS:
            //        return GreenColor;
            //    case eStockerBottleStatus.INTERRUPT_BOTTLE:
            //        return BlueColor;
            //    default:
            //        return WhiteColor;
            //};
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class MultiBindingCommandParamConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Clone();
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
