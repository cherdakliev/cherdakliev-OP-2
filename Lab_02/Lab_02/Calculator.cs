using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Data;

namespace Lab_02
{
    
    class Calculator
    {
        Window wn = new Window();
        TextBox TB = new TextBox();
        public Calculator()
        {
            int m = 7;
            int n = 4;
            int num = 1;
            wn.Title = "Калькулятор";
            wn.ResizeMode = ResizeMode.NoResize;
            wn.Height = 700;
            wn.Width = 500;
            wn.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF1A191B");

            Grid mygrid = new Grid();
            RowDefinition[] rows = new RowDefinition[m];
            ColumnDefinition[] cols = new ColumnDefinition[n];
            Button[,] ArrBtn = new Button[m, n];
            Button Back_Btn = new Button(); //exit btn

            Back_Btn.Content = "Назад";
            Back_Btn.FontSize = 48;
            Back_Btn.Click += Back_Click;
            Grid.SetRow(Back_Btn, m - 1);
            Grid.SetColumnSpan(Back_Btn, n);
            mygrid.Children.Add(Back_Btn);

            TB.TextWrapping = TextWrapping.Wrap;
            TB.Padding = new Thickness(10);
            TB.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFE4E4E4");
            TB.FontSize = 36;
            Grid.SetRow(TB, 0);
            Grid.SetColumnSpan(TB, n);

            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                {
                    ArrBtn[i, j] = new Button();
                    ArrBtn[i, j].Click += But_Click;
                    ArrBtn[i, j].FontSize = 36;
                    ArrBtn[i, j].Content = "(" + i.ToString() + "; "
                    + j.ToString() + ")";
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
                for (int j = 0; j < n; j++)
                {
                    Grid.SetRow(ArrBtn[i, j], i);
                    Grid.SetColumn(ArrBtn[i, j], j);
                }

            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                {
                    mygrid.Children.Add(ArrBtn[i, j]);
                }

            for (int i = 0; i < n; i++)
            {
                mygrid.Children.Remove(ArrBtn[0, i]);
            }
            for (int i = 0; i < n; i++)
            {
                mygrid.Children.Remove(ArrBtn[m-1, i]);
            }
            mygrid.Children.Add(TB);

            ArrBtn[1, 0].Content = "";
            ArrBtn[1, 0].IsEnabled = false;
            ArrBtn[5, 0].Content = "";
            ArrBtn[5, 0].IsEnabled = false;
            ArrBtn[5, 1].Content = "0";
            ArrBtn[5, 2].Content = ".";
            ArrBtn[1, 1].Content = "=";
            ArrBtn[1, 2].Content = "C";
            ArrBtn[1, 3].Content = "⌫";
            ArrBtn[2, 3].Content = "/";
            ArrBtn[3, 3].Content = "*";
            ArrBtn[4, 3].Content = "-";
            ArrBtn[5, 3].Content = "+";

            for (int i = m - 3; i > 1; i--)
                for (int j = 0; j < n-1; j++)
                {
                    ArrBtn[i, j].Content = num;
                    num++;
                }


            wn.Content = mygrid;
            wn.Show();
        }

        private void But_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string str = "";
            string text = ((Button)e.OriginalSource).Content.ToString();
            if (text == "C")
                TB.Text = "";
            else if (text == "=")
            {
                str = new DataTable().Compute(TB.Text, null).ToString();
                TB.Text = str;
            }
            else if (text == "⌫")
            {
                if (TB.Text != "")
                {
                    string ToDel = TB.Text;
                    ToDel = ToDel.Remove(ToDel.Length - 1);
                    TB.Text = ToDel; ;
                }
                else
                {
                    TB.Text = "";
                }

            }
            else
                TB.Text += text;

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
