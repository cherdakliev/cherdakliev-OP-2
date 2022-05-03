using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Configuration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.IO;

namespace Tournament_DB
{
    /// <summary>
    /// Логика взаимодействия для AddPlayer.xaml
    /// </summary>
    public partial class AddPlayer : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand com;
        SqlDataAdapter data;
        DataTable Table;
        int LenTable;
        string sql;
        Design d = new Design();
        public AddPlayer()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            for (int i = 1; i <= 12; i++)
            {
                Month.Items.Add(i);
            }
            for (int i = 1; i <= 31; i++)
            {
                Day.Items.Add(i);
            }
            sql = "SELECT * FROM Nation; ";
            GetAndDhowData(sql);
            for (int i = 0; i < LenTable; i++)
            {
                Country.Items.Add(Table.Rows[i][1]);
            }
            sql = "SELECT * FROM Position";
            GetAndDhowData(sql);
            for (int i = 0; i < LenTable; i++)
            {
                Position.Items.Add(Table.Rows[i][1]);
            }
            sql = "SELECT * FROM Player";
            GetAndDhowData(sql);
        }

        private void Close_But_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.Background = d.LinearGrad_1();
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.Background = d.LinearGrad_2();
        }

        private void Add_But_Click(object sender, RoutedEventArgs k)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            string name = Name.Text;
            string surname = Surname.Text;
            int pos = (Position.SelectedIndex + 1);
            string country = "";
            string age = Age.Text;
            string num = Number.Text;
            string month = "";
            string date = "";
            string dateOf = "";
            if (Month.SelectedIndex != -1 && Day.SelectedIndex != -1 && Country.SelectedIndex != -1)
            {
                country = Country.SelectedItem.ToString();
                month = Month.SelectedItem.ToString();
                date = "0" + month;
                if (Convert.ToInt32(Month.SelectedItem) > 9)
                date = month;
                dateOf = (2022 - Convert.ToInt32(Age.Text)).ToString() 
                + "-" + month + "-" + Day.SelectedItem;
            }
            if (name.Length == 0 || surname.Length == 0 || pos == -1 || country.Length == 0 || age.Length == 0
                || num.Length == 0 || month.Length == 0 || Day.SelectedIndex == -1)
            {
                MessageBox.Show("Заповніть пусті поля!");
            }
            else
            {
                bool hasLettersAge = age.Any(char.IsLetter);
                bool hasLettersNum = num.Any(char.IsLetter);

                if ((name.Length > 10) || (IsDigits(name)) || (IsDigits(surname)) || (hasLettersAge) || (hasLettersNum) || (surname.Length > 15) || (Convert.ToInt32(num) > 99))
                {
                    MessageBox.Show($"Символів в імені може бути не більше 10, ім'я не може мати цифри,{Environment.NewLine}" +
                        $"Символів в Фамілії може бути не більше 15, фамілія не може мати цифри,{Environment.NewLine}" +
                        $"Номер не може бути більше 99, номер та вік не можуть мати літери", "Помилка");
                }
                else
                {
                    if ((Convert.ToInt32(age) > 40) || (Convert.ToInt32(age) < 17))
                    {
                        MessageBox.Show("Футболіст замалий або застарий!", "Помилка");
                    }
                    else
                    {
                        sql = "INSERT INTO Player"
                       + " Values( " + (Convert.ToInt32(Table.Rows[LenTable - 1][0]) + 1) + ", '" + name + "', '"
                        + surname + "', " + pos + ", " + (Teams.index + 1) + ", (SELECT NationID FROM Nation WHERE NationName = '" + country + "'), " + age + ", " + num + ", '" + dateOf + "');";
                        try
                        {
                            com = new SqlCommand(sql, connection);
                            com.ExecuteNonQuery();
                            MessageBox.Show("Гравця " + surname + " успішно додано!", "Гравець");
                        }
                        catch (Exception e)
                        {

                            MessageBox.Show(e.Message);
                        }
                    }
                    
                }
                
            }
        }
        bool IsDigits(string str)
        {
            foreach (char c in str)
            {
                if (char.IsDigit(c))
                    return true;
            }

            return false;
        }
        private void GetAndDhowData(string SQLQuery)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            com = new SqlCommand(SQLQuery, connection);
            data = new SqlDataAdapter(com);
            Table = new DataTable();
            data.Fill(Table);
            LenTable = Table.Rows.Count;
            connection.Close();
        }
        private void Month_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int lastDay = 31;
            int day_index = Day.SelectedIndex;
            Day.Items.Clear();
            if ((Month.SelectedIndex + 1) % 2 == 0)
            {
                if ((Month.SelectedIndex + 1) == 2)
                {
                    lastDay = 28;
                }
                else
                {
                    lastDay = 30;
                }
            }
            for (int i = 1; i <= lastDay; i++)
            {
                Day.Items.Add(i);
            }
            Day.SelectedIndex = day_index;
        }

    }
}
