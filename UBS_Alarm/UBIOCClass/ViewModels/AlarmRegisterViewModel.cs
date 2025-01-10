using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;
using UBIOCClass.Commands;
using UBIOCClass.Models;
using UBIOCClass.Services;
using UBIOCClass.Views;

namespace UBIOCClass.ViewModels
{
    public class AlarmRegisterViewModel : NotifyPropertyChangedBase
    {
        private ObservableCollection<Alarm> _AlarmRegisterData = new ObservableCollection<Alarm>();
        public ObservableCollection<Alarm> AlarmRegisterData { get => _AlarmRegisterData; set => SetProperty(ref _AlarmRegisterData, value); }

        private Alarm _SelectedRegisterAlarmData = new Alarm();
        public Alarm SelectedRegisterAlarmData { get => _SelectedRegisterAlarmData; set { SetProperty(ref _SelectedRegisterAlarmData, value); SelectedModelData(); } }

        private void SelectedModelData()
        {
            try
            {
                AlarmCode = SelectedRegisterAlarmData.AlarmCode;
                AlarmName = SelectedRegisterAlarmData.AlarmName;
                AlarmType = SelectedRegisterAlarmData.AlarmType;
                AlarmDescription = SelectedRegisterAlarmData.AlarmDescription;
                AlarmSolveDescription = SelectedRegisterAlarmData.AlarmSolveDescription;
                AlarmLevel = SelectedRegisterAlarmData.AlarmLevel;
                AlarmNote = SelectedRegisterAlarmData.AlarmNote;
            }
            catch (NullReferenceException)
            {
                // 정상 작동한거임
            }
        }

        public string _AlarmCode;
        public string _AlarmType;
        public string _AlarmName;
        public string _AlarmDescription;
        public string _AlarmSolveDescription;
        public string _AlarmLevel;
        public string _AlarmNote;

        public string AlarmCode
        {
            get => _AlarmCode;
            set => SetProperty(ref _AlarmCode, value);
        }
        public string AlarmType
        {
            get => _AlarmType;
            set => SetProperty(ref _AlarmType, value);
        }
        public string AlarmName
        {
            get => _AlarmName;
            set => SetProperty(ref _AlarmName, value);
        }

        public string AlarmDescription
        {
            get => _AlarmDescription;
            set => SetProperty(ref _AlarmDescription, value);
        }

        public string AlarmSolveDescription
        {
            get => _AlarmSolveDescription;
            set => SetProperty(ref _AlarmSolveDescription, value);
        }

        public string AlarmLevel
        {
            get => _AlarmLevel;
            set => SetProperty(ref _AlarmLevel, value);
        }
        public string AlarmNote
        {
            get => _AlarmNote;
            set => SetProperty(ref _AlarmNote, value);
        }

        private ICommandModel _ICommandModel = new ICommandModel();
        public ICommandModel ICommandModel { get => _ICommandModel; set => SetProperty(ref _ICommandModel, value); }


        private void DBAlarmTest(object _)
        {
            int seqno = 1;
            int sensorNo = 4;
            double angle = 15.5;
            float curAngle = 7.5f;
            bool check = false;

            AlarmScreenShow("300", "StageSequence -", seqno, "에서 Sensor", sensorNo, "번 문제 발생.", "프로그램 재실행하세요.", angle, curAngle, check);
            AlarmScreenShow("300", "Sensor", sensorNo, "번의 문제가", "StageSequence -", seqno, "에서 발생.", "프로그램 재실행하세요.", angle, curAngle, check);
        }

        public string _SequenceName;
        public int _SequenceNumber;


        public string SequenceName
        {
            get => _SequenceName;
            set => SetProperty(ref _SequenceName, value);
        }
        public int SequenceNumber
        {
            get => _SequenceNumber;
            set => SetProperty(ref _SequenceNumber, value);
        }


        private void AlarmScreenShow(object AlarmCode, params object[] args)
        {
            if (AlarmCode is double || AlarmCode is float)
            {
                MessageBox.Show("소수점");
                return;
            }

            string str = string.Empty;
            foreach (var value in args)
            {
                str += value.ToString() + " ";
            }


            var cAlarmScreen = AlarmScreen(AlarmCode, str);
            cAlarmScreen.Show();
            MessageBox.Show(str);

        }

        private WinAlarm AlarmScreen(object AlarmCode, string SolveDescription)
        {
            var viewModel = new WinAlarmViewModel();
            viewModel.AlarmData = RegisterDBCommand.AlarmTest(AlarmCode, SolveDescription);

            var alarmWindow = new WinAlarm();
            alarmWindow.DataContext = viewModel;  // DataContext 설정

            // 윈도우를 화면 가운데에서 시작하도록 설정
            alarmWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            return alarmWindow;
        }

        private Alarm AlarmObject()
        {
            Alarm alarm = new Alarm();
            alarm.AlarmCode = AlarmCode;
            alarm.AlarmDescription = AlarmDescription;
            alarm.AlarmName = AlarmName;
            alarm.AlarmSolveDescription = AlarmSolveDescription;
            alarm.AlarmType = AlarmType;
            alarm.AlarmLevel = AlarmLevel;
            alarm.AlarmNote = AlarmNote;

            return alarm;
        }

        private void DataInsert(object _)
        {
            var alarm = AlarmObject();
            bool bSuccess = RegisterDBCommand.Register_DataInsert(ref alarm);
            if (bSuccess == true)
                DataRefresh(null);
        }


        private void DataUpdate(object _)
        {
            var alarm = AlarmObject();
            bool bSuccess = RegisterDBCommand.Register_DataUpdate(ref alarm);
            if (bSuccess == true)
                DataRefresh(null);
        }
        private void DataDelete(object _)
        {
            bool bSuccess = RegisterDBCommand.Register_DataDelete(ref _SelectedRegisterAlarmData);
            if (bSuccess == true)
                DataRefresh(null);
        }

        private void DataSelect(object _)
        {
            AlarmRegisterData.Clear();
            AlarmRegisterData = RegisterDBCommand.RegisterDataSelect(ref _SelectedRegisterAlarmData);
            NotifyPropertyChanged();
        }
        private void DataRefresh(object _)
        {
            AlarmRegisterData.Clear();
            AlarmRegisterData = RegisterDBCommand.RegisterRefresh(ref _SelectedRegisterAlarmData);
            NotifyPropertyChanged();
        }
        private void CSVImport(object _)
        {

            string path = string.Empty;
            // OpenFileDialog 인스턴스 생성
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "파일 선택",
                Filter = "모든 파일 (*.*)|*.*" // 필요하면 필터 설정
            };

            // 대화 상자 표시
            if (openFileDialog.ShowDialog() == true)
            {
                // 선택한 파일 경로 가져오기
                path = openFileDialog.FileName;

                // 파일 경로를 출력하거나 다른 작업 수행
                MessageBox.Show("선택한 파일 경로: " + path);
            }
            else
            {
                MessageBox.Show("파일이 선택되지 않았습니다.");
            }

            RegisterDBCommand.ExcelToDBInsert(path);
            NotifyPropertyChanged();
        }

        public AlarmRegisterViewModel(INavigationService navigationService)
        {
            DataSelect(null);
            RegisterCommands();
        }

        private void RegisterCommands()
        {
            ICommandModel.DB_Insert = new DelegateCommand(DataInsert);
            ICommandModel.DB_Update = new DelegateCommand(DataUpdate);
            ICommandModel.DB_Delete = new DelegateCommand(DataDelete);
            ICommandModel.DB_Select = new DelegateCommand(DataSelect);
            ICommandModel.AlarmTest = new DelegateCommand(DBAlarmTest);
            ICommandModel.ExcelImport = new DelegateCommand(CSVImport);
        }

    }
}
