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
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!App.database.Read(App.selectedDate, Database.Database.ReadType.Next, true))
                NextButton.IsEnabled = false;
            if (!App.database.Read(App.selectedDate, Database.Database.ReadType.Previous, true))
                PreviousButton.IsEnabled = false;
            FillDates();
        }

        private void FillDates()
        {
            string[] date = App.selectedDate.Split('-');
            List<int> activeDays = new List<int>();

            DaysGrid.Children.Clear();

            for (int i = 0; i < App.database.data.Count; i++)
            {
                int day = int.Parse(App.database.data[i].date.Split('-')[2]);

                int column = (day - 1) % DaysGrid.ColumnDefinitions.Count;
                int row = (day - column - 1) / DaysGrid.ColumnDefinitions.Count;

                Button btn = new Button
                {
                    Content = day,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Tag = date[0] + "-" + date[1] + "-" + day
                };

                btn.Click += DateButton_Click;
                btn.SetValue(Grid.ColumnProperty, column);
                btn.SetValue(Grid.RowProperty, row);

                DaysGrid.Children.Add(btn);
            }

            int daysInMonth = DateTime.DaysInMonth(int.Parse(date[0]), int.Parse(date[1]));
            for (int i = 0; i < daysInMonth; i++)
            {
                int column = i % DaysGrid.ColumnDefinitions.Count;
                int row = (i - column) / DaysGrid.ColumnDefinitions.Count;

                if (activeDays.Contains(i))
                    continue;

                TextBlock textBlock = new TextBlock
                {
                    Text = (i + 1).ToString(),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                };

                textBlock.SetValue(Grid.ColumnProperty, column);
                textBlock.SetValue(Grid.RowProperty, row);

                DaysGrid.Children.Add(textBlock);
            }
        }

        private void CheckNext()
        {
            /*if (!App.database.Read(App.selectedDate, Database.Database.ReadType.Next, true))
                NextButton.IsEnabled = false;
            else
                NextButton.IsEnabled = true;
            if (!App.database.Read(App.selectedDate, Database.Database.ReadType.Previous, true))
                PreviousButton.IsEnabled = false;
            else
                PreviousButton.IsEnabled = true;*/
        }

        private void DateButton_Click(object sender, RoutedEventArgs e)
        {
            App.selectedDate = (string)((Button)sender).Tag;
            Debug.WriteLine(App.selectedDate);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            WindowsController.BackToPreviousWindow();
        }

        private void TopDateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            App.database.Read(App.selectedDate, Database.Database.ReadType.Next);
            if (!App.database.Read(App.selectedDate, Database.Database.ReadType.Next, true))
                NextButton.IsEnabled = false;
            if (App.database.Read(App.selectedDate, Database.Database.ReadType.Previous, true))
                PreviousButton.IsEnabled = true;
            FillDates();
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            App.database.Read(App.selectedDate, Database.Database.ReadType.Previous);
            if (!App.database.Read(App.selectedDate, Database.Database.ReadType.Previous, true))
                PreviousButton.IsEnabled = false;
            if (App.database.Read(App.selectedDate, Database.Database.ReadType.Next, true))
                NextButton.IsEnabled = true;
            FillDates();
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
