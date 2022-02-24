using System;
using System.Windows;
using System.ComponentModel;


namespace MoodTracker.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        private void StatiscticButton_Click(object sender, RoutedEventArgs e)
        {
            WindowsController.GoToWindow(this, WindowsController.statisticWindow);
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowsController.GoToWindow(this, WindowsController.dateChoiceWindow);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
