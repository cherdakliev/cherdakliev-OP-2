using Prac_class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Prac_01
{
    /// <summary>
    /// Логика взаимодействия для ProtectionModeWindow.xaml
    /// </summary>
    public partial class ProtectionModeWindow : Window
    {
        static double inter = 0;
        static double Fr = 0;
        static int[] r;
        static int q = 0;
        static List<double> fisher_list = new List<double>();
        static int att = 1; //спроба
        static int g;
        public ProtectionModeWindow()
        {
            InitializeComponent();
            StreamReader reader = new StreamReader(@"C:\Users\User\source\repos\Prac_01\stats.txt");
            while ((!reader.EndOfStream))
            {
                string[] Line = reader.ReadLine().Split(" ");
                string[] newLine = new string[Line.Length - 1]; // Прибирання зайвого пробіла
                for (int i = 0; i < global.n - 1; i++)
                {
                    newLine[i] = Line[i];
                }
                Line = newLine;
                for (int i = 0; i < global.n - 1; i++)
                {

                    global.temp.Add(Convert.ToDouble(Line[i]));
                    if (global.temp.Count == global.n - 1)
                    {
                        global.global_intervals.Add(global.temp);
                        global.temp = new List<double>();
                    }
                }
            }
            reader.Close();
        }

        private void CloseStudyMode_Click(object sender, RoutedEventArgs e)
        {
            global.List_of_Avar_M_i.Clear();
            global.List_of_Avar_D_i.Clear();
            MainWindow mw = new MainWindow();
            Hide();
            global.List_of_Avar_M_e.Clear();
            global.List_of_Avar_D_e.Clear();
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

        private void InputField_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            SymbolCount.Content = InputField.Text.Length;
            string current = " ";
            current = InputField.Text;
            g = Convert.ToInt32(CountProtection.Text);
            if (g >= att)
            {
                if (current.Length != 0)
                {
                    if (current[current.Length - 1] == global.CodeWord[current.Length - 1])
                    {

                        if (InputField.Text == global.CodeWord)
                        {
                            att++;
                            MessageBox.Show("Спроба № " + att);
                            InputField.Text = "";
                            for (int j = 0; j < global.y.Count; j++)
                            {
                                global.M += global.y[j] / (global.n - 1);
                                global.Dx += (global.y[j] - global.M) / (global.n - 2);
                            }
                            global.List_of_Avar_M_i.Add(global.M);
                            global.List_of_Avar_D_i.Add(global.Dx);
                            global.M = 0;
                            global.Dx = 0;
                            if (global.global_intervals.Count > q)
                            {
                                for (int j = 0; j < global.y.Count; j++)
                                {
                                    global.M += global.global_intervals[q][j] / (global.n - 1);
                                    global.Dx += (global.global_intervals[q][j] - global.M) / (global.n - 2);
                                }
                                global.List_of_Avar_M_e.Add(global.M);
                                global.List_of_Avar_D_e.Add(global.Dx);
                                global.M = 0;
                                global.Dx = 0;
                            }
                            inter = 0;
                            global.y.Clear();
                            q++;

                        }
                    }
                    else
                    {
                        MessageBox.Show("Помилка у кодовому слові!");
                        InputField.Text = "";
                        att++;
                        global.y.Clear();
                    }
                }

            }
            else
            {
                string data = "";
                double m = 0;
                double s = 0;
                for (int i = 0; i < global.List_of_Avar_M_i.Count; i++)
                {
                    m = global.List_of_Avar_M_i[i];
                    s = global.List_of_Avar_D_i[i];

                }

                global.sw = new StreamWriter(@"C:\Users\User\source\repos\Prac_01\auth_res.txt", false);
                att = 1;
                InputField.IsEnabled = false;
                Fisher(global.List_of_Avar_D_e, global.List_of_Avar_D_i);
                Auntefication(global.List_of_Avar_D_e, global.List_of_Avar_D_i, global.List_of_Avar_M_e, global.List_of_Avar_M_i);
                P_12st();
                data = "Attempts: " + g + "\n" + "P indefication: " + StatisticsBlock.Content + "\n"
                        + "P_1: " + P1Field.Content + "\n" + "P_2: " + P2Field.Content;
                global.sw.WriteLine(data);
                global.global_intervals.Clear();
                global.sw.Close();

            }

        }
        public void Fisher(List<double> etalon, List<double> auth)
        {
            int count = 0;
            for (int i = 0; i < etalon.Count; i++)
            {
                for (int j = 0; j < auth.Count; j++)
                {
                    Fr = Math.Max(etalon[i], auth[j]) / Math.Min(etalon[i], auth[j]);
                    if (Fr > global.Ft)
                    {
                        count++;
                    }
                }
            }
            if (count >= g)
            {
                DispField.Content = "Неоднорідні";
            }
            else
            {
                DispField.Content = "Однорідні";
            }
        }
        public void P_12st()
        {
            double P_1st = 0, P_2nd = 0;
            for (int i = 0; i < g; i++)
            {
                P_1st = (g - r[i]) * 1.0 / (g);
                P_2nd = (r[i]) * 1.0 / (g);
            }
            P_1st = Math.Round(P_1st / g, 2);
            P_2nd = Math.Round(P_2nd / g, 2);
            P1Field.Content = P_1st;
            P2Field.Content = P_2nd;
        }
        public void Auntefication(List<double> etalon_disp, List<double> auth_disp,
            List<double> etalon_math, List<double> auth_math)
        {
            r = new int[g];
            int n = global.CodeWord.Length;
            double P = 0;
            double etalon_d = 0, auth_d = 0, etalon_m = 0, auth_m = 0, S = 0, tp = 0;
            for (int i = 0; i < auth_disp.Count; i++)
            {
                auth_d = auth_disp[i];
                auth_m = auth_math[i];
                for (int j = 0; j < etalon_disp.Count; j++)
                {
                    etalon_d = etalon_disp[j];
                    etalon_m = etalon_math[j];
                    S = Math.Sqrt((etalon_d * etalon_d + auth_d * auth_d) * (n - 1) / (2 * n - 1));
                    tp = Math.Abs(etalon_m - auth_m) / (S * Math.Sqrt(2.0 / n));
                    if (tp < global.tT)
                    {
                        r[i]++;
                    }
                }

                P += (r[i] * 1.0 / g);
            }
            P = Math.Round(P / r.Length, 2);
            StatisticsBlock.Content = P;

        }

        private void CountProtection_SelectionChanged_1(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            InputField.Text = "";
            att = 1;
            q = 0;
        }
    }
}
