using MoodTracker.Database;
using System.Diagnostics;
using System.Windows;

namespace MoodTracker.Windows
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class DBTest : Window
    {
        public DBTest()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var day = new Day(App.database.ToDateSQLite(DatePicker.Text), MoodBox.SelectedIndex, NoteText.Text);
            App.database.Write(day);
            App.database.Read();
        }

        private void ReadButton_Click(object sender, RoutedEventArgs e)
        {
            var date = App.database.ToDateSQLite(DatePicker.Text);
            App.database.Read(date, Database.Database.ReadType.Month);
        }
    }
}
