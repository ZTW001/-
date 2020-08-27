using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace FlowerCurve
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point _bigCircleCenter = new Point(400, 400);
        private double _bigRadius = 400;
        private double _smallRadius = 260;
        private double _d = 110;
        DispatcherTimer timer = new DispatcherTimer();
        private double _angle = 0;
        private PathFigure _figure;
        private double _step = 0.05D;
        List<Point> _points = new List<Point>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Draw(PathFigure figure, Point point, bool isStartPoint)
        {
            if (isStartPoint)
            {
                figure.StartPoint = point;
            }
            else
            {
                figure.Segments.Add(new LineSegment(point, true));
            }
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Interval = TimeSpan.FromMilliseconds(5);
            timer.Tick += Timer_Tick;
            timer.Start();

            PathGeometry curve = new PathGeometry();
            path.Data = curve;
            //curve.StartPoint = new Point(_bigRadius - _smallRadius - _d, 0);
            //curve.mo
            _figure = new PathFigure();
            curve.Figures.Add(_figure);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Point p = Calculate(_angle);
            Draw(_figure, p, _angle == 0);
            _points.Add(p);
            if (_points.Count > 10)
            {
                bool finished = Math.Abs(p.X - _points[0].X) < 2 && Math.Abs(p.Y - _points[0].Y) < 2;
                Debug.WriteLine($"{_points[0].X - p.X} || {_points[0].Y - p.Y}");
                if (finished)
                {
                    MessageBox.Show("绘制完成");
                    timer.Stop();
                }
            }

            _angle += _step;
        }

        private Point Calculate(double angle)
        {
            double xr = _bigCircleCenter.X + (_bigRadius - _smallRadius) * Math.Cos(angle);
            double yr = _bigCircleCenter.Y - (_bigRadius - _smallRadius) * Math.Sin(angle);
            double a = _bigRadius * angle / _smallRadius;
            double x = xr + _d * Math.Cos(a);
            double y = yr + _d * Math.Sin(a);
            Point p = new Point(x, y);

            return p;
        }
    }
}