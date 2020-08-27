using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 跟随鼠标移动的小球
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        int TimeSet = 75;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var thisTimer = new Timer(TimeSet);
            thisTimer.Elapsed += UpdateMoveDirection;
            thisTimer.Start();
        }

        private void UpdateMoveDirection(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                var MouseLocation = Mouse.GetPosition(ThisCanvas);
                if (MouseLocation.X < 0 || MouseLocation.Y < 0) return;
                var MouseVector = Vector.Parse(MouseLocation.ToString());
                var TargetVector = Vector.Parse(Target.Center.ToString());
                var DirectionVector = MouseVector - TargetVector;
                if (DirectionVector.Length < 5) return;
                DirectionVector.Normalize();
                var thisPointAnimation = new PointAnimation()
                {
                    Duration = new Duration(TimeSpan.FromMilliseconds(TimeSet)),
                    By = new Point(DirectionVector.X * 20, DirectionVector.Y * 20)
                };
                Target.BeginAnimation(EllipseGeometry.CenterProperty, thisPointAnimation);
            }));
        }
    }
}
