using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Lab_01_work
{
    /// <summary>
    /// Логика взаимодействия для GameWin.xaml
    /// </summary>
    public partial class GameWin : Window
    {
        public GameWin()
        {

            InitializeComponent();

            foreach (ComboBox b in this.MyGrid.Children.OfType<ComboBox>())
            {
                b.Items.Add("❌");
                b.Items.Add("⭕");
            }
           
            
        }

        private void GoToMainBut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }

        private void emp1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox b = (ComboBox)e.Source;
            b.IsEnabled = false;


            if (b.SelectedItem == null)
            {
                PlayStep.Content = "Хід:";
            }
            else if (b.SelectedItem.ToString() == "❌")
            {
                PlayStep.Content = "Хід: Нолики " + b.Items[1];
            }
            else
            {
                PlayStep.Content = "Хід: Хрестики " + b.Items[0];
            }
            CheckWin();
        }

        public void ShowWin(ComboBox s)
        {
            MessageBoxResult result = MessageBox.Show("Переможець: " + s.SelectedItem + "\nХочете почати спочатку? ", "Результат", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    foreach (ComboBox b in this.MyGrid.Children.OfType<ComboBox>())
                    {
                        b.SelectedIndex = -1;
                        b.IsEnabled = true;
                    }
                    break;
                case MessageBoxResult.No:
                    MainWindow mw = new MainWindow();
                    Hide();
                    mw.Show();
                    break;
            }
        }
        public void CheckWin()
            {
                

                if ((A1.SelectedItem == A2.SelectedItem) && (A2.SelectedItem == A3.SelectedItem)
                    && (A3.SelectedItem == A4.SelectedItem) && (A4.SelectedItem == A5.SelectedItem) && (A1.SelectedIndex != -1))
                {
                    ShowWin(A1);

                }
                else if ((B1.SelectedItem == B2.SelectedItem) && (B2.SelectedItem == B3.SelectedItem)
                    && (B3.SelectedItem == B4.SelectedItem) && (B4.SelectedItem == B5.SelectedItem) && (B1.SelectedIndex != -1))
                {
                    ShowWin(B1);
                }
                else if ((C1.SelectedItem == C2.SelectedItem) && (C2.SelectedItem == C3.SelectedItem)
                    && (C3.SelectedItem == C4.SelectedItem) && (C4.SelectedItem == C5.SelectedItem) && (C1.SelectedIndex != -1))
                {
                    ShowWin(C1);
                }
                else if ((D1.SelectedItem == D2.SelectedItem) && (D2.SelectedItem == D3.SelectedItem)
                    && (D3.SelectedItem == D4.SelectedItem) && (D4.SelectedItem == D5.SelectedItem) && (D1.SelectedIndex != -1))
                {
                    ShowWin(D1);
                }
                else if ((E1.SelectedItem == E2.SelectedItem) && (E2.SelectedItem == E3.SelectedItem)
                    && (E3.SelectedItem == E4.SelectedItem) && (E4.SelectedItem == E5.SelectedItem) && (E1.SelectedIndex != -1))
                {
                    ShowWin(E1);
                }
                else if ((A1.SelectedItem == B1.SelectedItem) && (B1.SelectedItem == C1.SelectedItem)
                    && (C1.SelectedItem == D1.SelectedItem) && (D1.SelectedItem == E1.SelectedItem) && (A1.SelectedIndex != -1))
                {
                    ShowWin(A1);
                }
                else if ((A2.SelectedItem == B2.SelectedItem) && (B2.SelectedItem == C2.SelectedItem)
                    && (C2.SelectedItem == D2.SelectedItem) && (D2.SelectedItem == E2.SelectedItem) && (A2.SelectedIndex != -1))
                {
                    ShowWin(A2);
                }
                else if ((A3.SelectedItem == B3.SelectedItem) && (B3.SelectedItem == C3.SelectedItem)
                    && (C3.SelectedItem == D3.SelectedItem) && (D3.SelectedItem == E3.SelectedItem) && (A3.SelectedIndex != -1))
                {
                    ShowWin(A3);
                }
                else if ((A4.SelectedItem == B4.SelectedItem) && (B4.SelectedItem == C4.SelectedItem)
                    && (C4.SelectedItem == D4.SelectedItem) && (D4.SelectedItem == E4.SelectedItem) && (A4.SelectedIndex != -1))
                {
                    ShowWin(A4);
                }
                else if ((A5.SelectedItem == B5.SelectedItem) && (B5.SelectedItem == C5.SelectedItem)
                    && (C5.SelectedItem == D5.SelectedItem) && (D5.SelectedItem == E5.SelectedItem) && (A5.SelectedIndex != -1))
                {
                    ShowWin(A5);
                }
                else if ((A1.SelectedItem == B2.SelectedItem) && (B2.SelectedItem == C3.SelectedItem)
                    && (C3.SelectedItem == D4.SelectedItem) && (D4.SelectedItem == E5.SelectedItem) && (A1.SelectedIndex != -1))
                {
                    ShowWin(A1);
                }
                else if ((A5.SelectedItem == B4.SelectedItem) && (B4.SelectedItem == C3.SelectedItem)
                    && (C3.SelectedItem == D2.SelectedItem) && (D2.SelectedItem == E1.SelectedItem) && (A5.SelectedIndex != -1))
                {
                    ShowWin(A5);
                }
            
        }

        private void Again_But_Click(object sender, RoutedEventArgs e)
        {
            foreach (ComboBox b in this.MyGrid.Children.OfType<ComboBox>())
            {
                b.SelectedIndex = -1;
                b.IsEnabled = true;
            }
        }
    }
}
