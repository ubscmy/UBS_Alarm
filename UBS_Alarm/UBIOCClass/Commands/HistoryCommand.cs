using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBIOCClass.Models;
using UBIOCClass.ViewModels;

namespace UBIOCClass.Commands
{
    public class HistoryCommand : SQLQuery
    {
        public bool CreateTable() { return _CreateTable(); } // 테이블 생성

        // Hisotry에서 Data를 Insert한다.
        public void Hisotry_DataInsert(ref Alarm historyModel)
        {
            string AlarmCode = historyModel.AlarmCode;
            string NowDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            HisInsertData(AlarmCode, NowDateTime);
            historyModel.AlarmCode = string.Empty;
        }

        // History 기본 데이터를 불러온다
        public ObservableCollection<Alarm> HistoryDataSelect(ref Alarm historyModel)
        {
            List<string>[] list = History_ReadData();
            ObservableCollection<Alarm> _AlarmData = new ObservableCollection<Alarm>
            (
              list[0].Select((alarmID, index) => new Alarm
              {
                  AlarmID = alarmID,
                  AlarmCode = list[1][index],
                  AlarmType = list[2][index],
                  AlarmName = list[3][index],
                  AlarmDescription = list[4][index],
                  AlarmSolveDescription = list[5][index],
                  AlarmLevel = list[6][index],
                  AlarmNote = list[7][index],
                  AlarmOcucurrenceTime = list[8][index]
              })
            );
            return _AlarmData;
        }
        // History에서 검색했을 때 데이터를 불러온다.
        public ObservableCollection<Alarm> HistoryDataSearch(ref Alarm historyModel)
        {
            // 데이터 조회 후 list에 저장
            List<string>[] list = History_SearchData(historyModel.AlarmCode, historyModel.AlarmType, historyModel.AlarmName, historyModel.AlarmDescription, historyModel.AlarmSolveDescription, historyModel.AlarmLevel, historyModel.AlarmNote, historyModel.AlarmStartDateTime, historyModel.AlarmEndDateTime);

            // Alarm 객체 생성
            var alarms = Enumerable.Range(0, list[0].Count).Select(index => new Alarm
            {
                AlarmID = list[0][index],
                AlarmCode = list[1][index],
                AlarmType = list[2][index],
                AlarmName = list[3][index],
                AlarmDescription = list[4][index],
                AlarmSolveDescription = list[5][index],
                AlarmLevel = list[6][index],
                AlarmNote = list[7][index],
                AlarmOcucurrenceTime = list[8][index]
            }).ToList();

            return new ObservableCollection<Alarm>(alarms);
        }
    }
}
