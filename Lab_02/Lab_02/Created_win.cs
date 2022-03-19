using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lab_02
{
    class Created_win
    {
        Window wn = new Window();
        public Created_win()
        {
            Student();
        }

        private void Student()
        {
            wn.Title = "Дані про про розробника";
            wn.ResizeMode = ResizeMode.NoResize;
            wn.Height = 480;
            wn.Width = 800;
            Grid mygrid = new Grid();
            mygrid.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF110921");
            RowDefinition[] rows = new RowDefinition[3];
            ColumnDefinition[] cols = new ColumnDefinition[4];
            Label[] ArrLab = new Label[3]; //текст
            
            for (int i = 0; i < 3; i++)
            {
                rows[i] = new RowDefinition();
                mygrid.RowDefinitions.Add(rows[i]);
            }
            for (int i = 0; i < 4; i++)
            {
                cols[i] = new ColumnDefinition();
                mygrid.ColumnDefinitions.Add(cols[i]);
            }
            for (int i = 0; i < 3; i++)
            {
                    ArrLab[i] = new Label();
                    ArrLab[i].HorizontalAlignment = HorizontalAlignment.Left;
                    ArrLab[i].VerticalAlignment = VerticalAlignment.Center;
                    ArrLab[i].FontFamily = new FontFamily("Bahnschrift");
                    ArrLab[i].FontSize = 36;
                    ArrLab[i].Foreground = Brushes.White;
                    ArrLab[i].Content = "2022";
                ArrLab[i].Margin = new Thickness(28, 0, 0, 0);

            }
            ArrLab[0].Content = "Чердаклієв Кирило Євгенович";
            ArrLab[0].FontWeight = FontWeights.Bold;
            Grid.SetRow(ArrLab[0], 0);
            Grid.SetColumnSpan(ArrLab[0], 3);

            ArrLab[1].Content = "Група: КП-12";
            Grid.SetRow(ArrLab[1], 1);
            Grid.SetColumnSpan(ArrLab[1], 2);
            ArrLab[1].HorizontalAlignment = HorizontalAlignment.Left;
            ArrLab[1].VerticalAlignment = VerticalAlignment.Top;

            ArrLab[2].Content = "2022";
            Grid.SetRow(ArrLab[2], 1);
            Grid.SetColumn(ArrLab[2], 0);
            ArrLab[2].HorizontalAlignment = HorizontalAlignment.Left;

            Border[] ln = new Border[2]; //дизайн, лінії
            for (int i = 0; i < 2; i++)
            {
                ln[i] = new Border();
                ln[i].Width = 700;
                ln[i].Height = 35;
                ln[i].Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFA9D7FF");
                ln[i].HorizontalAlignment = HorizontalAlignment.Left;
                ln[i].CornerRadius = new CornerRadius(20, 20, 20, 20);
            }
            ln[1].Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFCB57A6");
            ln[0].Margin = new Thickness(-350, 80, 0, 0);
            ln[1].Margin = new Thickness(-250, 0, 0, 20);
            Grid.SetRow(ln[0], 2);
            Grid.SetColumnSpan(ln[0], 4);
            Grid.SetRow(ln[1], 2);
            Grid.SetColumnSpan(ln[1], 4);

            Button btn = new Button();
            btn.Content = "Назад";
            btn.Click += Back_Click;
            btn.Width = 100;
            btn.Height = 50;
            Grid.SetRow(btn, 2);
            Grid.SetColumn(btn, 3);

            mygrid.Children.Add(btn);
            mygrid.Children.Add(ln[0]);
            mygrid.Children.Add(ln[1]);

            for (int i = 0; i < 3; i++)
                mygrid.Children.Add(ArrLab[i]);

            wn.Content = mygrid;
            wn.Show();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            MainWindow mw = new MainWindow();
            wn.Hide();
            mw.Show();
        }
    }
}
