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

namespace Lab_01_work
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExitBut_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void ToWin1_Click(object sender, RoutedEventArgs e)
        {
            Window1 dt = new Window1();
            Hide();
            dt.Show();
        }

        private void ToWin2_Click(object sender, RoutedEventArgs e)
        {
            GameWin dt = new GameWin();
            Hide();
            dt.Show();
        }

        private void ToWin4_Click(object sender, RoutedEventArgs e)
        {
            Created mw = new Created();
            Hide();
            mw.Show();
        }

        private void ToWin3_Click(object sender, RoutedEventArgs e)
        {
            Calculator cal = new Calculator();
            Hide();
            cal.Show();
        }
    }
}
