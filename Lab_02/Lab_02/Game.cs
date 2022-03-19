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
    class Game
    {
        Window wn = new Window();
        static int m = 5;
        static int n = 6;
        Grid mygrid = new Grid();
        Label PlayStep = new Label(); //label playstep
        ComboBox[,] cm = new ComboBox[m, n]; //combobxs
        public Game()
        {
            int m = 5;
            int n = 6;
            wn.Title = "Хрестики-Нолики";
            wn.ResizeMode = ResizeMode.NoResize;
            wn.Height = 500;
            wn.Width = 800;
            wn.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF1A191B");

            
            RowDefinition[] rows = new RowDefinition[m];
            ColumnDefinition[] cols = new ColumnDefinition[n];
            mygrid.ShowGridLines = true;

            
            PlayStep.Content = "Хід";
            PlayStep.FontSize = 20;
            PlayStep.Foreground = Brushes.White;
            Grid.SetRow(PlayStep, 0);
            Grid.SetColumn(PlayStep, n - 1);

            Button Again_But = new Button(); // кнопка назад
            Again_But.Click += Again_But_Click;
            Again_But.Content = "Спочатку";
            Again_But.FontSize = 20;
            Again_But.Width = 100;
            Again_But.Height = 50;
            Grid.SetRow(Again_But, 1);
            Grid.SetColumn(Again_But, n - 1);

            Button GoToMainBut = new Button();
            GoToMainBut.Click += GoToMainBut_Click;
            GoToMainBut.Content = "Назад";
            GoToMainBut.FontSize = 20;
            GoToMainBut.Width = 100;
            GoToMainBut.Height = 50;
            Grid.SetRow(GoToMainBut, m-1);
            Grid.SetColumn(GoToMainBut, n - 1);

             //combobxs
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n-1; j++)
                {
                    cm[i, j] = new ComboBox();
                    cm[i, j].FontSize = 28;
                    cm[i, j].Width = 90;
                    cm[i, j].Height = 60;
                    cm[i, j].VerticalAlignment = VerticalAlignment.Center;
                    cm[i, j].HorizontalAlignment = HorizontalAlignment.Center;
                    cm[i, j].Items.Add("×");
                    cm[i, j].Items.Add("o");
                    cm[i, j].SelectionChanged += Combo_SelectionChanged;

                }

            for (int i = 0; i < m; i++)
            {
                rows[i] = new RowDefinition();
                mygrid.RowDefinitions.Add(rows[i]);
            }

            for (int i = 0; i < n; i++)
            {
                cols[i] = new ColumnDefinition();
                mygrid.ColumnDefinitions.Add(cols[i]);
            }

            for (int i = 0; i < m; i++)
                for (int j = 0; j < n-1; j++)
                {
                    Grid.SetRow(cm[i, j], i);
                    Grid.SetColumn(cm[i, j], j);
                }

            for (int i = 0; i < m; i++)
                for (int j = 0; j < n-1; j++)
                {
                    mygrid.Children.Add(cm[i, j]);
                }

            mygrid.Children.Add(PlayStep);
            mygrid.Children.Add(Again_But);
            mygrid.Children.Add(GoToMainBut);

        
            wn.Content = mygrid;
            wn.Show();

        }

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox b = (ComboBox)e.Source;
            b.IsEnabled = false;


            if (b.SelectedItem == null)
            {
                PlayStep.Content = "Хід:";
            }
            else if (b.SelectedItem.ToString() == "×")
            {
                PlayStep.Content = "Хід: Нолики ";
            }
            else
            {
                PlayStep.Content = "Хід: Хрестики ";
            }
            CheckWin();
        }

        private void GoToMainBut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            wn.Hide();
            mw.Show();
        }

        private void Again_But_Click(object sender, RoutedEventArgs e)
        {
            foreach (ComboBox b in this.mygrid.Children.OfType<ComboBox>())
            {
                b.SelectedIndex = -1;
                b.IsEnabled = true;
            }
        }

        public void ShowWin(ComboBox s)
        {
            MessageBoxResult result = MessageBox.Show("Переможець: " + s.SelectedItem + "\nХочете почати спочатку? ", "Результат", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    foreach (ComboBox b in this.mygrid.Children.OfType<ComboBox>())
                    {
                        b.SelectedIndex = -1;
                        b.IsEnabled = true;
                    }
                    break;
                case MessageBoxResult.No:
                    MainWindow mw = new MainWindow();
                    wn.Hide();
                    mw.Show();
                    break;
            }
        }

        public void CheckWin()
        {


            if ((cm[0, 0].SelectedItem == cm[0, 1].SelectedItem) && (cm[0, 1].SelectedItem == cm[0, 2].SelectedItem)
                && (cm[0, 2].SelectedItem == cm[0, 3].SelectedItem) && (cm[0, 3].SelectedItem == cm[0, 4].SelectedItem) && (cm[0, 0].SelectedIndex != -1))
            {
                ShowWin(cm[0, 0]);

            }
            else if ((cm[1, 0].SelectedItem == cm[1, 1].SelectedItem) && (cm[1, 1].SelectedItem == cm[1, 2].SelectedItem)
                && (cm[1, 2].SelectedItem == cm[1, 3].SelectedItem) && (cm[1, 3].SelectedItem == cm[1, 4].SelectedItem) && (cm[1, 0].SelectedIndex != -1))
            {
                ShowWin(cm[1, 0]);

            }
            else if ((cm[2, 0].SelectedItem == cm[2, 1].SelectedItem) && (cm[2, 1].SelectedItem == cm[2, 2].SelectedItem)
                && (cm[2, 2].SelectedItem == cm[2, 3].SelectedItem) && (cm[2, 3].SelectedItem == cm[2, 4].SelectedItem) && (cm[2, 0].SelectedIndex != -1))
            {
                ShowWin(cm[2, 0]);

            }
            else if ((cm[3, 0].SelectedItem == cm[3, 1].SelectedItem) && (cm[3, 1].SelectedItem == cm[3, 2].SelectedItem)
                && (cm[3, 2].SelectedItem == cm[3, 3].SelectedItem) && (cm[3, 3].SelectedItem == cm[3, 4].SelectedItem) && (cm[3, 0].SelectedIndex != -1))
            {
                ShowWin(cm[3, 0]);

            }
            else if ((cm[4, 0].SelectedItem == cm[4, 1].SelectedItem) && (cm[4, 1].SelectedItem == cm[4, 2].SelectedItem)
                && (cm[4, 2].SelectedItem == cm[4, 3].SelectedItem) && (cm[4, 3].SelectedItem == cm[4, 4].SelectedItem) && (cm[4, 0].SelectedIndex != -1))
            {
                ShowWin(cm[4, 0]);

            }
            else if ((cm[0, 0].SelectedItem == cm[1, 0].SelectedItem) && (cm[1, 0].SelectedItem == cm[2, 0].SelectedItem)
                && (cm[2, 0].SelectedItem == cm[3, 0].SelectedItem) && (cm[3, 0].SelectedItem == cm[4, 0].SelectedItem) && (cm[0, 0].SelectedIndex != -1))
            {
                ShowWin(cm[0, 0]);
            }
            else if ((cm[0, 1].SelectedItem == cm[1, 1].SelectedItem) && (cm[1, 1].SelectedItem == cm[2, 1].SelectedItem)
                && (cm[2, 1].SelectedItem == cm[3, 1].SelectedItem) && (cm[3, 1].SelectedItem == cm[4, 1].SelectedItem) && (cm[0, 1].SelectedIndex != -1))
            {
                ShowWin(cm[0, 1]);
            }
            else if ((cm[0, 2].SelectedItem == cm[1, 2].SelectedItem) && (cm[1, 2].SelectedItem == cm[2, 2].SelectedItem)
                && (cm[2, 2].SelectedItem == cm[3, 2].SelectedItem) && (cm[3, 2].SelectedItem == cm[4, 2].SelectedItem) && (cm[0, 2].SelectedIndex != -1))
            {
                ShowWin(cm[0, 2]);
            }
            else if ((cm[0, 3].SelectedItem == cm[1, 3].SelectedItem) && (cm[1, 3].SelectedItem == cm[2, 3].SelectedItem)
                && (cm[2, 3].SelectedItem == cm[3, 3].SelectedItem) && (cm[3, 3].SelectedItem == cm[4, 3].SelectedItem) && (cm[0, 3].SelectedIndex != -1))
            {
                ShowWin(cm[0, 3]);
            }
            else if ((cm[0, 4].SelectedItem == cm[1, 4].SelectedItem) && (cm[1, 4].SelectedItem == cm[2, 4].SelectedItem)
                && (cm[2, 4].SelectedItem == cm[3, 4].SelectedItem) && (cm[3, 4].SelectedItem == cm[4, 4].SelectedItem) && (cm[0, 4].SelectedIndex != -1))
            {
                ShowWin(cm[0, 4]);
            }

            else if ((cm[0, 0].SelectedItem == cm[1, 1].SelectedItem) && (cm[1, 1].SelectedItem == cm[2, 2].SelectedItem)
                && (cm[2, 2].SelectedItem == cm[3, 3].SelectedItem) && (cm[3, 3].SelectedItem == cm[4, 4].SelectedItem) && (cm[0, 0].SelectedIndex != -1))
            {
                ShowWin(cm[0, 0]);
            }
            else if ((cm[0, 4].SelectedItem == cm[1, 3].SelectedItem) && (cm[1, 3].SelectedItem == cm[2, 2].SelectedItem)
                && (cm[2, 2].SelectedItem == cm[3, 1].SelectedItem) && (cm[3, 1].SelectedItem == cm[4, 0].SelectedItem) && (cm[0, 4].SelectedIndex != -1))
            {
                ShowWin(cm[0, 4]);
            }

        }

    }
}
