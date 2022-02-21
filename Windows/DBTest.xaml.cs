using MoodTracker.Database;
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
            var day = new Day(DatePicker.Text, MoodBox.SelectedIndex, NoteText.Text);
            database.Write(day);
            database.Read();
        }
    }
}
