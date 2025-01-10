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
using System.Windows.Shapes;
using UBIOCClass.Services;
using UBIOCClass.Stores;
using UBIOCClass.ViewModels;

namespace UBIOCClass.Views
{
    /// <summary>
    /// Alarm.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AlarmView : UserControl
    {
        public AlarmHistoryViewModel AlarmHistoryViewmodel { get; set; }
        public AlarmRegisterViewModel AlarmRegisterViewmodel { get; set; }
        public WinAlarmChartViewModel WinAlarmChartViewModel { get; set; }


        public AlarmView()
        {
            InitializeComponent();

            var mainNavigationStore = new MainNavigationStore();
            INavigationService navigationService = new NavigationService(mainNavigationStore);

            AlarmHistoryViewmodel = new AlarmHistoryViewModel(navigationService);
            AlarmRegisterViewmodel = new AlarmRegisterViewModel(navigationService);
            WinAlarmChartViewModel = new WinAlarmChartViewModel(navigationService);

            DataContext = this; // MainWindow 자체를 DataContext로 설정
        }
    }
}
