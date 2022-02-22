using System;
using System.Windows;
using MoodTracker.Windows;

namespace MoodTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static Database.Database database;
        internal static string selectedDate = DateTime.Now.ToString("yyyy-MM-dd");

        /*private static MainWindow mainWindow = new MainWindow();
        private static NewDayWindow newDayWindow = new NewDayWindow();
        private static StatisticWindow statisticWindow = new StatisticWindow();
        private static DateChoiceWindow dateChoiceWindow = new DateChoiceWindow();*/

        public App()
        {
            Startup += App_Startup;
            database = new Database.Database();
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            if(database.currentDay is null)
                WindowsController.Startup(new NewDayWindow());
            else
                WindowsController.Startup(new MainWindow());
            App.Current.MainWindow.Show();
        }
    }
}
