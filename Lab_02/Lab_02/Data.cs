using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using static System.Console;

namespace Lab_02
{
    class Data
    {
        static StreamWriter sw;
        static StreamReader rd;
        static List<string> stu = new List<string>();
        Window wn = new Window();
        TextBox[] tx = new TextBox[5];
        Button[] ArrBtn = new Button[3];
        TextBox del_tx = new TextBox();
        Label res = new Label();

        public Data()
        {
            int m = 5;
            int n = 5;
            wn.Title = "Зчитування даних";
            wn.ResizeMode = ResizeMode.NoResize;
            wn.Height = 700;
            wn.Width = 700;
            wn.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF1A191B");

            Grid mygrid = new Grid();
            RowDefinition[] rows = new RowDefinition[m];
            ColumnDefinition[] cols = new ColumnDefinition[n];
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

            for (int i = 0; i < ArrBtn.Length; i++)
            {
                ArrBtn[i] = new Button();
                ArrBtn[i].FontSize = 20;
                ArrBtn[i].Width = 100;
                ArrBtn[i].Height = 50;
                mygrid.Children.Add(ArrBtn[i]);
            } 

            ArrBtn[0].Content = "Назад";//exit btn
            ArrBtn[0].Click += Back_Click;
            Grid.SetRow(ArrBtn[0], m - 1);
            Grid.SetColumn(ArrBtn[0], n - 1);

            ArrBtn[1].Content = "Надіслати";//send btn
            ArrBtn[1].Click += SendBut_Click;
            ArrBtn[1].VerticalAlignment = VerticalAlignment.Bottom;
            ArrBtn[1].Margin = new Thickness(0, 50, 0, 0);
            ArrBtn[1].Height = 35;
            Grid.SetRow(ArrBtn[1], m - 2);
            Grid.SetColumn(ArrBtn[1], 0);

            ArrBtn[2].Content = "Видалити";//del btn
            ArrBtn[2].Click += DelBut_Click;
            ArrBtn[2].HorizontalAlignment = HorizontalAlignment.Center;
            ArrBtn[2].VerticalAlignment = VerticalAlignment.Center;
            ArrBtn[2].Margin = new Thickness(0, 10, 40, 0);
            ArrBtn[2].Height = 35;
            Grid.SetRow(ArrBtn[2], 1);
            Grid.SetRowSpan(ArrBtn[2], 2);
            Grid.SetColumn(ArrBtn[2], 2);
            Grid.SetColumnSpan(ArrBtn[2], 2);

            Label tit = new Label();
            tit.Content = "Зчитування данних про студентів";
            tit.VerticalAlignment = VerticalAlignment.Center;
            tit.HorizontalAlignment = HorizontalAlignment.Center;
            tit.FontSize = 30;
            tit.Foreground = Brushes.White;
            tit.FontFamily = new FontFamily("Bahnschrift");
            tit.FontWeight = FontWeights.Bold;
            Grid.SetRow(tit, 0);
            Grid.SetColumn(tit, 0);
            Grid.SetColumnSpan(tit, 5);
            mygrid.Children.Add(tit);

            Label enter_data = new Label();
            enter_data.Content = "Введіть дані";
            enter_data.FontSize = 24;
            enter_data.Foreground = Brushes.White;
            enter_data.FontFamily = new FontFamily("Bahnschrift");
            enter_data.Margin = new Thickness(10, 0, 0, 0);
            Grid.SetRow(enter_data, 1);
            Grid.SetColumn(enter_data, 0);
            Grid.SetColumnSpan(enter_data, 2);
            mygrid.Children.Add(enter_data);

            Label del_data = new Label();
            del_data.Content = "Видалити студента з бази";
            del_data.FontSize = 24;
            del_data.Foreground = Brushes.White;
            del_data.FontFamily = new FontFamily("Bahnschrift");
            del_data.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(del_data, 1);
            Grid.SetColumn(del_data, 2);
            Grid.SetColumnSpan(del_data, 3);
            mygrid.Children.Add(del_data);

            res.Content = "";
            res.FontSize = 24;
            res.Foreground = Brushes.White;
            res.FontFamily = new FontFamily("Bahnschrift");
            res.HorizontalAlignment = HorizontalAlignment.Center;
            res.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(res, 2);
            Grid.SetColumn(res, 2);
            Grid.SetColumnSpan(res, 3);
            mygrid.Children.Add(res);

            int margin = 60;   
            for (int i = 0; i < tx.Length; i++)
            {
                tx[i] = new TextBox();
                tx[i].Width = 220;
                tx[i].Height = 35;
                tx[i].FontSize = 18;
                tx[i].Foreground = Brushes.Black;
                tx[i].VerticalAlignment = VerticalAlignment.Top;
                tx[i].HorizontalAlignment = HorizontalAlignment.Left;
                tx[i].Margin = new Thickness(15, margin, 0, 0);
                tx[i].GotMouseCapture += NumberStu_GotMouseCapture;
                Grid.SetColumnSpan(tx[i], 3);
                Grid.SetColumn(tx[i], 0);
                Grid.SetRowSpan(tx[i], 3);
                Grid.SetRow(tx[i], 1);
                mygrid.Children.Add(tx[i]);
                margin += 60;
            }

            tx[0].Text = "Код залікової книжки";
            tx[1].Text = "Прізвище";
            tx[2].Text = "Ім'я";
            tx[3].Text = "По-батькові";
            tx[4].Text = "Група";

            del_tx.Width = 220;
            del_tx.Text = "Код залікової книжки";
            del_tx.Height = 35;
            del_tx.FontSize = 18;
            del_tx.Foreground = Brushes.Black;
            del_tx.VerticalAlignment = VerticalAlignment.Top;
            del_tx.HorizontalAlignment = HorizontalAlignment.Center;
            del_tx.Margin = new Thickness(0, 60, 50, 0);
            del_tx.GotMouseCapture += NumberStuDel_GotMouseCapture;
            Grid.SetRow(del_tx, 1);
            Grid.SetColumn(del_tx, 2);
            Grid.SetColumnSpan(del_tx, 3);
            mygrid.Children.Add(del_tx);


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

        private void NumberStuDel_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            res.Content = "";
            del_tx.Text = "";
        }

        private void NumberStu_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TextBox t = (TextBox)e.Source;
            if (t.Text == "Код залікової книжки" || t.Text == "Прізвище" ||
                t.Text == "Ім'я" || t.Text == "По-батькові" || t.Text == "Група")
            {
                t.Text = "";
            }

        }

        private void SendBut_Click(object sender, RoutedEventArgs s)
        {
            try
            {
                sw = new StreamWriter(@"D:\vs\projects\Lab_02\Lab_02\bin\Debug\database.txt", true);


            }
            catch (Exception e)
            {
                WriteLine(e.Message);
            }
            string code = tx[0].Text;
            string lstName = tx[1].Text;
            string fstName = tx[2].Text;
            string mdlName = tx[3].Text;
            string group = tx[4].Text;
            string student = code + " " + lstName
                + " " + fstName + " " + mdlName + " " + group;
            stu.Add(student);
            sw.WriteLine(student);
            sw.Close();

        }

        private void DelBut_Click(object sender, RoutedEventArgs s)
        {
            try
            {
                rd = new StreamReader(@"D:\vs\projects\Lab_02\Lab_02\bin\Debug\database.txt");
            }
            catch (Exception e)
            {

                WriteLine(e.Message);
            }
            int i = 0;
            string[] Lines = File.ReadAllLines(@"D:\vs\projects\Lab_02\Lab_02\bin\Debug\database.txt");
            string code = del_tx.Text;

            while (!rd.EndOfStream)
            {

                string[] Line = rd.ReadLine().Split(' ');
                if (code == Line[0])
                {
                    Lines[i] = "";
                    res.Content = "Cтудента було видалено!";

                }
                i++;
            }
            for (int k = 0; k < Lines.Length - 1; k++)
            {
                if (Lines[k] == "")
                {
                    Lines[k] = Lines[k + 1];
                    Lines[k + 1] = "";

                }
            }
            rd.Close();
            StreamWriter sr = new StreamWriter(@"D:\vs\projects\Lab_02\Lab_02\bin\Debug\database.txt", false);
            for (int j = 0; j < Lines.Length; j++)
            {
                sr.WriteLine(Lines[j]);
            }
            sr.Close();

        }

    }
}
