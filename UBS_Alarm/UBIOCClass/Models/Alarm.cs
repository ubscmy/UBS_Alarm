using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using UBIOCClass.ViewModels;

namespace UBIOCClass
{
    public class Alarm : NotifyPropertyChangedBase
    {
        private string _AlarmID = "";
        private string _AlarmOcucurrenceTime = "";
        private string _AlarmLevel = "";
        private string _AlarmCode = "";
        private string _AlarmName = "";
        private string _AlarmType = "";
        private string _AlarmDescription = "";
        private string _AlarmSolveDescription = "";
        private string _AlarmNote = "";

        public string AlarmID { get => _AlarmID; set => SetProperty(ref _AlarmID, value); }
        public string AlarmOcucurrenceTime { get => _AlarmOcucurrenceTime; set => SetProperty(ref _AlarmOcucurrenceTime, value); }
        public string AlarmLevel { get => _AlarmLevel; set => SetProperty(ref _AlarmLevel, value); }
        public string AlarmCode { get => _AlarmCode; set => SetProperty(ref _AlarmCode, value); }
        public string AlarmName { get => _AlarmName; set => SetProperty(ref _AlarmName, value); }
        public string AlarmType { get => _AlarmType; set => SetProperty(ref _AlarmType, value); }
        public string AlarmDescription { get => _AlarmDescription; set => SetProperty(ref _AlarmDescription, value); }
        public string AlarmSolveDescription { get => _AlarmSolveDescription; set => SetProperty(ref _AlarmSolveDescription, value); }
        public string AlarmNote { get => _AlarmNote; set => SetProperty(ref _AlarmNote, value); }

        public Brush AlarmLevelColor { get { return AlarmLevel == "LIGHT" ? Brushes.Yellow : Brushes.Red; }}
        public Brush AlarmLevelForeColor { get { return AlarmLevel == "LIGHT" ? Brushes.Black : Brushes.White; }}
    }
}
