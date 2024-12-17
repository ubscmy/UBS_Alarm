using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UBIOCClass.Views
{
    /// <summary>
    /// NavigationBar.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NavigationBar : UserControl
    {
        public NavigationBar()
        {
            InitializeComponent();
        }

         public Orientation OrientationMode
         {
            get { return (Orientation)GetValue(OrientationModeProperty); }
            set { SetValue(OrientationModeProperty, value); }
        }
 
         // Using a DependencyProperty as the backing store for OrientationMode.  This enables animation, styling, binding, etc...
         public static DependencyProperty OrientationModeProperty = 
             DependencyProperty.Register("OrientationMode", typeof(Orientation), 
                 typeof(NavigationBar),
                 new PropertyMetadata(Orientation.Vertical, OnCustomOrientationChanged));

        private static void OnCustomOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var panel = d as NavigationBar;
            if (panel != null)
            {
                // StackPanel의 기본 Orientation 속성을 커스텀 속성으로 동기화
                panel.mainpanel.Orientation = (Orientation)e.NewValue;
            }
        }
    }
}
