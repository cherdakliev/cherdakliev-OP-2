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

namespace Prac_03
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Admin_But_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Admin admin = new Admin();
            admin.Show();

        }

        private void Exit_But_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void User_But_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            User user = new User();
            user.Show();
        }

        private void About_But_Click(object sender, RoutedEventArgs e)
        {
            
            Hide();
            About about = new  About();
            about.Show();
        }
    }
}
