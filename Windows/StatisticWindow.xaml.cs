using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для StatisticWindow.xaml
    /// </summary>
    public partial class StatisticWindow : Window
    {
        public StatisticWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            DrawGraphic();
        }

        private void DrawGraphic()
        {
            Random rand = new Random();
            for (int i = 1; i < 11; i++)
            {
                if(i == 10)
                {
                    ScrollLine.X1 = 128 + (i * 96);
                    ScrollLine.X2 = 128 + (i * 96);
                    break;
                }

                var line = new Line
                {
                    X1 = 128 + ((i - 1) * 96),
                    X2 = 128 + (i * 96),
                    Y1 = 256 + rand.Next(32),
                    Y2 = 256 + rand.Next(32),
                    Stroke = Brushes.Black
                };
                GraphicBox.Children.Add(line);
            }
        }
    }
}
