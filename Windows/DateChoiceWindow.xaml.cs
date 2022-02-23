using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MoodTracker.Windows
{
    /// <summary>
    /// Логика взаимодействия для DateChoiceWindow.xaml
    /// </summary>
    public partial class DateChoiceWindow : Window
    {
        public DateChoiceWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            WindowsController.BackToPreviousWindow();
        }

        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RightButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
