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
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace caow.View
{
    /// <summary>
    /// Logika interakcji dla klasy Charter.xaml
    /// </summary>
    public partial class Charter : UserControl
    {
        public static readonly DependencyProperty CurrentDataPoints = DependencyProperty.Register(nameof(DataPoints), typeof(ObservableCollection<int>), typeof(Charter), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty CurrentPlotColor = DependencyProperty.Register(nameof(PlotColor), typeof(Color), typeof(Charter), new FrameworkPropertyMetadata(null));
        public ObservableCollection<int> DataPoints
        {
            get
            {
                return (ObservableCollection<int>)GetValue(CurrentDataPoints);
            }
            set
            {
                SetValue(CurrentDataPoints, value);
                RefreshChart();
            }
        }
        public Color PlotColor
        {
            get
            {
                return (Color)GetValue(CurrentPlotColor);
            }
            set
            {
                SetValue(CurrentPlotColor, value);
            }
        }
        private double VDividerBarOffset = 0;
        private void ClearCanvas() => GraphCanvas.Children.Clear();
        private void DrawChart()
        {
            if (DataPoints == null)
                return;
            double step = GraphCanvas.ActualWidth / DataPoints.Count;
            double height = GraphCanvas.ActualHeight;
            double ystep = height / 4;
            string[] txt = new string[]{"25%","50%","75%"};
            //draw dividers
            for(int i = 1; i < 4; i++)
            {
                Line line = new Line();
                line.X1 = 0;
                line.X2 = GraphCanvas.ActualWidth;
                line.Y1 = ystep * i;
                line.Y2 = ystep * i;
                line.Stroke = Brushes.Gray;
                line.StrokeThickness = 1;
                GraphCanvas.Children.Add(line);
                /* text for l8r?
                TextBlock textBlock = new TextBlock();
                textBlock.Text = txt[i-1];
                Canvas.SetLeft(textBlock, 200);
                Canvas.SetTop(textBlock, line.Y2);
                GraphCanvas.Children.Add(textBlock);
                */
            }
            for(int i = 0; i <= (DataPoints.Count/5)+1; i++)
            {
                Line line = new Line();
                line.X1 = (step*5*i)- VDividerBarOffset*step;
                line.X2 = (step*5*i) - VDividerBarOffset*step;
                if (line.X1 < 0 || line.X1 > GraphCanvas.ActualWidth)
                    continue;
                line.Y1 = 0;
                line.Y2 = height;
                line.Stroke = Brushes.Gray;
                line.StrokeThickness = 1;
                GraphCanvas.Children.Add(line);
            }
            if (VDividerBarOffset < 5)
                VDividerBarOffset++;
            else
                VDividerBarOffset = 1;
            //draw CPU usage line
            PointCollection points = new PointCollection();
            for (int x = 0; x < DataPoints.Count; x++)
            {
                double y = height - ((Convert.ToDouble(DataPoints[x]) / 100.0) * height);
                points.Add(new Point(Convert.ToDouble(x)*step, y));
            }
            Polyline polyline = new Polyline();
            polyline.StrokeThickness = 2;
            polyline.Stroke = new SolidColorBrush(PlotColor);
            polyline.Points = points;
            GraphCanvas.Children.Add(polyline);
        }
        public void RefreshChart()
        {
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
