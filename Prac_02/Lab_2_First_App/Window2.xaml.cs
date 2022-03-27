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
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Math;

namespace Lab_2_First_App
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        static DispatcherTimer dT;
        static int Radius = 30;
        static int PointCount = 5;
        static Polygon myPolygon = new Polygon();
        static List<Ellipse> EllipseArray = new List<Ellipse>();
        static PointCollection pC = new PointCollection();
        static List<double> dist = new List<double>();
        static List<double> dist_ind = new List<double>();
        List<int> indexes = new List<int>();
        static List<int> way = new List<int>();
        static List<int> way_add = new List<int>();
        static int index1 = 0;
        static int g = 0;
        public Window2()
        {
            dT = new DispatcherTimer();
            InitializeComponent();
            InitPoints();
            InitPolygon();
            dT = new DispatcherTimer();
            dT.Tick += new EventHandler(OneStep);
            dT.Interval = new TimeSpan(0, 0, 0, 0, 1000);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wn = new MainWindow();
            Hide();
            wn.Show();
        }

        private void NumElemCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox CB = (ComboBox)e.Source;
            ListBoxItem item = (ListBoxItem)CB.SelectedItem;

            PointCount = Convert.ToInt32(item.Content);
            InitPoints();
            InitPolygon();
        }

        private void StopStart_Click(object sender, RoutedEventArgs e)
        {
            if (dT.IsEnabled)
            {
                dT.Stop();
                NumElemCB.IsEnabled = true;

            }
            else
            {
                g = 0;
                way_add.Clear();
                indexes.Clear();
                MyCanvas.Children.Clear();
                NumElemCB.IsEnabled = false;
                dT.Start();
                InitPoints();
                InitPolygon();
                way = Generate_way();
                for (int i = 0; i < way.Count; i++)
                {
                    way_add.Add(way[i]);
                }
            }
        }

        private void VelCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox CB = (ComboBox)e.Source;
            ListBoxItem item = (ListBoxItem)CB.SelectedItem;

            dT.Interval = new TimeSpan(0, 0, 0, 0, Convert.ToInt16(item.Content));
        }

        private void InitPoints()
        {
            Random rnd = new Random();
            pC.Clear();
            EllipseArray.Clear();


            for (int i = 0; i < PointCount; i++)
            {
                Point p = new Point();

                p.X = rnd.Next(Radius, (int)(0.75 * MainWin.Width) - 3 * Radius);
                p.Y = rnd.Next(Radius, (int)(0.90 * MainWin.Height - 3 * Radius));
                pC.Add(p);
            }

            for (int i = 0; i < PointCount; i++)
            {
                Ellipse el = new Ellipse();

                el.StrokeThickness = 2;
                el.Height = el.Width = Radius;
                el.Stroke = Brushes.Yellow;
                el.Fill = Brushes.Red;
                EllipseArray.Add(el);
            }
        }

        private void OneStep(object sender, EventArgs e)
        {
            MyCanvas.Children.Clear();
            PlotPoints();
            PlotWay(GetBestWay());
            if (g < PointCount - 1)
            {
                g++;

            }
        }

        private List<int> Generate_way()
        {
            Random rnd = new Random();
            List<int> way = new List<int>();
            for (int i = 0; i < PointCount; i++)
                way.Add(i);
            for (int s = 0; s < 2 * PointCount; s++)
            {
                int i1, i2, tmp;
                i1 = rnd.Next(PointCount);
                i2 = rnd.Next(PointCount);
                tmp = way[i1];
                way[i1] = way[i2];
                way[i2] = tmp;
            }
            for (int i = 0; i < way.Count; i++)
            {

            }
            return way;
        }

        private List<int>GetBestWay()
        {
            double d = 0;
            for (int j = 0; j < way.Count; j++)
            {
                if (j == index1)
                {
                    continue;
                }
                d = Convert.ToDouble(Sqrt(Pow((pC[way[index1]].X - pC[way[j]].X), 2)
                    + Pow((pC[way[index1]].Y - pC[way[j]].Y), 2)));
                dist.Add(Round(d, 3));
                dist_ind.Add(Round(d, 3));
            }

            if (way.Count > 0)
            {
                indexes.Add(way[index1]);
                way.RemoveAt(index1);
            }

            if (dist.Count != 0)
            {
                dist.Sort();
                index1 = dist_ind.IndexOf(dist[0]);
                dist.Clear();
                dist_ind.Clear();
            }

            way_add.Insert(g, indexes[g]);
            way_add.RemoveAt(g + 1);

            if (g == PointCount - 1)
            {
                way_add.Add(way_add[0]);
            }
            return way_add;
        }

        private void InitPolygon()
        {
            myPolygon.Stroke = System.Windows.Media.Brushes.Black;
            myPolygon.StrokeThickness = 2;
        }

        private void PlotPoints()
        {
            for (int i = 0; i < PointCount; i++)
            {
                Canvas.SetLeft(EllipseArray[i], pC[i].X - Radius / 2);
                Canvas.SetTop(EllipseArray[i], pC[i].Y - Radius / 2);
                MyCanvas.Children.Add(EllipseArray[i]);
            }
        }

        private void PlotWay(List<int> BestWayIndex)
        {
            PointCollection Points = new PointCollection();
            for (int i = 0; i < BestWayIndex.Count; i++)
                Points.Add(pC[BestWayIndex[i]]);
            myPolygon.Points = Points;
            MyCanvas.Children.Add(myPolygon);
        }
    }
}
