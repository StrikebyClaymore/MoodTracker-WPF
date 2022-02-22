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
        internal Database.Database database = new Database.Database();

        public DBTest()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var day = new Day(database.ToDateSQLite(DatePicker.Text), MoodBox.SelectedIndex, NoteText.Text);
            database.Write(day);
            database.Read();
        }

        private void ReadButton_Click(object sender, RoutedEventArgs e)
        {
            var date = database.ToDateSQLite(DatePicker.Text);
            var ymd = date.Split('-')[0] + "-" + date.Split('-')[1];
            Debug.WriteLine(ymd);
            database.Read(ymd);
        }
    }
}
