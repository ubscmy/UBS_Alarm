using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBIOCClass.Models;
using UBIOCClass.ViewModels;

namespace UBIOCClass.Commands
{
    public class RegisterCommand : SQLQuery
    {
        public enum CheckStatus
        {
            ValueExists = 0,               // 값 존재
            EmptyValue = 1,            // 빈 값
            DuplicateExists = 20,      // 중복 있음
            NoDuplicate = 30,           // 중복 없음

            UpdateAllowed = 20,   // Update 진행 가능
            InsertAllowed = 30,  // Insert 진행 가능
        }

        public bool AlarmDataNullOrEmpty(Alarm Register) { return new[] { Register.AlarmCode, Register.AlarmType, Register.AlarmName, Register.AlarmDescription, Register.AlarmSolveDescription, Register.AlarmLevel, Register.AlarmNote }.Any(string.IsNullOrEmpty); }
        public bool DuplicateExists(Alarm Register) { return CheckRegisterDup(Register.AlarmCode); }
        public void AlarmPropertiesClear(ref Alarm Register) { Register.AlarmCode = Register.AlarmType = Register.AlarmName = Register.AlarmDescription = Register.AlarmSolveDescription = Register.AlarmLevel = Register.AlarmNote = string.Empty; }
        public int CheckIsAallowed(Alarm Register)
        {
            // Insert나 Update 중 조건 확인
            int Status = 0;

            Status += AlarmDataNullOrEmpty(Register) == true ? (int)CheckStatus.EmptyValue : (int)CheckStatus.ValueExists; // 입력이 안된 곳이 있는지 Check
            Status += DuplicateExists(Register) == true ? (int)CheckStatus.DuplicateExists : (int)CheckStatus.NoDuplicate; // AlarmCode 중복 체크
            return Status;
        }

        public bool CreateTable() { return _CreateTable(); } // 테이블 생성

        // Register에서 기본 데이터를 불러온다.
        public ObservableCollection<Alarm> RegisterDataSelect(ref Alarm registerModel)
        {
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
        public bool Register_DataInsert(ref Alarm Register)
        {
            if (CheckIsAallowed(Register) != (int)CheckStatus.InsertAllowed)
                return false;

            InsertData(Register.AlarmCode, Register.AlarmType, Register.AlarmName, Register.AlarmDescription, Register.AlarmSolveDescription, Register.AlarmLevel, Register.AlarmNote);
            AlarmPropertiesClear(ref Register);

            return true;
        }

        // Register에서 Data를 Update한다.
        public bool Register_DataUpdate(ref Alarm Register)
        {
            if (CheckIsAallowed(Register) != (int)CheckStatus.UpdateAllowed)
                return false;

            UpdateData(Register.AlarmCode, Register.AlarmType, Register.AlarmName, Register.AlarmDescription, Register.AlarmSolveDescription, Register.AlarmLevel, Register.AlarmNote);
            AlarmPropertiesClear(ref Register);

            return true;
        }
        // Register에서 Data를 Delete한다.
        public bool Register_DataDelete(ref Alarm Register)
        {
            if (Register.AlarmCode == null || Register.AlarmCode.Equals("")) return false;

            DeleteData(Register.AlarmCode);
            AlarmPropertiesClear(ref Register);

            return true;
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
