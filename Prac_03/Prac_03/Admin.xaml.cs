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
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        string connectionString = null;
        SqlConnection sqlConn = null;
        SqlCommand Com;
        SqlDataAdapter Data;
        static DataTable dT;
        int LenTable;
        int log_count = 3;
        string password;
        string strQ;
        int index = 0;
        public Admin()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        }

        private void Close_But_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            MainWindow mw = new MainWindow();
            mw.Show();
         }

        private void Exit_But_Click(object sender, RoutedEventArgs e)
        {
            Db.IsEnabled = false;
            PastPass.IsEnabled = false;
            NewPass1.IsEnabled = false;
            NewPass2.IsEnabled = false;
            Update_But.IsEnabled = false;
            Prev_But.IsEnabled = false;
            Next_But.IsEnabled = false;
            User_Cmbx.IsEnabled = false;
            Set_But.IsEnabled = false;
            AddUser_But.IsEnabled = false;
            AdminPassword.Password = "";

            dT.Clear();
            User_Cmbx.SelectedIndex = -1;
        }

        public void UpdateTable()
        {
            sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                strQ = "SELECT Name, Surname, Login, Status, Restriction FROM Users"; //дані що відображаються в таблиці
                Data = new SqlDataAdapter(strQ, sqlConn);
                Com = new SqlCommand(strQ, sqlConn);
                dT = new DataTable("Users");
                Data.Fill(dT);
                Db.ItemsSource = dT.DefaultView;
                LenTable = dT.Rows.Count;
            }
            sqlConn.Close();
        }
        private void Login_But_Click(object sender, RoutedEventArgs k)
        {

            password = AdminPassword.Password;
            strQ = "SELECT Password FROM Users WHERE Login = 'ADMIN';";// пароль адміна


            try
            {
                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                Data = new SqlDataAdapter(strQ, sqlConn);
                Com = new SqlCommand(strQ, sqlConn);
                DataTable pass = new DataTable("password");
                Data.Fill(pass);
                if (password == pass.Rows[0][0].ToString())
                {
                    Db.IsEnabled = true;
                    PastPass.IsEnabled = true;
                    NewPass1.IsEnabled = true;
                    NewPass2.IsEnabled = true;
                    Update_But.IsEnabled = true;
                    Prev_But.IsEnabled = true;
                    Next_But.IsEnabled = true;
                    User_Cmbx.IsEnabled = true;
                    Set_But.IsEnabled = true;
                    AddUser_But.IsEnabled = true;
                    MessageBox.Show("Вітаю, ADMIN!", "Welcome");
                    strQ = "SELECT Name, Surname, Login, Status, Restriction FROM Users"; //дані що відображаються в таблиці
                    Data = new SqlDataAdapter(strQ, sqlConn);
                    Com = new SqlCommand(strQ, sqlConn);
                    dT = new DataTable("Users");
                    Data.Fill(dT);
                    Db.ItemsSource = dT.DefaultView;
                    LenTable = dT.Rows.Count;

                    Login.Content = dT.Rows[0][2].ToString();
                    Name.Content = dT.Rows[0][0].ToString();
                    Surname.Content = dT.Rows[0][1].ToString();
                    Status.Content = dT.Rows[0][3].ToString();
                    Restraitment.Content = dT.Rows[0][4].ToString();
                    for (int i = 0; i < LenTable; i++)
                    {
                        User_Cmbx.Items.Add(dT.Rows[i][2]);
                    }
                }
                else
                {
                    log_count--;
                    if (log_count > 0)
                    {
                        
                        MessageBox.Show(("Пароль неправильний. Залишилось спроб: " + log_count), "Помилка");
                    }
                    else
                    {
                        System.Windows.Application.Current.Shutdown();
                    }
                    
                    
                }
                sqlConn.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }

        private void Update_But_Click(object sender, RoutedEventArgs e)
        {
            sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();
            String RealPass = PastPass.Password.ToString();
            String NewPass = NewPass1.Password.ToString();
            String NewPass3 = NewPass2.Password.ToString();
            if (RealPass == password)
            {
                if ((NewPass == NewPass3) && (NewPass != ""))
                {
                    if (sqlConn.State == System.Data.ConnectionState.Open)
                    {
                        strQ = "UPDATE Users SET Password = '" + NewPass + "' WHERE Login = 'ADMIN';";
                        Com = new SqlCommand(strQ, sqlConn);
                        Com.ExecuteNonQuery();
                        MessageBox.Show("Пароль успішно змінено!", "Пароль");
                    }
                }
                else
                {
                    MessageBox.Show("Паролі не співпадають!", "Пароль");
                }
            }
            else
            {
                MessageBox.Show("Поточний пароль невірний!", "Пароль");
            }
            PastPass.Password = "";
            NewPass1.Password = "";
            NewPass2.Password = "";
            sqlConn.Close();
        }

        private void Prev_But_Click(object sender, RoutedEventArgs e)
        {
            if (index > 0)
            {
                index--;
                Login.Content = dT.Rows[index][2].ToString();
                Name.Content = dT.Rows[index][0].ToString();
                Surname.Content = dT.Rows[index][1].ToString();
                Status.Content = dT.Rows[index][3].ToString();
                Restraitment.Content = dT.Rows[index][4].ToString();
                User_Cmbx.SelectedItem = Login.Content;
            }
        }

        private void Next_But_Click(object sender, RoutedEventArgs e)
        {
            if (index < LenTable - 1)
            {
                index++;
                Login.Content = dT.Rows[index][2].ToString();
                Name.Content = dT.Rows[index][0].ToString();
                Surname.Content = dT.Rows[index][1].ToString();
                Status.Content = dT.Rows[index][3].ToString();
                Restraitment.Content = dT.Rows[index][4].ToString();
                User_Cmbx.SelectedItem = Login.Content;
            }
        }

        private void AddUser_But_Click(object sender, RoutedEventArgs e)
        {
            sqlConn = new SqlConnection(connectionString); 
            sqlConn.Open();
            string login = AddUser_Field.Text;
            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Open)
                {
                    strQ = "INSERT INTO Users(Name, Surname, Login, Status, Restriction)"
                            + "VALUES('', '', '" + login + "', 1, 0);";

                    Com = new SqlCommand(strQ, sqlConn);
                    Com.ExecuteNonQuery();
                }
                UpdateTable();
                for (int i = 0; i < LenTable; i++)
                {
                    User_Cmbx.Items.Add(dT.Rows[i][2]);
                }
            }
            catch
            {

                MessageBox.Show("Користувача не додано! Можливо такий вже є!");
            }
            sqlConn.Close();
        }

        private void Set_But_Click(object sender, RoutedEventArgs e)
        {
            sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();
            bool UserStatus = (bool)Activity.IsChecked;
            bool Restriction = (bool)PassU.IsChecked;
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                strQ = "UPDATE Users SET Status = '" + UserStatus + "', Restriction = '" 
                    + Restriction + "' WHERE Login = '" + User_Cmbx.SelectedItem.ToString() + "';";
                Com = new SqlCommand(strQ, sqlConn);
                Com.ExecuteNonQuery();
            }
            sqlConn.Close();
            UpdateTable();
        }
    }
}
