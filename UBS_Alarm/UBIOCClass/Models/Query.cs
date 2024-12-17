using ExcelDataReader;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UBIOCClass.ViewModels;

namespace UBIOCClass.Models
{
    public class Query : SQLConn
    {
        //SQLConn sqlDB = new SQLConn();
        public enum CheckStatus
        {
            ValueExists = 0,               // 값 존재
            EmptyValue = 1,            // 빈 값
            DuplicateExists = 20,      // 중복 있음
            NoDuplicate = 30,           // 중복 없음

            UpdateAllowed = 20,   // Update 진행 가능
            InsertAllowed = 30,  // Insert 진행 가능
        }

        public bool AlarmDataNullOrEmpty(RegisterModel Register) { return new[] { Register.AlarmCode, Register.AlarmType, Register.AlarmName, Register.AlarmDescription, Register.AlarmSolveDescription, Register.AlarmLevel, Register._AlarmNote }.Any(string.IsNullOrEmpty); }
        public bool DuplicateExists(RegisterModel Register) { return CheckRegisterDup(Register.AlarmCode); }
        public void AlarmPropertiesClear(ref RegisterModel Register) { Register.AlarmCode = Register.AlarmType = Register.AlarmName = Register.AlarmDescription = Register.AlarmSolveDescription = Register.AlarmLevel = Register.AlarmNote = string.Empty; }
        public int CheckIsAallowed(RegisterModel Register)
        {
            // Insert나 Update 중 조건 확인
            int Status = 0;

            Status += AlarmDataNullOrEmpty(Register) == true ? (int)CheckStatus.EmptyValue : (int)CheckStatus.ValueExists; // 입력이 안된 곳이 있는지 Check
            Status += DuplicateExists(Register) == true ? (int)CheckStatus.DuplicateExists : (int)CheckStatus.NoDuplicate; // AlarmCode 중복 체크
            return Status;
        }


        public bool CreateTable() { return _CreateTable(); } // 테이블 생성


        // History 기본 데이터를 불러온다
        public ObservableCollection<Alarm> HistoryDataSelect(ref HistoryModel historyModel)
        {
            historyModel.AlarmData.Clear();

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
        public ObservableCollection<Alarm> HistoryDataSearch(ref HistoryModel historyModel)
        {
            historyModel.AlarmData.Clear();
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

        // Register에서 기본 데이터를 불러온다.
        public ObservableCollection<Alarm> RegisterDataSelect(ref RegisterModel registerModel)
        {
            registerModel.AlarmData.Clear();
            // 데이터 조회 후 list에 저장
            List<string>[] list = Register_ReadData();

            // 데이터가 비어 있거나 열 수가 부족한 경우 빈 컬렉션 반환
            if (list == null || list.Length < 8 || list.Any(l => l == null || l.Count == 0))
                return new ObservableCollection<Alarm>();

            // Alarm 객체를 생성하여 ObservableCollection으로 처리
            var Alarms = Enumerable.Range(0, list[0].Count).Select(i => new Alarm
            {
                AlarmID = list[0][i],
                AlarmCode = list[1][i],
                AlarmType = list[2][i],
                AlarmName = list[3][i],
                AlarmDescription = list[4][i],
                AlarmSolveDescription = list[5][i],
                AlarmLevel = list[6][i],
                AlarmNote = list[7][i]
            })
                .ToList();
            AlarmPropertiesClear(ref registerModel);
            // ObservableCollection으로 변환
            return new ObservableCollection<Alarm>(Alarms);
        }

        // Register에서 Data를 Insert한다.
        public bool Regster_DataInsert(ref RegisterModel Register)
        {
            if (CheckIsAallowed(Register) != (int)CheckStatus.InsertAllowed)
                return false;

            InsertData(Register.AlarmCode, Register.AlarmType, Register.AlarmName, Register.AlarmDescription, Register.AlarmSolveDescription, Register.AlarmLevel, Register._AlarmNote);
            AlarmPropertiesClear(ref Register);

            return true;
        }

        // Register에서 Data를 Update한다.
        public bool Register_DataUpdate(ref RegisterModel Register)
        {
            if (CheckIsAallowed(Register) != (int)CheckStatus.UpdateAllowed)
                return false;

            UpdateData(Register.AlarmCode, Register.AlarmType, Register.AlarmName, Register.AlarmDescription, Register.AlarmSolveDescription, Register.AlarmLevel, Register._AlarmNote);
            AlarmPropertiesClear(ref Register);

            return true;
        }
        // Register에서 Data를 Delete한다.
        public bool Register_DataDelete(ref RegisterModel Register)
        {
            if (Register.AlarmCode == null || Register.AlarmCode.Equals("")) return false;

            DeleteData(Register.AlarmCode);
            AlarmPropertiesClear(ref Register);

            return true;
        }

        // Hisotry에서 Data를 Insert한다.
        public void Hisotry_DataInsert(ref HistoryModel historyModel)
        {
            string AlarmCode = historyModel.AlarmCode;
            string NowDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            HisInsertData(AlarmCode, NowDateTime);
            historyModel.AlarmCode = string.Empty;
        }


        // Register에 기본적인 값을 입력한다.
        public void ExcelToDBInsert(string FilePath)
        {
            // Excel 값을 읽어와 DB에 넣기
            _CreateTable();
            string Xlsx_FilePath = FilePath;

            // Excel 파일을 읽어오기 위한 인코딩 등록
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // Excel 데이터 읽기
            using (var stream = File.Open(Xlsx_FilePath, FileMode.Open, FileAccess.Read))
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var config = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration { UseHeaderRow = true }
                };

                while (reader.Read())
                {
                    if (reader.Depth == 0) continue; // 헤더 행 건너뜀

                    InsertData(
                        reader.GetValue(0)?.ToString(),
                        reader.GetValue(1)?.ToString(),
                        reader.GetValue(2)?.ToString(),
                        reader.GetValue(3)?.ToString(),
                        reader.GetValue(4)?.ToString(),
                        reader.GetValue(5)?.ToString(),
                        reader.GetValue(6)?.ToString()
                    );
                }
            }
        }

    }
}
