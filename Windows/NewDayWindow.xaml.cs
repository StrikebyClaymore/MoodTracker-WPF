using MoodTracker.Database;
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
    /// Логика взаимодействия для NewDayWindow.xaml
    /// </summary>
    public partial class NewDayWindow : Window
    {
        private int selectedMood;
        private string selectedDate;

        public NewDayWindow()
        {
            InitializeComponent();

            selectedDate = DateTime.Now.ToString("yyyy-MM-dd");
        }

        #region ChoiceMood

        private void CloseButton_Click(object sender, RoutedEventArgs e)    
        {
            WindowsController.BackToPreviousWindow();
        }

        private void DateIcon_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Mood_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            selectedMood = int.Parse((string)button.Tag);
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            App.database.Write(new Day(selectedDate, selectedMood, NoteText.Text));
        }

        private void EditNote_Click(object sender, RoutedEventArgs e)
        {
            Mood.Visibility = Visibility.Hidden;
            Note.Visibility = Visibility.Visible;
        }

        #endregion

        #region EditNote

        private void CloseNote_Click(object sender, RoutedEventArgs e)
        {
            Note.Visibility = Visibility.Hidden;
            Mood.Visibility = Visibility.Visible;
        }

        private void NoteText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        #endregion
    }
}
