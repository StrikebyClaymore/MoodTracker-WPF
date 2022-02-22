using MoodTracker.Windows;
using System.Windows;

namespace MoodTracker
{
    internal static class WindowsController
    {
        private static Window _previous;

        public static void Startup(Window startWindow)
        {
            startWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            App.Current.MainWindow = startWindow;
        }

        public static void GoToWindow(Window from, Window to)
        {
            App.Current.MainWindow.Close();
            _previous = from;
            App.Current.MainWindow = to;
            App.Current.MainWindow.Show();
        }

        public static void BackToPreviousWindow()
        {
            App.Current.MainWindow.Close();
            App.Current.MainWindow = _previous;
            _previous = null;
            App.Current.MainWindow.Show();
        }
    }
}
