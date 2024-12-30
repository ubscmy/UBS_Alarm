using System;
using System.Collections.Generic;
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
    public class WinAlarmViewModel : NotifyPropertyChangedBase
    {
        public void Cancel(object _)
        {
            return;
        }


        private Alarm _AlarmData;

        public Alarm AlarmData
        {
            get => _AlarmData;
            set { SetProperty(ref _AlarmData, value); SetAlarmValue(); }
        }



        public string _AlarmCode;
        public string _AlarmType;
        public string _AlarmName;
        public string _AlarmDescription;
        public string _AlarmSolveDescription;
        public string _AlarmLevel;
        public string _AlarmNote;


        public void SetAlarmValue()
        {
            AlarmCode = AlarmData.AlarmCode;
            AlarmType = AlarmData.AlarmType;
            AlarmName = AlarmData.AlarmName;
            AlarmDescription = AlarmData.AlarmDescription;
            AlarmSolveDescription = AlarmData.AlarmSolveDescription;
            AlarmLevel = AlarmData.AlarmLevel;
            AlarmNote = AlarmData.AlarmNote;
        }

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


    }
}
