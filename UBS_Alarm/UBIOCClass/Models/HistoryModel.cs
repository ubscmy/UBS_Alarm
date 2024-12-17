using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using UBIOCClass.ViewModels;

namespace UBIOCClass.Models
{
    public class HistoryModel : NotifyPropertyChangedBase
    {
        private ObservableCollection<Alarm> _AlarmData = new ObservableCollection<Alarm>();
        public ObservableCollection<Alarm> AlarmData { get => _AlarmData; set => SetProperty(ref _AlarmData, value); }

        private Alarm _SelectedAlarmData = new Alarm();
        public Alarm SelectedAlarmData { get => _SelectedAlarmData; set => SetProperty(ref _SelectedAlarmData, value); }

        public string _AlarmCode;
        public string _AlarmType;
        public string _AlarmName;
        public string _AlarmDescription;
        public string _AlarmSolveDescription;
        public string _AlarmLevel;
        public string _AlarmNote;
        public DateTime _AlarmStartDateTime = DateTime.Now.AddDays(-7);
        public DateTime _AlarmEndDateTime = DateTime.Now.AddDays(1);


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
        public DateTime AlarmStartDateTime
        {
            get => _AlarmStartDateTime;
            set => SetProperty(ref _AlarmStartDateTime, value);
        }
        public DateTime AlarmEndDateTime
        {
            get => _AlarmEndDateTime;
            set => SetProperty(ref _AlarmEndDateTime, value);
        }

    }
}
