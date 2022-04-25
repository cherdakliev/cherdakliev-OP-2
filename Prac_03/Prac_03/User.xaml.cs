using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Prac_03
{
    /// <summary>
    /// Логика взаимодействия для User.xaml
    /// </summary>
    public partial class User : Window
    {
        string connectionString = null;
        SqlConnection sqlConn = null;
        SqlCommand Com;
        SqlDataAdapter Data;
        static DataTable dT;
        int count = 3;
        string strQ;
        string Login;
        public User()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void Autor_But_Click(object sender, RoutedEventArgs e)
        {
            Login = LoginField.Text;
            string password = PassField.Password.ToString();

            sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                strQ = "SELECT * FROM Users WHERE Login='" + Login + "';";
                Data = new SqlDataAdapter(strQ, sqlConn);
                Com = new SqlCommand(strQ, sqlConn);
                dT = new DataTable("Користувачі системи");
                Data.Fill(dT);
                if (dT.Rows.Count == 0)
                {
                    MessageBox.Show("Такого користувача на знайдено");
                }
                else
                {
                    bool Status = Convert.ToBoolean(dT.Rows[0][4]);
                    if (!Status)MessageBox.Show("Користувач заблокований Адміністратором системи!");
                    else
                    {
                        if ((dT.Rows[0][2].ToString() == Login) && (dT.Rows[0][3].ToString() == password))
                        {
                            Name.Text = dT.Rows[0][0].ToString();
                            Surname.Text = dT.Rows[0][1].ToString();
                            NewPass1.Password = "";
                            NewPass2.Password = "";
                            Name.IsEnabled = true;
                            Surname.IsEnabled = true;
                            NewPass1.IsEnabled = true;
                            NewPass2.IsEnabled = true;
                            Update_But.IsEnabled = true;
                            Exit_But.IsEnabled = true;
                            MessageBox.Show("Вітаємо, " + Name.Text + "!", "Вітання");
                        }
                        else
                        {
                            count--;
                            if (count == 0)
                            {
                                System.Windows.Application.Current.Shutdown();
                            }
                            MessageBox.Show("Невірно введений пароль. Залишилось спроб: " + count);
                        }
                    }
                }
            }
            sqlConn.Close();

        }

        private void Close_But_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        private void Exit_But_Click(object sender, RoutedEventArgs e)
        {
            Exit_But.IsEnabled = false;
            Name.IsEnabled = false;
            Surname.IsEnabled = false;
            NewPass1.IsEnabled = false;
            NewPass2.IsEnabled = false;
            Update_But.IsEnabled = false;
            LoginField.Text = "";
            PassField.Password = "";
        }
        Boolean RestrictionFunc(string password)
        {
            Byte Count1, Count2, Count3, Count4;
            Byte LenPass = (Byte)password.Length;
            Count1 = Count2 = Count3 = Count4 = 0;
            if (LenPass > 8 &&  LenPass < 21)
                Count1++;

            for (Byte i = 0; i < LenPass; i++)
            {
                if ((Convert.ToInt32(password[i]) >= 65) && (Convert.ToInt32(password[i]) <= 65 + 25))
                    Count2++;

                if ((Convert.ToInt32(password[i]) >= 97) && (Convert.ToInt32(password[i]) <= 97 + 25))
                    Count3++;

                if ((password[i] == '&') || (password[i] == '#') || (password[i] == '*') || (password[i] == '/'))
                    Count4++;
            }
            return (Count1 * Count2 * Count3 * Count4 != 0);
        }

        Boolean RestrictionFuncLogin(string login)
        {
            Byte Count1, Count2;
            Byte LenPass = (Byte)login.Length;
            Count1 = Count2 = 1;
            if (LenPass > 12)
                Count1 = 0;

            for (Byte i = 0; i < LenPass; i++)
            {

                if ((login[i] == '&') || (login[i] == '#') || (login[i] == '*') ||
                    (login[i] == '/') || (login[i] == '!') || (login[i] == '$') || (login[i] == '%')
                    || (login[i] == '^') || (login[i] == '(') || (login[i] == ')')
                    || (login[i] == '-') || (login[i] == '+') || (login[i] == '='))
                    Count2 = 0;
            }
            return (Count1 * Count2 != 0);
        }

        private void Reg_But_Click(object sender, RoutedEventArgs e)
        {
            sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();
            string name = Name_reg.Text;
            string surname = Surname_reg.Text;
            string login = LoginField_reg.Text;
            string newPass = NewPass1_reg.Password;
            string newPass2 = NewPass2_reg.Password;
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    if ((newPass == newPass2) && (login != "") && (newPass != ""))
                    {
                        if (RestrictionFuncLogin(login))
                        {
                            strQ = "INSERT INTO Users VALUES('" + name +"', '"
                                + surname + "', '" + login + "', '" 
                                + newPass + "', 'True', 'False' );";
                            Com = new SqlCommand(strQ, sqlConn);
                            Com.ExecuteNonQuery();
                            MessageBox.Show("Користувача " + login + " зареєстровано", "Реєстрація");
                            Name_reg.Text = Surname_reg.Text = LoginField_reg.Text = NewPass1_reg.Password = NewPass2_reg.Password = "";
                        }
                        else
                        {
                            MessageBox.Show("Ім'я користувача не відповідає умовам", "Помилка");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Обліковий запис не створено. Спробуйте ще раз!", "Помилка");
                    }
                }
                catch
                {
                    MessageBox.Show("Користувач з таким логіном вжеіснує у системі!", "Помилка");
                }
                
                sqlConn.Close();
            }

        }

        private void Update_But_Click(object sender, RoutedEventArgs e)
        {
            sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();

            string newname = Name.Text;
            string newsurname = Surname.Text;
            string newpasswd = NewPass1.Password;
            string newpasswd2 = NewPass2.Password;

            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                if ((newpasswd == newpasswd2) && (newpasswd != ""))
                {
                    Boolean flag = RestrictionFunc(newpasswd);
                    if (Convert.ToBoolean(dT.Rows[0][5]) == true)
                    {
                        if (flag == true)
                        {
                            strQ = "UPDATE Users SET Name = '" + newname + "',  "
                                + "Surname = '" + newsurname + "', "
                                + "Password = '" + newpasswd + "' "
                                + "WHERE Login = '" + Login + "';";
                            Com = new SqlCommand(strQ, sqlConn);
                            Com.ExecuteNonQuery();
                        }
                        else
                        {
                            MessageBox.Show("У паролі немає літер верхнього та нижнього регістрів, а також арифметичних операцій!Спробуйте знову!");
                        }

                    }
                    else
                    {
                        strQ = "UPDATE Users SET Name = '" + newname + "',  "
                               + "Surname = '" + newsurname + "', "
                               + "Password = '" + newpasswd + "', "
                               + "WHERE Login = '" + Login + "';";
                        Com = new SqlCommand(strQ, sqlConn);
                        Com.ExecuteNonQuery();
                    }
                }
                else
                {
                    MessageBox.Show("Введено пустий пароль або новий пароль повторно введено некоректно!");
                }
            }
            sqlConn.Close();
        }
    }
}
