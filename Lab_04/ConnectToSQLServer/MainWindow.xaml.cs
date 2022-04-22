using System.Windows;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Windows.Controls;

namespace ConnectToSQLServer
{
    public partial class MainWindow : Window
    {
        string connectionString = null;        
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;

        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //MessageBox.Show(connectionString);
            GetTournamentResultData();
            GetTeamData();
            GetPlayerRecordsData();
            GetScheduleData();

        }

        private void GetAndDhowData(string SQLQuery, DataGrid dataGrid)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            DataTable Table = new DataTable();
            adapter.Fill(Table);
            dataGrid.ItemsSource = Table.DefaultView;
            connection.Close();

            
        }

        private void GetTournamentResultData()
        {
            string sqlQ = "SELECT       TOP (100) PERCENT dbo.Team.TeamName AS Команда, dbo.TournamentResult.Points AS Бали, dbo.TournamentResult.Wins AS Перемоги," 
                         + "dbo.TournamentResult.Draws AS Нічиї, dbo.TournamentResult.Looses AS Поразки, dbo.TournamentResult.Scored AS Забито," 
                         + "dbo.TournamentResult.Missed AS Пропущено, dbo.TournamentResult.DiffBetween AS[Різниця забитих та пропущених]"
                        + "FROM         dbo.Team INNER JOIN dbo.TournamentResult ON dbo.Team.TeamID = dbo.TournamentResult.TeamID;";
            try
            {
                GetAndDhowData(sqlQ, TournamentDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void GetTeamData()
        {
            string sqlQ = "SELECT TOP (100) PERCENT dbo.Team.TeamName AS Команда, dbo.Team.City AS Місто, dbo.Trainer.TrainerSurName AS Тренер,"
                          + " dbo.Team.PastPos AS [Позиція в минулому сезоні], dbo.Stadium.StadiumName AS Стадіон, dbo.Team.YearOf AS [Рік заснування]"
                          + "FROM dbo.Team INNER JOIN dbo.Trainer ON dbo.Team.CoachID = dbo.Trainer.CoachID INNER JOIN dbo.Stadium ON dbo.Team.StadiumID = dbo.Stadium.StadiumID;";
            try
            {
                GetAndDhowData(sqlQ, TeamDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void GetPlayerRecordsData()
        {
            string sqlQ = "SELECT TOP (100) PERCENT dbo.Player.PlayerSurname AS Гравець, dbo.Team.TeamName AS Команда, dbo.PlayerRecords.Goals AS Голи,"
                         + "dbo.PlayerRecords.Assists AS [Гольові передачі]"
                         + "FROM  dbo.Player INNER JOIN dbo.PlayerRecords ON dbo.Player.PlayerID=dbo.PlayerRecords.PlayerID INNER JOIN dbo.Team ON dbo.Player.TeamID=dbo.Team.TeamID;";
            try
            {
                GetAndDhowData(sqlQ, PlayerRecDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void GetScheduleData()
        {
            string sqlQ = "SELECT ht.TeamName as [Домашня команда], HomeTeamGoals as [Голи],"
                           + "GuestTeamGoals as [Голи], gt.TeamName as [Гостьова команда], DateOf as Дата from Schedule sc"
                           + "  join Team gt on sc.GuestTeamID = gt.TeamID"
                           + "  join Team ht on sc.HomeTeamID = ht.TeamID;";
            try
            {
                GetAndDhowData(sqlQ, ScheduleDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}