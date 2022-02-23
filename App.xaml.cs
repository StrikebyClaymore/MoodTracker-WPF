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
        internal static string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
        internal static string selectedDate = DateTime.Now.ToString("yyyy-MM-dd");

        public App()
        {
            Startup += App_Startup;
            database = new Database.Database();
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            if(database.currentDay is null)
                WindowsController.Startup(WindowsController.newDayWindow);
            else
                WindowsController.Startup(WindowsController.mainWindow);

            WindowsController.Startup(new StatisticWindow());
            
            App.Current.MainWindow.Show();
            App.Current.MainWindow.WindowStartupLocation = WindowStartupLocation.Manual;
        }
    }
}
