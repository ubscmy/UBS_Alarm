using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public string AlarmID { get => _AlarmID; set => SetProperty(ref _AlarmID, value); }

        private string _AlarmOcucurrenceTime = "";
        public string AlarmOcucurrenceTime { get => _AlarmOcucurrenceTime; set => SetProperty(ref _AlarmOcucurrenceTime, value); }

        private string _AlarmLevel = "";
        public string AlarmLevel { get => _AlarmLevel; set => SetProperty(ref _AlarmLevel, value); }

        private string _AlarmCode = "";
        public string AlarmCode { get => _AlarmCode; set => SetProperty(ref _AlarmCode, value); }

        private string _AlarmName = "";
        public string AlarmName { get => _AlarmName; set => SetProperty(ref _AlarmName, value); }

        private string _AlarmType = "";
        public string AlarmType { get => _AlarmType; set => SetProperty(ref _AlarmType, value); }
                
        private string _AlarmDescription = "";
        public string AlarmDescription { get => _AlarmDescription; set => SetProperty(ref _AlarmDescription, value); }    
        
        private string _AlarmSolveDescription = "";
        public string AlarmSolveDescription { get => _AlarmSolveDescription; set => SetProperty(ref _AlarmSolveDescription, value); }  

        private string _AlarmNote = "";
        public string AlarmNote { get => _AlarmNote; set => SetProperty(ref _AlarmNote, value); }    
        
        public DateTime _AlarmStartDateTime = DateTime.Now.AddDays(-7);
        public DateTime AlarmStartDateTime { get => _AlarmStartDateTime; set => SetProperty(ref _AlarmStartDateTime, value); }
        
        public DateTime _AlarmEndDateTime = DateTime.Now.AddDays(1);
        public DateTime AlarmEndDateTime { get => _AlarmEndDateTime; set => SetProperty(ref _AlarmEndDateTime, value); }

        public Brush AlarmLevelColor { get { return AlarmLevel == "LIGHT" ? Brushes.Yellow : Brushes.Red; }}
        public Brush AlarmLevelForeColor { get { return AlarmLevel == "LIGHT" ? Brushes.Black : Brushes.White; }}
    }
}
