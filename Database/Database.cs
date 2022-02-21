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
        private const string DbPath = "moods.db";
        public DataTable data = new DataTable();

        public Database()
        {
            string stringCommand = "";

            if (!File.Exists(DbPath))
            {
                SQLiteConnection.CreateFile(DbPath);
                stringCommand = @"CREATE TABLE [moods] (
                      [id] integer NOT NULL PRIMARY KEY,
                      [date] char(10) NOT NULL,
                      [mood] int NOT NULL,
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

            Read();
        }

        public void Write(Day day)
        {
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", DbPath));
            connection.Open();

            SQLiteCommand command = new SQLiteCommand(connection);
            command = new SQLiteCommand(connection)
            {
                CommandText = "INSERT OR REPLACE INTO [moods] ([id], [date], [mood], [note]) VALUES (" + day.id + ", '" + day.date + "', " + day.mood + ", '" + day.note + "')"
            };
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

        public void Read()
        {
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", DbPath));
            connection.Open();

            SQLiteDataAdapter adapter;

            SQLiteCommand command = new SQLiteCommand(connection);
            command = new SQLiteCommand(connection)
            {
                CommandText = "SELECT * FROM [moods]"
            };

            adapter = new SQLiteDataAdapter(command);
            adapter.Fill(data);

            connection.Close();

            Debug.WriteLine("--------------------------------------------------------------------------------");
            //Debug.WriteLine(data.Rows[0]["id"]);
            foreach (DataRow row in data.Rows)
            {
                var str = "";
                foreach (DataColumn col in data.Columns)
                    str += row[col].ToString() + " ";
                Debug.WriteLine(str);
            }
        }
    }
}
