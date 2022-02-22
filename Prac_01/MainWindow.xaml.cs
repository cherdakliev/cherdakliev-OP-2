using System.Windows;

namespace Prac_01
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

        private void StudyModeBtn_Click(object sender, RoutedEventArgs e)
        {
            StudyModeWindow studyModeWindow = new StudyModeWindow();
            Hide();
            studyModeWindow.Show();
        }

        private void ProtectionModeBtn_Click(object sender, RoutedEventArgs e)
        {
            ProtectionModeWindow protectionModeWindow = new ProtectionModeWindow();
            Hide();
            protectionModeWindow.Show();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
