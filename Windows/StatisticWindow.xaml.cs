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
            Activated += StatisticWindow_Activated;
            Deactivated += StatisticWindow_Deactivated;
        }

        private void StatisticWindow_Activated(object sender, EventArgs e)
        {
            DrawGraphic();
        }

        private void StatisticWindow_Deactivated(object sender, EventArgs e)
        {
            Scroller.ScrollToHome();
        }

        private void DrawGraphic()
        {
            Random rand = new Random();

            GraphicBox.Children.Clear();

            var scrollLine = new Line { X1 = 640, X2 = 640, Y1 = 0, Y2 = 0, };
            GraphicBox.Children.Add(scrollLine);

            var polygon = new Polygon { Fill = new SolidColorBrush(Color.FromArgb(100, 40, 60, 240)) };
            polygon.Points.Add(new Point(128, Height));

            for (int i = 1; i < App.database.data.Count + 1; i++)
            {
                if (i == App.database.data.Count)
                {
                    polygon.Points.Add(new Point(128 + ((i - 1) * 96), Height));
                    scrollLine.X1 = 128 + (i * 96);
                    scrollLine.X2 = 128 + (i * 96);
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

                var tBlock = new TextBlock
                {
                    Text = App.database.data[i - 1].date.Split('-')[2],
                    Margin = new Thickness(line.X1 - 10, Height - 96, 0, 0),
                    FontSize = 20
                };

                polygon.Points.Add(new Point(128 + ((i - 1) * 96), p1.Y + m1.ActualHeight / 2));

                if (i == App.database.data.Count - 1)
                {
                    GraphicBox.Children.Add(new TextBlock
                    {
                        Text = App.database.data[i].date.Split('-')[2],
                        Margin = new Thickness(line.X2 - 10, Height - 96, 0, 0),
                        FontSize = 20
                    });
                    polygon.Points.Add(new Point(128 + (i * 96), p2.Y + m2.ActualHeight / 2));
                }

                GraphicBox.Children.Add(line);
                GraphicBox.Children.Add(tBlock);
            }

            GraphicBox.Children.Add(polygon);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            WindowsController.BackToPreviousWindow();
        }
    }
}
