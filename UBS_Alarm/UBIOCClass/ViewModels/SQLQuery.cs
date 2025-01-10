using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UBIOCClass.Commands;
using UBIOCClass.Models;

namespace UBIOCClass.ViewModels
{
    public class SQLQuery
    {
        private const string dbPath = "Alarm.db";
        private const string AlarmListFile = "D:\\DATA\\TestAlarmCode.xlsx";
        private const string connectionString = $"Data Source={dbPath};";

        private SqliteConnection OpenConnection()
        {
            var connection = new SqliteConnection(connectionString);
            connection.Open();

            var dbFile = new FileInfo(dbPath); // 데이터베이스 파일 경로
            if (!dbFile.Exists || dbFile.Length == 0)
            {
                _CreateTable();

                if (File.Exists(AlarmListFile))
                {
                    RegisterCommand Register = new RegisterCommand();
                    Register.ExcelToDBInsert(AlarmListFile);
                }

            }
            return connection;
        }

        // 테이블 생성 (단일 실행)
        public bool _CreateTable()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                // Register 테이블 생성
                var createRegisterTable = @"
                CREATE TABLE IF NOT EXISTS Register (
                    AlarmId INTEGER PRIMARY KEY AUTOINCREMENT,
                    AlarmCode INTEGER,
                    AlarmType TEXT,
                    AlarmName TEXT,
                    AlarmDescription TEXT,
                    AlarmSolveDescription TEXT,
                    AlarmLevel TEXT,
                    AlarmNote TEXT
                );
            ";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = createRegisterTable;
                    command.ExecuteNonQuery();
                }

                // History 테이블 생성
                var createHistoryTable = @"
                CREATE TABLE IF NOT EXISTS History (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    AlarmCode INTEGER,
                    DateTime DATETIME
                );
            ";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = createHistoryTable;
                    command.ExecuteNonQuery();
                }

                var createRegisterIndex = @"CREATE INDEX IF NOT EXISTS idx_register_alarmcode ON Register(AlarmCode);";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = createRegisterIndex;
                    command.ExecuteNonQuery();
                }

                var createHistoryIndex = @"CREATE INDEX IF NOT EXISTS idx_history_alarmcode ON History(AlarmCode);
"; using (var command = connection.CreateCommand())
                {
                    command.CommandText = createHistoryIndex;
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            return true;
        }

        // 데이터 삽입
        public void InsertData(string Code, string Type, string Name, string Cause, string Action, string Level, string Note)
        {
            using (var connection = OpenConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = @"
    INSERT INTO Register 
    (AlarmCode, AlarmType, AlarmName, AlarmDescription, AlarmSolveDescription, AlarmLevel, AlarmNote)
    VALUES (@AlarmCode, @AlarmType, @AlarmName, @AlarmDescription, @AlarmSolveDescription, @AlarmLevel, @AlarmNote);
    ";


                // 파라미터 설정
                command.Parameters.AddWithValue("@AlarmCode", Code);
                command.Parameters.AddWithValue("@AlarmType", Type);
                command.Parameters.AddWithValue("@AlarmName", Name);
                command.Parameters.AddWithValue("@AlarmDescription", Cause);
                command.Parameters.AddWithValue("@AlarmSolveDescription", Action);
                command.Parameters.AddWithValue("@AlarmLevel", Level);
                command.Parameters.AddWithValue("@AlarmNote", Note);

                command.ExecuteNonQuery();
            }
        }
        public void UpdateData(string Code, string Type, string Name, string Cause, string Action, string Level, string Note)
        {
            using (var connection = OpenConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = @"
                    UPDATE Register SET 
                        AlarmType = @AlarmType,
                        AlarmName = @AlarmName,
                        AlarmDescription = @AlarmDescription,
                        AlarmSolveDescription = @AlarmSolveDescription,
                        AlarmLevel = @AlarmLevel,
                        AlarmNote = @AlarmNote
                    WHERE AlarmCode = @AlarmCode;
                    ";

                // 파라미터 설정
                command.Parameters.AddWithValue("@AlarmCode", Code);
                command.Parameters.AddWithValue("@AlarmType", Type);
                command.Parameters.AddWithValue("@AlarmName", Name);
                command.Parameters.AddWithValue("@AlarmDescription", Cause);
                command.Parameters.AddWithValue("@AlarmSolveDescription", Action);
                command.Parameters.AddWithValue("@AlarmLevel", Level);
                command.Parameters.AddWithValue("@AlarmNote", Note);

                command.ExecuteNonQuery();

            }
        }

        public void DeleteData(string Code)
        {
            using (var connection = OpenConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = @"DELETE FROM Register WHERE AlarmCode = @AlarmCode;";

                // 파라미터 설정
                command.Parameters.AddWithValue("@AlarmCode", Code);

                command.ExecuteNonQuery();


            }
        }
        public List<string>[] SELECT_AlarmWeekCount(string AlarmCode)
        {
            // 해당 Alarm이 1주일 간 얼마나 발생했는지 개수를 가져온다. Alarm 화면 표시에 사용
            List<string>[] select = new List<string>[2];
            using (var connection = OpenConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT DateTime FROM History WHERE AlarmCode = @AlarmCode AND DateTime >= datetime('now', '-7 days')";
                command.Parameters.AddWithValue("@AlarmCode", AlarmCode);

                using (var reader = command.ExecuteReader())
                {
                    for (int i = 0; i < select.Length; i++)
                    {
                        select[i] = new List<string>();
                    }

                    while (reader.Read())
                    {
                        select[0].Add(reader.GetString(0));
                    }
                }
                connection.Close();
            }

            return select;
        }


        public void HisInsertData(string Code, string OcucurrenceTime)
        {
            using (var connection = OpenConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = @"INSERT INTO History (AlarmCode,DateTime) VALUES (@AlarmCode,@DateTime)";

                // 파라미터 설정
                command.Parameters.AddWithValue("@AlarmCode", Code);
                command.Parameters.AddWithValue("@DateTime", OcucurrenceTime);
                command.ExecuteNonQuery();
            }
        }

        // 데이터 조회
        public List<string>[] Register_ReadData()
        {
            List<string>[] select = new List<string>[8];
            using (var connection = OpenConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Register ORDER BY AlarmCode ASC";

                using (var reader = command.ExecuteReader())
                {
                    for (int i = 0; i < select.Length; i++)
                    {
                        select[i] = new List<string>();
                    }

                    while (reader.Read())
                    {
                        select[0].Add(reader.GetInt32(0).ToString());
                        select[1].Add(reader.GetString(1));
                        select[2].Add(reader.GetString(2));
                        select[3].Add(reader.GetString(3));
                        select[4].Add(reader.GetString(4));
                        select[5].Add(reader.GetString(5));
                        select[6].Add(reader.GetString(6));
                        select[7].Add(reader.GetString(7));
                    }
                }
                connection.Close();
            }

            return select;
        }
        public List<string>[] Register_AlarmTest(object Code)
        {
            List<string>[] select = new List<string>[8];
            using (var connection = OpenConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Register WHERE AlarmCode = @AlarmCode ORDER BY AlarmCode ASC";
                command.Parameters.AddWithValue("@AlarmCode", Code);

                using (var reader = command.ExecuteReader())
                {
                    for (int i = 0; i < select.Length; i++)
                    {
                        select[i] = new List<string>();
                    }

                    while (reader.Read())
                    {
                        select[0].Add(reader.GetInt32(0).ToString());
                        select[1].Add(reader.GetString(1));
                        select[2].Add(reader.GetString(2));
                        select[3].Add(reader.GetString(3));
                        select[4].Add(reader.GetString(4));
                        select[5].Add(reader.GetString(5));
                        select[6].Add(reader.GetString(6));
                        select[7].Add(reader.GetString(7));
                    }
                }
                connection.Close();
            }

            return select;
        }

        public bool CheckRegisterDup(string AlarmCode)
        {
            int select = 0;
            using (var connection = OpenConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM Register WHERE AlarmCode = @AlarmCode ORDER BY AlarmCode ASC";
                command.Parameters.AddWithValue("@AlarmCode", AlarmCode);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        select = reader.GetInt32(0);
                }
                connection.Close();
            }

            // 중복 코드가 존재하면 true : 없으면 false
            return select > 0 ? true : false;
        }



        // 데이터 조회
        public List<string>[] History_ReadData()
        {
            List<string>[] select = new List<string>[9]; // 열 개수에 맞춰 배열 크기 설정
            using (var connection = OpenConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT h.Id, h.AlarmCode, r.AlarmType, r.AlarmName, r.AlarmDescription, r.AlarmSolveDescription, r.AlarmLevel, r.AlarmNote, h.DateTime FROM History h INNER JOIN Register r ON h.AlarmCode = r.AlarmCode  ORDER BY h.DateTime DESC;";


                using (var reader = command.ExecuteReader())
                {
                    for (int i = 0; i < select.Length; i++)
                    {
                        select[i] = new List<string>();
                    }

                    while (reader.Read())
                    {
                        select[0].Add(reader.GetInt32(0).ToString());
                        select[1].Add(reader.GetInt32(1).ToString());
                        select[2].Add(reader.GetString(2));
                        select[3].Add(reader.GetString(3));
                        select[4].Add(reader.GetString(4));
                        select[5].Add(reader.GetString(5));
                        select[6].Add(reader.GetString(6));
                        select[7].Add(reader.GetString(7));
                        select[8].Add(reader.GetString(8));
                    }
                }
                connection.Close();
            }
            return select;
        }
        public List<string>[] History_SearchData(string AlarmCode, string AlarmType, string AlarmDescription, string AlarmLevel, string AlarmName, string AlarmNote, string AlarmSolveDescription, DateTime? AlarmStartDateTime, DateTime? AlarmEndDateTime)
        //public List<string>[] History_SearchData(string code, string type, string alarmName, string cause, string action, string level, string note, DateTime? startDateTime, DateTime? endDateTime)
        {
            List<string>[] select = new List<string>[9]; // 열 개수에 맞춰 배열 크기 설정

            using (var connection = OpenConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    // 기본 쿼리, 한 줄로 작성
                    string query = "SELECT h.Id, h.AlarmCode, r.AlarmType, r.AlarmName, r.AlarmDescription, r.AlarmSolveDescription, r.AlarmLevel, r.AlarmNote, h.DateTime AS HistoryDateTime FROM History h INNER JOIN Register r ON h.AlarmCode = r.AlarmCode WHERE 1=1";

                    //동적으로 조건 추가
                    if (!string.IsNullOrEmpty(AlarmCode))
                        query += " AND h.AlarmCode = @AlarmCode";
                    if (!string.IsNullOrEmpty(AlarmType))
                        query += " AND r.AlarmType = @AlarmType";
                    if (!string.IsNullOrEmpty(AlarmName))
                        query += " AND r.AlarmName = @AlarmName";
                    if (!string.IsNullOrEmpty(AlarmDescription))
                        query += " AND r.AlarmDescription = @AlarmDescription";
                    if (!string.IsNullOrEmpty(AlarmSolveDescription))
                        query += " AND r.AlarmSolveDescription = @AlarmSolveDescription";
                    if (!string.IsNullOrEmpty(AlarmLevel))
                        query += " AND r.AlarmLevel = @AlarmLevel";
                    if (!string.IsNullOrEmpty(AlarmNote))
                        query += " AND r.AlarmNote = @AlarmNote";
                    if (AlarmStartDateTime.HasValue && AlarmEndDateTime.HasValue)
                        query += " AND h.DateTime BETWEEN @StartDateTime AND @EndDateTime";
                    else if (AlarmStartDateTime.HasValue)
                        query += " AND h.DateTime >= @StartDateTime";
                    else if (AlarmEndDateTime.HasValue)
                        query += " AND h.DateTime <= @EndDateTime";

                    query += (" ORDER BY h.DateTime DESC");
                    command.CommandText = query;

                    //매개변수 설정
                    if (!string.IsNullOrEmpty(AlarmCode))
                        command.Parameters.AddWithValue("@AlarmCode", AlarmCode);
                    if (!string.IsNullOrEmpty(AlarmType))
                        command.Parameters.AddWithValue("@AlarmType", AlarmType);
                    if (!string.IsNullOrEmpty(AlarmName))
                        command.Parameters.AddWithValue("@AlarmName", AlarmName);
                    if (!string.IsNullOrEmpty(AlarmDescription))
                        command.Parameters.AddWithValue("@AlarmDescription", AlarmDescription);
                    if (!string.IsNullOrEmpty(AlarmSolveDescription))
                        command.Parameters.AddWithValue("@AlarmSolveDescription", AlarmSolveDescription);
                    if (!string.IsNullOrEmpty(AlarmLevel))
                        command.Parameters.AddWithValue("@AlarmLevel", AlarmLevel);
                    if (!string.IsNullOrEmpty(AlarmNote))
                        command.Parameters.AddWithValue("@AlarmNote", AlarmNote);

                    //날짜가 유효하면 매개변수 추가
                    if (AlarmStartDateTime.HasValue && AlarmStartDateTime.Value != DateTime.MinValue)
                        command.Parameters.AddWithValue("@StartDateTime", AlarmStartDateTime.Value.ToString("yyyy-MM-dd"));
                    if (AlarmEndDateTime.HasValue && AlarmEndDateTime.Value != DateTime.MinValue)
                        command.Parameters.AddWithValue("@EndDateTime", AlarmEndDateTime.Value.ToString("yyyy-MM-dd"));


                    using (var reader = command.ExecuteReader())
                    {
                        for (int i = 0; i < select.Length; i++)
                        {
                            select[i] = new List<string>();
                        }

                        while (reader.Read())
                        {
                            select[0].Add(reader.GetInt32(0).ToString());
                            select[1].Add(reader.GetInt32(1).ToString());
                            select[2].Add(reader.GetString(2));
                            select[3].Add(reader.GetString(3));
                            select[4].Add(reader.GetString(4));
                            select[5].Add(reader.GetString(5));
                            select[6].Add(reader.GetString(6));
                            select[7].Add(reader.GetString(7));
                            select[8].Add(reader.GetString(8));
                        }
                    }
                }
                connection.Close();
            }

            return select;
        }
    }
}
