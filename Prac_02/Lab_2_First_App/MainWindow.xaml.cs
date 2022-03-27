using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Math;

namespace Lab_2_First_App
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static DispatcherTimer dT;
        static int Radius = 20; 
        static int PointCount; //к-ть населених пунктів
        static int ParentCount = 5; // к-сть хромосом
        static int NPopulation = 5; // к-сть популяцій
        static double mutation;
        static List<List<int>> Population = new List<List<int>>();
        static Polygon myPolygon = new Polygon();
        static List<double> dist = new List<double>();
        static List<double> dist_ind = new List<double>();
        static double BestWay;
        static List<Ellipse> EllipseArray = new List <Ellipse>();
        static PointCollection pC = new PointCollection();
        static Random rnd = new Random();

        public MainWindow()
        {
            dT = new DispatcherTimer();

            InitializeComponent();
            InitPoints();
            InitPolygon();
            
            dT = new DispatcherTimer();
            dT.Tick += new EventHandler(OneStep);
            dT.Interval = new TimeSpan(0, 0, 0, 0, 1000);
           
            
        }
        private void Generate_First_Population()
        {
            //Get_Populations();
            for (int i = 0; i < ParentCount; i++)
            {
                Population.Add(Get_Parent());
            }
           
            
            
        }
        private void InitPoints()
        {
            Random rnd = new Random();
            pC.Clear();
            EllipseArray.Clear();

            for (int i = 0; i < PointCount; i++)
            {
                Point p = new Point();

                p.X = rnd.Next(Radius, (int)(0.75*MainWin.Width)-3*Radius);
                p.Y = rnd.Next(Radius, (int)(0.90*MainWin.Height-3*Radius));                
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

        
        private void InitPolygon()
        {
            myPolygon.Stroke = System.Windows.Media.Brushes.Black;            
            myPolygon.StrokeThickness = 2;            
        }

        private void PlotPoints()
        {            
            for (int i=0; i<PointCount; i++)
            {
                Canvas.SetLeft(EllipseArray[i], pC[i].X - Radius/2);
                Canvas.SetTop(EllipseArray[i], pC[i].Y - Radius/2);
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

        private void VelCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox CB = (ComboBox)e.Source;
            ListBoxItem item = (ListBoxItem)CB.SelectedItem;
                        
            dT.Interval = new TimeSpan(0,0,0,0,Convert.ToInt16(item.Content));
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
                MyCanvas.Children.Clear();               
                NumElemCB.IsEnabled = false;
                mutation = Convert.ToDouble(Mutation_Field.Text);
                dT.Start();
                InitPoints();
                InitPolygon();
                Population.Clear();
                Generate_First_Population();
                
            }
            
        }

        private void NumElemCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox CB = (ComboBox)e.Source;
            ListBoxItem item = (ListBoxItem)CB.SelectedItem;

            PointCount = Convert.ToInt32(item.Content);
            InitPoints();
            InitPolygon();
        }

        private void make_child()
        {
            List<int> tmp1 = new List<int>();
            List<int> tmp2 = new List<int>();
            // 2 random parents
            int i1 = rnd.Next(NPopulation - 1);
            int i2 = rnd.Next(NPopulation - 1);
            if (i1 == i2)
            {
                i2++;
            }
            List<int> p1 = Population[i1];
            List<int> p2 = Population[i2];

            //crossover
            int cross = rnd.Next(1, PointCount - 2);

            for (int i = 0; i <= cross; i++)
            {
                tmp1.Add(p1[i]);
                tmp2.Add(p2[i]);
            }
            for (int i = cross + 1; i < PointCount; i++)
            {
                tmp1.Add(p2[i]);
                tmp2.Add(p1[i]);
            }
            List<int> ch1 = new List<int>();
            List<int> ch2 = new List<int>();

            for (int i = 0; i < PointCount; i++)
            {
                ch1.Add(tmp1[i]);
                ch2.Add(tmp2[i]);
            }

            for (int i = 0; i < PointCount; i++)
            {
                ch1.Add(tmp2[i]);
                ch2.Add(tmp1[i]);
            }

            ch1 = make_uniq(ch1); // uniq elements
            ch2 = make_uniq(ch2);

            if (rnd.NextDouble() > 0.5)
            {
                if (rnd.NextDouble() < mutation)
                {
                    Mutate(ch1);
                }
                Population.Add(ch1);

            }
            else
            {
                if (rnd.NextDouble() < mutation)
                {
                    Mutate(ch2);
                }
                Population.Add(ch2);
            }
        }
        public List<int> make_uniq(List<int> list)
        {
            List<int> uniq = new List<int>();
            var uniq_items = new HashSet<int>(list);
            foreach (int i in uniq_items)
            {
                uniq.Add(i);
            }
            return uniq;
        }

        private List<int> Mutate(List<int> list) //mutation
        {
            List<int> temp = new List<int>();
            int i1 = rnd.Next(PointCount - 1);
            int i2 = rnd.Next(PointCount - 1);

            if ((i1 == i2) && (i1 != PointCount - 1))
            {
                i2++;
            }else if ((i1 == i2) && (i1 == PointCount - 1)){
                i2--;
            }
            int index1 = Math.Min(i1, i2);
            int index2 = Math.Max(i1, i2);
            for (int i = index1; i <= index2; i++)
            {
                temp.Add(list[i]);
            }
            temp.Reverse();
            int j = 0;
            for (int i = index1; i <= index2; i++)
            {

                list[i] = temp[j];
                j++;
            }
            return list;

        }
        
        private void OneStep(object sender, EventArgs e)
        {
            List<int> indexes = new List<int>();
            for (int i = 0; i < ParentCount; i++)
            {
                make_child();
            }
            for (int i = 0; i < 2*ParentCount; i++)
            {
                GetWay(Population[i]);
            }
            dist.Sort();
            BestWay = dist[0];
            dst.Content = "Distance: " + BestWay;
            for (int i = 0; i < ParentCount; i++)
            {
                indexes.Add(dist_ind.IndexOf(dist[i]));
            }
            MyCanvas.Children.Clear();
            PlotPoints();
            PlotWay(Population[0]);
            
            for (int i = 0; i < ParentCount; i++)
            {
                Population[i] = Population[indexes[i]];
            }
            Population.RemoveRange(ParentCount, ParentCount);
            dist_ind.Clear();
            dist.Clear();
        }

        private List<int> Get_Parent()
        {
            List<int> way = new List<int>();
            for (int j = 0; j < PointCount; j++)
                way.Add(j);
            int i1, i2, tmp;
            for (int s = 0; s < 2 * PointCount; s++)
            {
                i1 = rnd.Next(PointCount);
                i2 = rnd.Next(PointCount);
                tmp = way[i1];
                way[i1] = way[i2];
                way[i2] = tmp;
            }
            return way;
        }

        private void GetWay(List<int> way)
        {
            double d = 0;
            int index;
            for (int i = 0; i < way.Count; i++)
            {
                index = i + 1;
                if (i == way.Count - 1)
                {
                    index = 0;
                }
                d += Convert.ToDouble(Sqrt(Pow((pC[way[i]].X - pC[way[index]].X), 2)
                    + Pow((pC[way[i]].Y - pC[way[index]].Y), 2)));
            }
            dist.Add(Round(d, 3));
            dist_ind.Add(Round(d, 3));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window2 wn = new Window2();
            Hide();
            wn.Show();
        }
    }
}