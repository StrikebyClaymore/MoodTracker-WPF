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
        private const string DbPath = @"C:\Users\CGO\Documents\Code\C#\MoodTracker-WPF\resources\db\moods.db";
        //public DataTable data = new DataTable();
        public Day currentDay = null;

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

            Debug.WriteLine("INIT DB");
            Read();
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

        /*public void Update()
        {
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", DbPath));
            connection.Open();

            SQLiteCommand command = new SQLiteCommand(connection);
            command = new SQLiteCommand(connection)
            {
                CommandText = "UPDATE [moods] SET [mood] = " + 1 + " WHERE id =" + 1
            };
            command.ExecuteNonQuery();

            connection.Close();
        }*/

        public void Read(string selectedDate = null)
        {
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", DbPath));
            connection.Open();

            SQLiteDataAdapter adapter;

            SQLiteCommand command = new SQLiteCommand(connection);


            command = new SQLiteCommand(connection);

            if (selectedDate is null)
                command.CommandText = "SELECT * FROM [moods]";
            else
            {
                var ym = selectedDate.Split('-')[0] + "-" + selectedDate.Split('-')[1];
                command.CommandText = "SELECT * FROM [moods] WHERE ([date] BETWEEN '" + ym + "-01" + "' AND '" + ym + "-31" + "')";
            }
            //adapter = new SQLiteDataAdapter(command);
            //adapter.Fill(data);

            Debug.WriteLine("--------------------------------------------------------------------------------");

            //Debug.WriteLine(data.Rows[0]["id"]);
            /*foreach (DataRow row in data.Rows)
            {
                var str = "";
                foreach (DataColumn col in data.Columns)
                    str += row[col].ToString() + " ";
                Debug.WriteLine(str);
            }*/

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var id = (int)(long)reader["id"];
                        var date = (string)reader["date"];
                        var mood = (int)(long)reader["mood"];
                        var note = (string)reader["note"];

                        var data = string.Format("{0} {1} {2} {3}", id, date, mood, note);
                        Debug.WriteLine(data);

                        if (App.selectedDate == (string)reader["date"])
                            currentDay = new Day(date, mood, note, id);
                    }
                }
            }

            connection.Close();
        }

        public string ToDateSQLite(string date)
        {
            var split = date.Split('.');
            return split[2] + "-" + split[1] + "-" + split[0];
        }
    }
}
