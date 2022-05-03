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
    /// Логика взаимодействия для Teams.xaml
    /// </summary>
    public partial class Teams : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand com;
        SqlDataAdapter data;
        DataTable Table;
        int LenTable;
        public static int index = 0;
        DataSet ds;
        string sqlQ;
        int player_index = -1;
        int game_index = 0;
        Design d = new Design();
       
        public Teams()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            GetTeams();
            GetAllInfo();
            UpdateIndex(index);
            GetGames(index);
            GetPlayers(index);
            Team_DB.SelectedIndex = 0;
            for (int i = 1; i <= 12; i++)
            {
                Month.Items.Add(i);
            }
            for (int i = 1; i <= 31; i++)
            {
                Day.Items.Add(i);
            }
            for (int i = 12; i <= 22; i++)
            {
                Time.Items.Add(i + ":00");
            }


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

        private void Exit_But_Click(object sender, RoutedEventArgs e)
        {
            MainWindow t = new MainWindow();
            Hide();
            t.Show();
        }

        private void GetAndDhowData(string SQLQuery, DataGrid dataGrid)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            com = new SqlCommand(SQLQuery, connection);
            data = new SqlDataAdapter(com);
            Table = new DataTable();
            data.Fill(Table);
            dataGrid.ItemsSource = Table.DefaultView;
            LenTable = Table.Rows.Count;
            connection.Close();
        }
        private void GetPlayers(int index)
        {
            sqlQ = "SELECT dbo.Player.PlayerName AS [Ім'я'], dbo.Player.PlayerSurname AS Фамілія, dbo.Position.PositionName AS Позиція, dbo.Player.Number AS Номер,"
                + " dbo.Nation.NationName AS Країна, Age as Вік, dbo.Player.PlayerID AS ID "
                + " FROM dbo.Player INNER JOIN dbo.Nation ON dbo.Player.NationID = dbo.Nation.NationID"
                + " INNER JOIN dbo.Position ON dbo.Player.PositionID = dbo.Position.PositionID"
                + " WHERE (dbo.Player.TeamID = " + (index + 1) + ")";
            try
            {
                GetAndDhowData(sqlQ, Player_DB);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void GetTeams()
        {
             sqlQ = "SELECT TeamName AS Команда, City AS Місто"
                           + " FROM dbo.Team ";
            try
            {
                GetAndDhowData(sqlQ, Team_DB);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void GetGames(int index)
        {
                
            sqlQ = "SELECT ht.TeamName as [Домашня команда], HomeTeamGoals as [Голи 1], "
                + "GuestTeamGoals as [Голи 2], gt.TeamName as [Гостьова команда], "
                + "DateOf as Дата, TimeOF as Час, GameID AS ID "
                + "from Schedule sc "
                + "join Team gt on sc.GuestTeamID = gt.TeamID "
                + "join Team ht on sc.HomeTeamID = ht.TeamID where sc.HomeTeamID = " + (index + 1) + " or sc.GuestTeamID = " + (index + 1) + ";";
            try
            {
                GetAndDhowData(sqlQ, Games_DB);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void GetAllInfo()
        {
             sqlQ = "SELECT        dbo.Team.TeamName AS Команда, dbo.Team.City AS Місто, dbo.Stadium.StadiumName AS Стадіон, dbo.Team.YearOf AS Рік, dbo.Trainer.TrainerName, "
                + "dbo.Trainer.TrainerSurName, dbo.TournamentResult.Points, dbo.TournamentResult.Wins, dbo.TournamentResult.Draws, dbo.TournamentResult.Looses, "
                + "dbo.TournamentResult.Scored, dbo.TournamentResult.Missed, dbo.TeamStat.AvgBallPossesion, dbo.TeamStat.AvgShots, dbo.TeamStat.AvgFalls, "
                + "dbo.TeamStat.ScoreGoal, dbo.Team.Image "
                + "FROM            dbo.Team INNER JOIN"
                + " dbo.Trainer ON dbo.Team.CoachID = dbo.Trainer.CoachID INNER JOIN"
                + " dbo.TeamStat ON dbo.Team.TeamID = dbo.TeamStat.TeamID INNER JOIN"
                + " dbo.TournamentResult ON dbo.Team.TeamID = dbo.TournamentResult.TeamID INNER JOIN"
                + " dbo.Stadium ON dbo.Team.StadiumID = dbo.Stadium.StadiumID;";
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                com = new SqlCommand(sqlQ, connection);
                data = new SqlDataAdapter(com);
                Table = new DataTable();
                data.Fill(Table);
                LenTable = Table.Rows.Count;
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void Next_But_Click(object sender, RoutedEventArgs e)
        {
            GetAllInfo();
            if (index < LenTable - 1)
            {
                index++;
                UpdateIndex(index);
            }
            GetPlayers(index);
        }

        private void Prev_But_Click(object sender, RoutedEventArgs e)
        {
            GetAllInfo();
            if (index > 0)
            {
                index--;
                UpdateIndex(index);
            }
            GetPlayers(index);
        }
       
        private void UpdateIndex(int index)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            ds = new DataSet();
            SqlDataAdapter sqa = new SqlDataAdapter ("Select Image from Team where TeamID= " + (index+1) +  ";", connection);
            sqa.Fill(ds);
            connection.Close();
            byte[] data = (byte[])ds.Tables[0].Rows[0][0];
            MemoryStream strm = new MemoryStream();
            strm.Write(data, 0, data.Length);
            strm.Position = 0;
            System.Drawing.Image img = System.Drawing.Image.FromStream(strm);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Seek(0, SeekOrigin.Begin);
            bi.StreamSource = ms;
            bi.EndInit();
            img_logo.Source = bi;

            TeamName.Content = Table.Rows[index][0];
            City.Content = Table.Rows[index][1];
            Stadium.Content = Table.Rows[index][2];
            YearOf.Content = Table.Rows[index][3];
            Coach.Content = Table.Rows[index][4].ToString() + " " + Table.Rows[index][5].ToString();
            Points.Content = Table.Rows[index][6];
            Wins.Content = Table.Rows[index][7];
            Draws.Content = Table.Rows[index][8];
            Looses.Content = Table.Rows[index][9];
            Scored.Content = Table.Rows[index][10];
            Missed.Content = Table.Rows[index][11];
            Possesion.Content = Table.Rows[index][12];
            Shots.Content = Table.Rows[index][13];
            Falls.Content = Table.Rows[index][14];
            Goals_shots.Content = Table.Rows[index][15];
            Team_DB.SelectedIndex = index;
            GetPlayers(index);
            GetGames(index);
            GetAllInfo();
        }

        private void Team_DB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetAllInfo();
            index = Team_DB.SelectedIndex;
            if (index != 21)
            {
                UpdateIndex(index);
            }
            GetPlayers(index);
        }

        private void Add_But_Click(object sender, RoutedEventArgs e)
        {
            AddPlayer ad = new AddPlayer();
            ad.Show();
        }

        private void Del_But_Click(object sender, RoutedEventArgs k)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            if (player_index == -1)
            {
                MessageBox.Show("Оберіть гравця!", "Помилка");
            }
            else
            {
                int id = Convert.ToInt32(Table.Rows[player_index][6]);
                sqlQ = "DELETE FROM Player WHERE PlayerID = " + id + ";";
                try
                {
                    com = new SqlCommand(sqlQ, connection);
                    data = new SqlDataAdapter(com);
                    int deletionCount = com.ExecuteNonQuery();
                    MessageBox.Show("Видалено " + deletionCount + " гравця");
                    connection.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                GetPlayers(index);
                GetAllInfo();
            }
            
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

        private void Player_DB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            player_index = Player_DB.SelectedIndex;

        }

        

        private void Reschedule_But_Click(object sender, RoutedEventArgs k)
        {
            game_index = Games_DB.SelectedIndex;
            if (game_index != -1)
            {
                GetGames(index);
                string month = "";
                string date = "";
                string dateOf = "";
                string time = Table.Rows[game_index][5].ToString();
                int year = 0;
                if (Month.SelectedIndex != -1 && Day.SelectedIndex != -1)
                {
                    if (Time.SelectedIndex != -1)
                    {
                        time = Time.SelectedItem.ToString();
                    }
                    if (Convert.ToInt32(Month.SelectedItem) < 8)
                    {
                        year = 2022;
                    }
                    else
                    {
                        year = 2021;
                    }
                    month = Month.SelectedItem.ToString();
                    date = "0" + month;
                    if (Convert.ToInt32(Month.SelectedItem) > 9)
                        date = month;
                    dateOf = year.ToString() + "-" + date + "-" + Day.SelectedItem;

                    sqlQ = "SELECT * FROM Schedule WHERE DateOf = '" + dateOf + "' AND HomeTeamID = (SELECT TeamID FROM Team WHERE TeamName = '" + Table.Rows[game_index][0] + "') "
                        + "OR DateOf = '" + dateOf + "' AND GuestTeamID = (SELECT TeamID FROM Team WHERE TeamName = '" + Table.Rows[game_index][0] + "') "
                        + "OR DateOf = '" + dateOf + "' AND HomeTeamID = (SELECT TeamID FROM Team WHERE TeamName = '" + Table.Rows[game_index][3] + "') "
                        + "OR DateOf = '" + dateOf + "' AND GuestTeamID = (SELECT TeamID FROM Team WHERE TeamName = '" + Table.Rows[game_index][3] + "') ";
                    try
                    {
                        connection = new SqlConnection(connectionString);
                        connection.Open();
                        com = new SqlCommand(sqlQ, connection);
                        data = new SqlDataAdapter(com);
                        DataTable t = new DataTable();
                        data.Fill(t);
                        LenTable = t.Rows.Count;
                        connection.Close();

                    }
                    catch (Exception e)
                    {

                        MessageBox.Show(e.Message);
                    }
                    try
                    {
                        if (LenTable == 0)
                        {
                            connection = new SqlConnection(connectionString);
                            connection.Open();
                            sqlQ = "UPDATE Schedule SET DateOf = '" + dateOf + "', TimeOF = '" + time + "' WHERE GameID = " + Table.Rows[game_index][6] + ";";
                            com = new SqlCommand(sqlQ, connection);
                            MessageBox.Show("Змінено " + com.ExecuteNonQuery() + "рядків");
                        }
                        else
                        {
                            MessageBox.Show($"В такий день {Table.Rows[game_index][0]} або {Table.Rows[game_index][3]} вже грала!", "Помилка");
                        }
                    }
                    catch (Exception e)
                    {

                        MessageBox.Show(e.Message); ;
                    }
                }
                else if(Time.SelectedIndex != -1)
                {
                    time = Time.SelectedItem.ToString();
                    sqlQ = "UPDATE Schedule SET TimeOf = '" + time + "' WHERE GameID = " + Table.Rows[game_index][6] + ";";
                    try
                    {
                        connection = new SqlConnection(connectionString);
                        connection.Open();
                        com = new SqlCommand(sqlQ, connection);
                        data = new SqlDataAdapter(com);
                        MessageBox.Show("Змінено " + com.ExecuteNonQuery() + "рядків");
                        com.ExecuteNonQuery();
                        connection.Close();

                    }
                    catch (Exception e)
                    {

                        MessageBox.Show(e.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Заповніть поля, якщо хочете перенести зустріч", "Помилка");
                }
            }
            else
            {
                MessageBox.Show("Оберіть матч, який хочете перенести", "Помилка");
            }
            GetGames(index);
        }
    }
}
