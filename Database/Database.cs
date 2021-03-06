using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;

namespace MoodTracker.Database
{
    internal class Database
    {
        public readonly string DbPath = (AppDomain.CurrentDomain.BaseDirectory + "moods.db").Replace(@"\", @"/");

        public List<Day> data = new List<Day>();
        public Day currentDay = null;

        public enum ReadType { All, Month, Day, Next, Previous}

        public Database()
        {
            string stringCommand = "";

            if (!File.Exists(DbPath))
            {
                SQLiteConnection.CreateFile(DbPath);
                stringCommand = @"CREATE TABLE [moods] (
                      [id] integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                      [date] stringdate NOT NULL,
                      [mood] integer NOT NULL,
                      [note] char(100));";
            }

            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", DbPath));
            connection.Open();

            SQLiteCommand command = new SQLiteCommand(connection);

            if (stringCommand != "")
            {
                command.CommandText = stringCommand;
                command.ExecuteNonQuery();
            }

            connection.Close();

            /*var rnd = new Random();
            for (int i = 1; i < 29; i++)
            {
                string day = i.ToString();
                if (day.Length == 1)
                    day = "0" + i;
                Write(new Day("2022-01-" + day, rnd.Next(5), "test record"));
            }*/

            Debug.WriteLine("INIT DB");
            Read(App.selectedDate, ReadType.Month);
        }

        public void Write(Day day)
        {
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", DbPath));
            connection.Open();

            SQLiteCommand command = new SQLiteCommand(connection);
            command = new SQLiteCommand(connection);

            command.CommandText = "SELECT * FROM [moods] WHERE [date] = '" + day.date + "'";
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                var id = reader["id"];
                /*var date = reader["date"];
                var mood = reader["mood"];
                var note = reader["note"];*/
                reader.Close();
                command.CommandText = "UPDATE [moods] SET [mood] = " + day.mood + ", [note] = '" + day.note + "' WHERE id = " + id;
            }
            else
            {
                reader.Close();
                command.CommandText = "INSERT OR REPLACE INTO [moods] ([date], [mood], [note]) VALUES ('" + day.date + "', " + day.mood + ", '" + day.note + "')";
            }
            command.ExecuteNonQuery();
            connection.Close();
        }

        public bool Read(string selectedDate = null, ReadType readType = ReadType.All, bool check = false)
        {
            /*if (!check)
                Debug.WriteLine("--------------------------------------------------------------------------------");*/

            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", DbPath));
            connection.Open();

            SQLiteCommand command = new SQLiteCommand(connection);

            command = new SQLiteCommand(connection);

            switch (readType)
            {
                case ReadType.All:
                    ReadAll(command);
                    break;
                case ReadType.Month:
                    ReadMonth(command, selectedDate);
                    break;
                case ReadType.Day:
                    break;
                case ReadType.Next:
                case ReadType.Previous:
                    App.selectedDate = ReadNext(command, selectedDate, readType);
                    break;
            }

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (check)
                {
                    App.selectedDate = selectedDate;
                    return reader.HasRows;
                }
                else if (reader.HasRows)
                {
                    data.Clear();
                    while (reader.Read())
                    {
                        var id = (int)(long)reader["id"];
                        var date = (string)reader["date"];
                        var mood = (int)(long)reader["mood"];
                        var note = (string)reader["note"];

                        var stringData = string.Format("{0} {1} {2} {3}", id, date, mood, note);

                        //Debug.WriteLine(stringData);

                        var day = new Day(date, mood, note, id);

                        if (App.selectedDate == (string)reader["date"])
                            currentDay = day;

                        data.Add(day);
                    }
                }
            }

            connection.Close();

            return data.Count != 0;
        }

        private void ReadAll(SQLiteCommand command)
        {
            command.CommandText = "SELECT * FROM [moods]";
        }

        private void ReadMonth(SQLiteCommand command, string selectedDate)
        {
            var ym = selectedDate.Split('-')[0] + "-" + selectedDate.Split('-')[1];
            command.CommandText = "SELECT * FROM [moods] WHERE ([date] BETWEEN '" + ym + "-01" + "' AND '" + ym + "-31" + "')";
        }

        private string ReadNext(SQLiteCommand command, string selectedDate, ReadType readType)
        {
            var ym = selectedDate.Split('-')[0] + "-" + selectedDate.Split('-')[1];

            if (readType == ReadType.Next)
                command.CommandText = "SELECT * FROM [moods] WHERE ([date] > '" + ym + "-31'" + ")";
            else
                command.CommandText = "SELECT * FROM [moods] WHERE ([date] < '" + ym + "-01'" + ")";

            bool findNextDate = false;
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    selectedDate = (string)reader["date"];
                    ym = selectedDate.Split('-')[0] + "-" + selectedDate.Split('-')[1];
                    findNextDate = true;
                }
            }

            if (findNextDate)
                command.CommandText = "SELECT * FROM [moods] WHERE ([date] BETWEEN '" + ym + "-01" + "' AND '" + ym + "-31" + "')";
            return selectedDate;
        }

        public string ToDateSQLite(string date)
        {
            var split = date.Split('.');
            return split[2] + "-" + split[1] + "-" + split[0];
        }
    }
}
