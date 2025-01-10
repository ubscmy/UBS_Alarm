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
            set { SetProperty(ref _AlarmData, value); SetAlarmValue(AlarmData.AlarmSolveDescription); }
        }
        private ObservableCollection<Alarm> _AlarmHistoryData = new ObservableCollection<Alarm>();
        public ObservableCollection<Alarm> AlarmHistoryData { get => _AlarmHistoryData; set => SetProperty(ref _AlarmHistoryData, value); }


        private string? _AlarmCode;
        private string? _AlarmType;
        private string? _AlarmName;
        private string? _AlarmDescription;
        private string? _AlarmSolveDescription;
        private string? _AlarmLevel;
        private string? _AlarmNote;
        private int _AlarmWeekCount = 0; 

        //public void LOG(params object[] args)
        //{
         
        //  MessageBox.Show($"{args[0]}-{args[1]}번, Motor{args[2]}번에서 {args[3]} 발생.\r\n 문제 해결 방법 : {args[4]}");
        //}

        public void SetAlarmValue(string SolveDescription)
        {
            var AlarmDB = new SQLQuery();
            AlarmCode = AlarmData.AlarmCode;
            AlarmType = AlarmData.AlarmType;
            AlarmName = AlarmData.AlarmName;
            AlarmDescription = AlarmData.AlarmDescription;
            AlarmSolveDescription = SolveDescription;
            //AlarmSolveDescription = AlarmData.AlarmSolveDescription;
            AlarmLevel = AlarmData.AlarmLevel;
            AlarmNote = AlarmData.AlarmNote;
            AlarmHistoryData = SelectAlarmWeekCount(AlarmDB, AlarmCode);
            /*
                문제 해결 방안에 띄울 내용을 변수로 입력받는다.
                AlarmCode를 같이 입력해서 데이터를 불러온다.

             */
            //LOG(AlarmCode, $"문제 해결 방안 {sensor1}");
        }


        private ObservableCollection<Alarm> SelectAlarmWeekCount(SQLQuery AlarmDB, string AlarmCode)
        {
            List<string>[] value = AlarmDB.SELECT_AlarmWeekCount(AlarmCode);

            // Alarm 주간 발생 횟수를 입력한다.
            AlarmWeekCount = value[0].Count;

            ObservableCollection<Alarm> _AlarmData = new ObservableCollection<Alarm>
           (
             value[0].Select((AlarmTime, index) => new Alarm
             {
                 AlarmOcucurrenceTime = AlarmTime
             })
           );

            return _AlarmData;
        }

        public int AlarmWeekCount
        {
            get => _AlarmWeekCount;
            set => SetProperty(ref _AlarmWeekCount, value);
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
