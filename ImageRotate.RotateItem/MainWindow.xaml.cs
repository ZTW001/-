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

namespace ImageRotate.RotateItem
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public partial class RotateItem : UserControl
        {
            private bool IsMouseCaptured;
            private Point MousePosition;
            private Point LastPosition;
            public Point CanvasCenter;
            private double LastAngle;
            private double CurrentAngle;
            private double AngleDelta;

            public RotateItem()
            {
                InitializeComponent();
                Handle.MouseLeftButtonDown += new MouseButtonEventHandler(Handle_MouseLeftButtonDown);
                Handle.MouseLeftButtonUp += new MouseButtonEventHandler(Handle_MouseLeftButtonUp);
                Handle.MouseMove += new MouseEventHandler(Handle_MouseMove);
            }

            private void Handle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
            {
                FrameworkElement Item = sender as FrameworkElement;
                Item.ReleaseMouseCapture();
                IsMouseCaptured = false;
                Item.Cursor = null;
            }

            private void Handle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                FrameworkElement Item = sender as FrameworkElement;
                Item.CaptureMouse();
                Item.Cursor = Cursors.Hand;
                IsMouseCaptured = true;
                LastPosition = e.GetPosition(null);
            }

            private void Handle_MouseMove(object sender, MouseEventArgs e)
            {
                MousePosition = e.GetPosition(null);

                if (IsMouseCaptured)
                {
                    LastAngle = Math.Atan2(LastPosition.Y - CanvasCenter.Y, LastPosition.X - CanvasCenter.X);
                    CurrentAngle = Math.Atan2(MousePosition.Y - CanvasCenter.Y, MousePosition.X - CanvasCenter.X);
                    AngleDelta = CurrentAngle - LastAngle;
                    RotateItemCanvas.Angle += RadiansToDegrees(AngleDelta);
                    LastPosition = MousePosition;
                }
            }


            /// <summary>
            /// 弧度转化为角度
            /// </summary>
            /// <param name="Radians"></param>
            /// <returns></returns>
            private double RadiansToDegrees(double Radians)
            {
                return Radians * 180 / Math.PI;
            }
        }


    }
