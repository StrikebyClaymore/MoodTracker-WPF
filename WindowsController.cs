using MoodTracker.Windows;
using System.Windows;

namespace MoodTracker
{
    internal static class WindowsController
    {
        private static Window _previous;

        public static MainWindow mainWindow = new MainWindow();
        public static NewDayWindow newDayWindow = new NewDayWindow();
        public static StatisticWindow statisticWindow = new StatisticWindow();
        public static DateChoiceWindow dateChoiceWindow = new DateChoiceWindow();

        public static void Startup(Window startWindow)
        {
            startWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            App.Current.MainWindow = startWindow;
            if (startWindow == newDayWindow)
                _previous = mainWindow;
        }

        public static void GoToWindow(Window from, Window to)
        {
            App.Current.MainWindow.Hide();
            _previous = from;
            to.Left = from.Left;
            to.Top = from.Top;
            App.Current.MainWindow = to;
            App.Current.MainWindow.Show();
        }

        public static void BackToPreviousWindow()
        {
            App.Current.MainWindow.Hide();
            _previous.Left = App.Current.MainWindow.Left;
            _previous.Top = App.Current.MainWindow.Top;
            App.Current.MainWindow = _previous;
            _previous = null;
            App.Current.MainWindow.Show();
        }

        public static void CloseWindows()
        {
            for (int i = App.Current.Windows.Count - 1; i >= 0; i--)
                App.Current.Windows[i].Close();
        }
    }
}
