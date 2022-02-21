using System.Windows;
using MoodTracker.Windows;

namespace MoodTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /*internal static Database.Database database = new Database.Database();

        private static MainWindow mainWindow = new MainWindow();
        private static NewDayWindow newDayWindow = new NewDayWindow();
        private static StatisticWindow statisticWindow = new StatisticWindow();
        private static DateChoiceWindow dateChoiceWindow = new DateChoiceWindow();*/

        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            WindowsController.Startup(new DBTest());
            App.Current.MainWindow.Show();
        }
    }
}
