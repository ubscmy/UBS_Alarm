using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBIOCClass.ViewModels
{
    public class SQLConn
    {
        private const string connectionString = "Data Source=Alarm.db;";

        private SqliteConnection OpenConnection()
        {
            var connection = new SqliteConnection(connectionString);
            connection.Open();
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
                INSERT INTO Register (AlarmCode, AlarmType, AlarmName, AlarmDescription, AlarmSolveDescription, AlarmLevel,AlarmNote)
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

        public void HisInsertData(string Code, string OcucurrenceTime)
        {
            using (var connection = OpenConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = @"
            INSERT INTO History (AlarmCode,DateTime)
            VALUES (@AlarmCode,@DateTime)
        ";

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
                command.CommandText = "SELECT * FROM Register";

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
                command.CommandText = "SELECT COUNT(*) FROM Register WHERE AlarmCode = @AlarmCode";
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
                command.CommandText = "SELECT h.Id, h.AlarmCode, r.AlarmType, r.AlarmName, r.AlarmDescription, r.AlarmSolveDescription, r.AlarmLevel, r.AlarmNote, h.DateTime FROM History h INNER JOIN Register r ON h.AlarmCode = r.AlarmCode;";


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
        public List<string>[] History_SearchData(string code, string type, string alarmName, string cause, string action, string level, string note, DateTime? startDateTime, DateTime? endDateTime)
        {
            List<string>[] select = new List<string>[9]; // 열 개수에 맞춰 배열 크기 설정

            using (var connection = OpenConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    // 기본 쿼리, 한 줄로 작성
                    string query = "SELECT h.Id, h.AlarmCode, r.AlarmType, r.AlarmName, r.AlarmDescription, r.AlarmSolveDescription, r.AlarmLevel, AlarmNote, h.DateTime AS HistoryDateTime FROM History h INNER JOIN Register r ON h.AlarmCode = r.AlarmCode WHERE 1=1";

                    // 동적으로 조건 추가
                    if (!string.IsNullOrEmpty(code))
                        query += " AND h.AlarmCode = @AlarmCode";
                    if (!string.IsNullOrEmpty(type))
                        query += " AND r.AlarmType = @AlarmType";
                    if (!string.IsNullOrEmpty(alarmName))
                        query += " AND r.AlarmName = @AlarmName";
                    if (!string.IsNullOrEmpty(cause))
                        query += " AND r.AlarmDescription = @AlarmDescription";
                    if (!string.IsNullOrEmpty(action))
                        query += " AND r.AlarmSolveDescription = @AlarmSolveDescription";
                    if (!string.IsNullOrEmpty(level))
                        query += " AND r.AlarmLevel = @AlarmLevel";
                    if (!string.IsNullOrEmpty(note))
                        query += " AND r.AlarmNote = @AlarmNote";

                    command.CommandText = query;

                    // 매개변수 설정
                    if (!string.IsNullOrEmpty(code))
                        command.Parameters.AddWithValue("@AlarmCode", code);
                    if (!string.IsNullOrEmpty(type))
                        command.Parameters.AddWithValue("@AlarmType", type);
                    if (!string.IsNullOrEmpty(alarmName))
                        command.Parameters.AddWithValue("@AlarmName", alarmName);
                    if (!string.IsNullOrEmpty(cause))
                        command.Parameters.AddWithValue("@AlarmDescription", cause);
                    if (!string.IsNullOrEmpty(action))
                        command.Parameters.AddWithValue("@AlarmSolveDescription", action);
                    if (!string.IsNullOrEmpty(level))
                        command.Parameters.AddWithValue("@AlarmLevel", level);
                    if (!string.IsNullOrEmpty(note))
                        command.Parameters.AddWithValue("@AlarmNote", note);

                    // 날짜가 유효하면 매개변수 추가
                    if (startDateTime.HasValue && startDateTime.Value != DateTime.MinValue)
                        command.Parameters.AddWithValue("@StartDateTime", startDateTime.Value.ToString("yyyy-MM-dd"));
                    if (endDateTime.HasValue && endDateTime.Value != DateTime.MinValue)
                        command.Parameters.AddWithValue("@EndDateTime", endDateTime.Value.ToString("yyyy-MM-dd"));

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
