using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using static System.Console;

namespace Lab_01_work
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        static StreamWriter sw;
        static StreamReader rd;
        static List<string> stu = new List<string>();


        public Window1()
        {
            InitializeComponent();
        }


        private void GoToMainBut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }

        private void SendBut_Click(object sender, RoutedEventArgs s)
        {
            try
            {
                sw = new StreamWriter(@"D:\учеба\Student base(lab_01).txt", true);


            }
            catch (Exception e)
            {
                WriteLine(e.Message);
            }
            string code = NumberStu.Text;
            string lstName = LstName.Text;
            string fstName = FstName.Text;
            string mdlName = MdlName.Text;
            string group = GroupStu.Text;
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
                rd = new StreamReader(@"D:\учеба\Student base(lab_01).txt");
            }
            catch (Exception e)
            {

                WriteLine(e.Message);
            }
            int i = 0;
            string[] Lines = File.ReadAllLines(@"D:\учеба\Student base(lab_01).txt");
            string code = NumberStuDel.Text;

            while (!rd.EndOfStream)
            {

                string[] Line = rd.ReadLine().Split(' ');
                if (code == Line[0])
                {
                    Lines[i] = "";
                    label_1.Content = "Cтудента було видалено!";

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
            StreamWriter sr = new StreamWriter(@"D:\учеба\Student base(lab_01).txt", false);
            for (int j = 0; j < Lines.Length; j++)
            {
                sr.WriteLine(Lines[j]);
            }
            sr.Close();

        }

        private void NumberStuDel_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label_1.Content = "";
            NumberStuDel.Text = "";
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
    }
}
