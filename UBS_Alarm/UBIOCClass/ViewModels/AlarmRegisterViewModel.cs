using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
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


        private void DataInsert(object _)
        {
            bool bSuccess = RegisterDBCommand.Register_DataInsert(ref _SelectedRegisterAlarmData);
            if (bSuccess == true)
                DataSelect(null);
        }
        private void DataUpdate(object _)
        {
            bool bSuccess = RegisterDBCommand.Register_DataUpdate(ref _SelectedRegisterAlarmData);
            if (bSuccess == true)
                DataSelect(null);
        }
        private void DataDelete(object _)
        {
            bool bSuccess = RegisterDBCommand.Register_DataDelete(ref _SelectedRegisterAlarmData);
            if (bSuccess == true)
                DataSelect(null);
        }

        private void DataSelect(object _)
        {
            AlarmRegisterData.Clear();
            AlarmRegisterData = RegisterDBCommand.RegisterDataSelect(ref _SelectedRegisterAlarmData);
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
        }

    }
}
