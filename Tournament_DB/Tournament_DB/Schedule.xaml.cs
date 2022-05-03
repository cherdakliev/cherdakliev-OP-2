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
    /// Логика взаимодействия для Schedule.xaml
    /// </summary>
    public partial class Schedule : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand com;
        SqlDataAdapter data;
        DataTable Table;
        DataTable StatTable;
        DataTable TeamTable;
        DataTable empty = new DataTable();
        string sqlQ;
        string stadium = "";
        string team1 = "";
        string team2 = "";
        string dateOf = "";
        string and = "";
        string where = "";
        int index = -1;
        int LenTable;
        Design d = new Design();
        public Schedule()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            Fill_Comboboxes();
            GetGames();
            Games.SelectedIndex = 0;
            UpdateIndex();
        }

        private void Fill_Comboboxes()
        {
            GetStadium();
            for (int i = 0; i < LenTable; i++)
            {
                Stadium.Items.Add(Table.Rows[i][0]);
            }
            GetTeams();
            for (int i = 0; i < LenTable; i++)
            {
                Ht_Team.Items.Add(Table.Rows[i][0]);
                Gt_Team.Items.Add(Table.Rows[i][0]);
            }
            for (int i = 1; i <= 12; i++)
            {
                Month.Items.Add(i);
            }
            for (int i = 1; i <= 31; i++)
            {
                Day.Items.Add(i);
            }
        }

        private void GetGames()
        {
            sqlQ = "SELECT ht.TeamName as [Домашня команда], HomeTeamGoals as [Голи 1], "
                + "GuestTeamGoals as [Голи 2], gt.TeamName as [Гостьова команда], "
                + "DateOf as Дата, TimeOF as Час, sc.GameID AS ID, Cost as Квиток "
                + "from Schedule sc "
                + "join Team gt on sc.GuestTeamID = gt.TeamID "
                + "join Team ht on sc.HomeTeamID = ht.TeamID " 
                + "join TicketCost tc on tc.GameID = sc.GameID " + stadium + dateOf + team1 + team2;
            try
            {
                GetAndDhowData(sqlQ, Games);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void GetStatInfo()
        {
            sqlQ = "SELECT * FROM GameStatistic WHERE GameID = " + index;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                com = new SqlCommand(sqlQ, connection);
                data = new SqlDataAdapter(com);
                StatTable = new DataTable();
                data.Fill(StatTable);
                //LenTable = StatTable.Rows.Count;
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void GetScoredHome()
        {
            sqlQ = "SELECT PlayerSurname as Гравець , GoalTime as Час FROM Goals "
                    + "join Player on PlayerID = ScoredPlayerID WHERE GameID = " + index + " AND ScoredTeamID = " +
                    "(SELECT TeamID FROM Team WHERE TeamName = '" + Table.Rows[Games.SelectedIndex][0] + "') ORDER BY GoalTime DESC";
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                com = new SqlCommand(sqlQ, connection);
                data = new SqlDataAdapter(com);
                TeamTable = new DataTable();
                data.Fill(TeamTable);
                if (TeamTable.Rows.Count != 0)
                {
                    Ht_Players_Scored.ItemsSource = TeamTable.DefaultView;
                }
                else
                {
                    Ht_Players_Scored.ItemsSource = empty.DefaultView;
                }
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void GetScoredGuest()
        {
            sqlQ = "SELECT PlayerSurname as Гравець , GoalTime as Час  FROM Goals "
                    + "join Player on PlayerID = ScoredPlayerID WHERE GameID = " + index + " AND ScoredTeamID = " +
                    "(SELECT TeamID FROM Team WHERE TeamName = '" + Table.Rows[Games.SelectedIndex][3] + "') ORDER BY GoalTime DESC";
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                com = new SqlCommand(sqlQ, connection);
                data = new SqlDataAdapter(com);
                TeamTable = new DataTable();
                data.Fill(TeamTable);
                if (TeamTable.Rows.Count != 0)
                {
                    Gt_Players_Scored.ItemsSource = TeamTable.DefaultView;
                }
                else
                {
                    Gt_Players_Scored.ItemsSource = empty.DefaultView;
                }
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void GetHomePlayer()
        {
            sqlQ = "SELECT Number as Номер, PlayerSurname as Гравець FROM Player WHERE TeamID = (SELECT TeamID FROM Team WHERE TeamName = '" + Table.Rows[Games.SelectedIndex][0] + "');";
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                com = new SqlCommand(sqlQ, connection);
                data = new SqlDataAdapter(com);
                TeamTable = new DataTable();
                data.Fill(TeamTable);
                Ht_Players.ItemsSource = TeamTable.DefaultView;
                //LenTable = StatTable.Rows.Count;
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void GetGuestPlayer()
        {
            sqlQ = "SELECT Number as Номер, PlayerSurname as Гравець  FROM Player WHERE TeamID = (SELECT TeamID FROM Team WHERE TeamName = '" + Table.Rows[Games.SelectedIndex][3] + "');";
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                com = new SqlCommand(sqlQ, connection);
                data = new SqlDataAdapter(com);
                TeamTable = new DataTable();
                data.Fill(TeamTable);
                Gt_Players.ItemsSource = TeamTable.DefaultView;
                //LenTable = StatTable.Rows.Count;
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        
        private void UpdateIndex()
        {
            GetStatInfo();
            GetScoredHome();
            GetScoredGuest();
            GetHomePlayer();
            GetGuestPlayer();
            connection = new SqlConnection(connectionString);
            connection.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter sqa = new SqlDataAdapter("Select Image from Team where TeamName = '" + Table.Rows[Games.SelectedIndex][0] + "' OR TeamName = '" + Table.Rows[Games.SelectedIndex][3] + "';", connection);
            sqa.Fill(ds);
            connection.Close();
            byte[] data;
            if (index > 190)
            {
                data = (byte[])ds.Tables[0].Rows[1][0];
            }
            else
            {
                data = (byte[])ds.Tables[0].Rows[0][0];
            }
             
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
            Ht_img.Source = bi;

            if (index > 190)
            {
                data = (byte[])ds.Tables[0].Rows[0][0];
            }
            else
            {
                data = (byte[])ds.Tables[0].Rows[1][0];
            }
            strm = new MemoryStream();
            strm.Write(data, 0, data.Length);
            strm.Position = 0;
            img = System.Drawing.Image.FromStream(strm);
            bi = new BitmapImage();
            bi.BeginInit();
            ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Seek(0, SeekOrigin.Begin);
            bi.StreamSource = ms;
            bi.EndInit();
            Gt_img.Source = bi;

            HomeTeam.Content = Table.Rows[Games.SelectedIndex][0];
            GuestTeam.Content = Table.Rows[Games.SelectedIndex][3];
            Score_1.Content = Table.Rows[Games.SelectedIndex][1];
            Score_2.Content = Table.Rows[Games.SelectedIndex][2];

            Ht_Poss.Content = StatTable.Rows[0][1];
            Gt_Poss.Content = StatTable.Rows[0][2];
            Ht_Shots.Content = StatTable.Rows[0][3];
            Gt_Shots.Content = StatTable.Rows[0][4];
            Ht_Falls.Content = StatTable.Rows[0][5];
            Gt_Falls.Content = StatTable.Rows[0][6];
            Date.Content = Table.Rows[Games.SelectedIndex][4];
            Cost.Content = Table.Rows[Games.SelectedIndex][7] + " £";




        }
        private void GetStadium()
        {
            sqlQ = "SELECT StadiumName FROM Stadium ;";
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

        private void GetTeams()
        {
            sqlQ = "SELECT TeamName FROM Team;";
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

        private void Stadium_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (Stadium.SelectedIndex != -1)
            {
                and = "";
                where = "";
                if (team1.Length != 0 || team2.Length != 0 || (Day.SelectedIndex != -1 && Month.SelectedIndex != -1))
                {
                    and = " AND ";
                }
                else
                {
                    where = " WHERE ";
                }
                stadium = and + where + " HomeTeamID = (SELECT TeamID FROM Team WHERE StadiumID = " 
                    + "(SELECT StadiumID FROM Stadium WHERE StadiumName = '" + Stadium.SelectedItem +  "'))";
            }
        }

        private void Search_But_Click(object sender, RoutedEventArgs k)
        {
            int year = 0;
            string month = "";
            string date = "";
            and = "";
            where = "";
            if (Day.SelectedIndex != -1 && Month.SelectedIndex != -1)
            {
                if (stadium.Length != 0 || team1.Length != 0 || team2.Length != 0)
                {
                    and = " AND ";
                }
                else
                {
                    where = " WHERE ";
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
                date = year.ToString() + "-" + date + "-" + Day.SelectedItem;
                dateOf = and + where + " DateOf = '" + date + "'";

            }
            GetGames();
            
        }



        private void Clear_But_Click(object sender, RoutedEventArgs e)
        {
            Stadium.SelectedIndex = -1;
            Day.SelectedIndex = -1;
            Month.SelectedIndex = -1;
            Ht_Team.SelectedIndex = -1;
            Gt_Team.SelectedIndex = -1;
            team1 = team2 = dateOf = stadium = "";
            GetGames();

        }

        private void Ht_Team_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Ht_Team.SelectedIndex != -1)
            {
                and = "";
                where = "";
                if (stadium.Length != 0 || team2.Length != 0 || (Day.SelectedIndex != -1 && Month.SelectedIndex != -1))
                {
                    and = " AND ";
                }
                else
                {
                    where = " WHERE ";
                }
                team1 = and + where + " HomeTeamID = (SELECT TeamID FROM Team WHERE TeamName = '" + Ht_Team.SelectedItem + "')";
            }
        }

        private void Gt_Team_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Gt_Team.SelectedIndex != -1)
            {
                and = "";
                where = "";
                if (stadium.Length != 0 || team1.Length != 0 || (Day.SelectedIndex != -1 && Month.SelectedIndex != -1))
                {
                    and = " AND ";
                }
                else
                {
                    where = " WHERE ";
                }
                team2 = and + where + " GuestTeamID = (SELECT TeamID FROM Team WHERE TeamName = '" + Gt_Team.SelectedItem + "')";
            }
        }

        private void Games_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Games.SelectedIndex != -1)
            {
                index = Convert.ToInt32(Table.Rows[Games.SelectedIndex][6]);
                UpdateIndex();
            }
            
        }
    }
}
