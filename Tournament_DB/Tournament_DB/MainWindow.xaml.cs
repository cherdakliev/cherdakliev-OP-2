using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tournament_DB
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Design d = new Design();
        
        public MainWindow()
        {
            InitializeComponent();
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
            System.Windows.Application.Current.Shutdown();
        }

        private void Team_But_Click(object sender, RoutedEventArgs e)
        {
            Teams t = new Teams();
            Hide();
            t.Show();
        }
        private void Schedule_But_Click(object sender, RoutedEventArgs e)
        {
            Schedule sc = new Schedule();
            Hide();
            sc.Show();
        }

        private void Results_But_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
