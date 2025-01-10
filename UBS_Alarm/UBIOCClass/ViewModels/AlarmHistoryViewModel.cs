using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UBIOCClass.Commands;
using UBIOCClass.Models;
using UBIOCClass.Services;

namespace UBIOCClass.ViewModels
{
    public class AlarmHistoryViewModel : NotifyPropertyChangedBase
    {
        private ObservableCollection<Alarm> _AlarmHistoryData = new ObservableCollection<Alarm>();
        public ObservableCollection<Alarm> AlarmHistoryData { get => _AlarmHistoryData; set => SetProperty(ref _AlarmHistoryData, value); }

        private Alarm _SelectedHistoryAlarmData = new Alarm();
        public Alarm SelectedHistoryAlarmData { get => _SelectedHistoryAlarmData; set => SetProperty(ref _SelectedHistoryAlarmData, value); }

        private string _AlarmCode;
        private string _AlarmType;
        private string _AlarmName;
        private string _AlarmDescription;
        private string _AlarmSolveDescription;
        private string _AlarmLevel;
        private string _AlarmNote;
        private DateTime? _AlarmStartDateTime ;
        private DateTime? _AlarmEndDateTime;


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
        public DateTime? AlarmStartDateTime
        {
            get => _AlarmStartDateTime;
            set => SetProperty(ref _AlarmStartDateTime, value);
        }
        public DateTime? AlarmEndDateTime
        {
            get => _AlarmEndDateTime;
            set => SetProperty(ref _AlarmEndDateTime, value);
        }

        private ICommandModel _ICommandModel = new ICommandModel();
        public ICommandModel ICommandModel { get => _ICommandModel; set => SetProperty(ref _ICommandModel, value); }

        private readonly INavigationService _navigationService;

        private void DataSelect(object _)
        {
            AlarmCode = "";
            AlarmName = "";
            AlarmLevel = "";

            AlarmStartDateTime =  DateTime.Now.AddDays(-7);
            AlarmEndDateTime = DateTime.Now;
            AlarmHistoryData.Clear();
            AlarmHistoryData = HistoryDBCommand.HistoryDataSelect(ref _SelectedHistoryAlarmData);
            NotifyPropertyChanged();
        }

        public void TableCreate(object _)
        {
            if (HistoryDBCommand.CreateTable())
                MessageBox.Show("Table 생성 완료");
        }
        private void DataInsert(object _)
        {
            HistoryDBCommand.Hisotry_DataInsert(AlarmCode);
            AlarmCode = string.Empty;
            DataSelect(null);
        }

        private void DataSearch(object _)
        {
            AlarmHistoryData.Clear();
            AlarmHistoryData = HistoryDBCommand.HistoryDataSearch(AlarmCode, AlarmType, AlarmDescription, AlarmLevel, AlarmName, AlarmNote, AlarmSolveDescription,AlarmStartDateTime,AlarmEndDateTime);
            NotifyPropertyChanged();
        }


        public AlarmHistoryViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            DataSelect(null);
            HistoryCommands();
        }

        private void HistoryCommands()
        {
            ICommandModel.DB_Select = new DelegateCommand(DataSelect);
            ICommandModel.DB_Insert = new DelegateCommand(DataInsert);
            ICommandModel.DB_SearchSelect = new DelegateCommand(DataSearch);
            ICommandModel.CreateTable = new DelegateCommand(TableCreate);
        }
    }


}
