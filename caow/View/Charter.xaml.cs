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
using System.Collections.ObjectModel;

namespace caow.View
{
    /// <summary>
    /// Logika interakcji dla klasy Charter.xaml
    /// </summary>
    public partial class Charter : UserControl
    {
        public static readonly DependencyProperty CurrentDataPoints = DependencyProperty.Register(nameof(DataPoints), typeof(ObservableCollection<int>), typeof(Charter), new FrameworkPropertyMetadata(null));
        public ObservableCollection<int> DataPoints
        {
            get
            {
                Console.WriteLine("xdd");
                return (ObservableCollection<int>)GetValue(CurrentDataPoints);
            }
            set
            {
                Console.WriteLine("xda");
                SetValue(CurrentDataPoints, value);
                RefreshChart();
            }
        }
        private void ClearCanvas() => GraphCanvas.Children.Clear();
        private void DrawChart()
        {
            int[] values = { 100, 5, 5, 6, 7, 19, 21, 50, 60, 45, 100, 5, 5, 6, 7, 19, 21, 50, 60, 45, 100, 5, 5, 6, 7, 19, 21, 50, 60, 45 };
            PointCollection points = new PointCollection();
            double step = GraphCanvas.ActualWidth / values.Length;
            double xd = GraphCanvas.ActualHeight;
            for (int x = 0; x < values.Length; x++)
            {
                double y = ((Convert.ToDouble(values[x]) / 100.0) * /*xd*/ 100);
                points.Add(new Point(Convert.ToDouble(x)*10, y));
            }
            Console.WriteLine(points);
            Polyline polyline = new Polyline();
            polyline.StrokeThickness = 2;
            polyline.Stroke = Brushes.Green;
            polyline.Points = points;
            GraphCanvas.Children.Add(polyline);
        }
        public void RefreshChart()
        {
            Console.WriteLine("xd");
            ClearCanvas();
            DrawChart();
        }
        public Charter()
        {
            InitializeComponent();
            RefreshChart();
        }
    }
}
