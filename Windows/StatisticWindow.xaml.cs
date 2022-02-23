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

            var polygon = new Polygon { Fill = new SolidColorBrush(Color.FromArgb(100, 40, 60, 240)) };
            polygon.Points.Add(new Point(128, Height));

            for (int i = 1; i < App.database.data.Count + 1; i++)
            {
                if (i == App.database.data.Count)
                {
                    polygon.Points.Add(new Point(128 + ((i - 1) * 96), Height));
                    ScrollLine.X1 = 128 + (i * 96);
                    ScrollLine.X2 = 128 + (i * 96);
                    break;
                }

                var m1 = BackgroundGrid.Children[App.database.data[i - 1].mood] as Rectangle;
                var m2 = BackgroundGrid.Children[App.database.data[i].mood] as Rectangle;

                Point p1 = m1.TransformToAncestor(Root).Transform(new Point(0, 0));
                Point p2 = m2.TransformToAncestor(Root).Transform(new Point(0, 0));

                var line = new Line
                {
                    X1 = 128 + ((i - 1) * 96),
                    X2 = 128 + (i * 96),
                    Y1 = p1.Y + m1.ActualHeight / 2,
                    Y2 = p2.Y + m2.ActualHeight / 2,
                    StrokeThickness = 3,
                    Stroke = Brushes.Black
                };

                polygon.Points.Add(new Point(128 + ((i - 1) * 96), p1.Y + m1.ActualHeight / 2));

                if (i == App.database.data.Count - 1)
                    polygon.Points.Add(new Point(128 + (i * 96), p2.Y + m2.ActualHeight / 2));

                GraphicBox.Children.Add(line);
            }

            GraphicBox.Children.Add(polygon);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            WindowsController.BackToPreviousWindow();
        }
    }
}
