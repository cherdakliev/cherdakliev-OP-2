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

namespace Tournament_DB
{
    class Design
    {
        LinearGradientBrush Gradient_1 = new LinearGradientBrush();
        LinearGradientBrush Gradient_2 = new LinearGradientBrush();
        public LinearGradientBrush LinearGrad_2()
        {
            var bc = new ColorConverter();
            Gradient_2.StartPoint = new Point(0, 0);
            Gradient_2.EndPoint = new Point(0, 1);

            GradientStop col1 = new GradientStop();
            col1.Color = (Color)bc.ConvertFrom("#FF1C2B46");
            col1.Offset = 0.0006;
            Gradient_2.GradientStops.Add(col1);

            GradientStop col2 = new GradientStop();
            col2.Color = (Color)bc.ConvertFrom("#FF101317");
            col2.Offset = 1.0;
            Gradient_2.GradientStops.Add(col2);

            GradientStop col3 = new GradientStop();
            col3.Color = (Color)bc.ConvertFrom("#FF12161C");
            col3.Offset = 0.87;
            Gradient_2.GradientStops.Add(col3);

            return Gradient_2;
        }

        public LinearGradientBrush LinearGrad_1()
        {
            var bc = new ColorConverter();
            Gradient_1.StartPoint = new Point(0, 0);
            Gradient_1.EndPoint = new Point(0, 1);

            GradientStop col1 = new GradientStop();
            col1.Color = (Color)bc.ConvertFrom("#FF1C2B46");
            col1.Offset = 0.0006;
            Gradient_1.GradientStops.Add(col1);

            GradientStop col2 = new GradientStop();
            col2.Color = (Color)bc.ConvertFrom("#FF101317");
            col2.Offset = 0.664;
            Gradient_1.GradientStops.Add(col2);

            GradientStop col3 = new GradientStop();
            col3.Color = (Color)bc.ConvertFrom("#FF12161C");
            col3.Offset = 0.87;
            Gradient_1.GradientStops.Add(col3);

            return Gradient_1;
        }
    }
}
