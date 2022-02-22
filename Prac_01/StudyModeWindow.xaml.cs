using Prac_class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Prac_01
{
    /// <summary>
    /// Логика взаимодействия для StudyModeWindow.xaml
    /// </summary>
    /// 

    public partial class StudyModeWindow : Window
    {
        public static List<string> data_list = new List<string>();
        static int att = 1; //спроба
        static int g;
        public StudyModeWindow()
        {
            InitializeComponent();
        }
        static double inter = 0;

        private void CloseStudyMode_Click(object sender, RoutedEventArgs e)
        {
            global.List_of_Avar_M_i.Clear();
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }
        private void InputField_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (inter == 0)
            {
                inter = Convert.ToDouble(DateTime.Now.Ticks / 10000) / 1000;
            }
            else
            {
                global.y.Add(Math.Round(Convert.ToDouble(DateTime.Now.Ticks / 10000) / 1000 - inter, 5));
                inter = Convert.ToDouble(DateTime.Now.Ticks / 10000) / 1000;
            }
            if (InputField.Text == global.CodeWord)
            {
                inter = 0;
                global.y.Clear();
            }


        }
        private void InputField_PreviewKeyUp(object sender, KeyEventArgs k)
        {
            string current = " ";
            current = InputField.Text;
            SymbolCount.Content = InputField.Text.Length;
            g = Convert.ToInt32(CountProtection.Text);
            if (g >= att)
            {
                if (current.Length != 0)
                {
                    if (current[current.Length - 1] == global.CodeWord[current.Length - 1])
                    {

                        if (InputField.Text == global.CodeWord)
                        {
                            string data = "";
                            for (int i = 0; i < global.y.Count; i++)
                            {
                                data += global.y[i] + " ";
                            }
                            data_list.Add(data);
                            att++;
                            MessageBox.Show("Спроба № " + att);
                            InputField.Text = "";
                            global.intervals.Clear();
                            for (int i = 0; i < global.y.Count; i++)
                            {
                                global.tmplist = new List<double>();
                                for (int j = 0; j < global.y.Count; j++)
                                {
                                    if (i != j)
                                    {
                                        global.tmplist.Add(global.y[j]);
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                global.intervals.Add(global.tmplist);
                            }
                            for (int i = 0; i < global.y.Count; i++)
                            {
                                for (int j = 0; j < global.y.Count - 1; j++)
                                {
                                    global.M += global.intervals[i][j] / (global.n - 1);
                                    global.Dx += (global.intervals[i][j] - global.M) / (global.n - 2);
                                }
                                global.List_of_M.Add(global.M);
                                global.List_of_D.Add(global.Dx);
                                global.temp_stu = Math.Abs((global.y[i] - global.List_of_M[i]) / (Math.Sqrt(global.Dx / (global.n - 1))));
                                global.M = 0;
                                global.Dx = 0;
                                inter = 0;
                                if (global.temp_stu >= global.tT)
                                {
                                    MessageBox.Show("незначущий елемент");
                                    att = 1;
                                    InputField.Text = "";
                                    global.List_of_Avar_M_e.Clear();
                                    global.List_of_Avar_D_e.Clear();
                                    global.global_intervals.Clear();
                                    break;

                                }
                            }
                            Find_Avar_M();
                            Find_Avar_Disp();
                            global.List_of_M.Clear();
                            global.List_of_D.Clear();
                            global.y.Clear();
                        }


                    }
                    else
                    {
                        FileStream fs = File.Open(@"C:\Users\User\source\repos\Prac_01\stats.txt", FileMode.Open, FileAccess.ReadWrite);
                        MessageBox.Show("Помилка у кодовому слові!");
                        InputField.Text = "";
                        att = 1;
                        fs.SetLength(0);
                        global.y.Clear();
                        global.List_of_Avar_D_e.Clear();
                        global.List_of_Avar_M_e.Clear();
                        global.global_intervals.Clear();
                        fs.Close();
                        inter = 0;
                    }
                }

            }
            else
            {
                string message = "";
                double m = 0;
                double s = 0;
                for (int i = 0; i < global.List_of_Avar_M_e.Count; i++)
                {
                    m = global.List_of_Avar_M_e[i];
                    s = global.List_of_Avar_D_e[i];
                    message += m + "\n";
                }
                
                MessageBox.Show(message, "Еталонні значення");
                att = 1;
                global.List_of_Avar_D_e.Clear();
                global.List_of_Avar_M_e.Clear();
                global.y.Clear();
                global.sw = new StreamWriter(@"C:\Users\User\source\repos\Prac_01\stats.txt", false); //запис еталонних даних
                foreach (string data in data_list)
                {
                    global.sw.WriteLine(data);
                }
                global.sw.Close();
            }
        }
        public void Find_Avar_M() // пошук серенього мат сподівання
        {
            for (int p = 0; p < global.List_of_M.Count; p++)
            {
                global.Avarage_M += global.List_of_M[p];
            }
            global.Avarage_M = global.Avarage_M / global.List_of_M.Count;
            global.List_of_Avar_M_e.Add(global.Avarage_M);
            global.Avarage_M = 0;
        }
        public void Find_Avar_Disp()// пошук сереньої дисперсії
        {
            for (int p = 0; p < global.List_of_D.Count; p++)
            {
                global.Avarage_D += global.List_of_D[p];
            }
            global.Avarage_D = global.Avarage_D / global.List_of_D.Count;
            global.List_of_Avar_D_e.Add(global.Avarage_D);
            global.Avarage_D = 0;
        }
        public void CountProtection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InputField.Text = "";
            att = 1;

        }
    }
}
