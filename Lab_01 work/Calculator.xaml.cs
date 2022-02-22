using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Lab_01_work
{
    /// <summary>
    /// Логика взаимодействия для Calculator.xaml
    /// </summary>
    public partial class Calculator : Window
    {
        public Calculator()
        {
            InitializeComponent();
            foreach (Button b in this.myGrid.Children.OfType<Button>())
            {
                b.Click += But_Click;
            }
        }

        private void But_Click(object sender, RoutedEventArgs e)
        {
            string str = "";
            string text = ((Button)e.OriginalSource).Content.ToString();
            if (text == "C")
                TB.Text = "";
            else if (text == "=")
            {
                str = new DataTable().Compute(TB.Text, null).ToString();
                TB.Text = str;
            }
            else if (text == "⌫")
            {
                if (TB.Text != "")
                {
                    string ToDel = TB.Text;
                    ToDel = ToDel.Remove(ToDel.Length - 1);
                    TB.Text = ToDel; ;
                }
                else
                {
                    TB.Text = "";
                }

            }
            else
                TB.Text += text;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }


    }
}
