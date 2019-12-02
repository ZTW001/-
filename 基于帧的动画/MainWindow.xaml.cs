using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 基于帧的动画
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private List<EllipseInfo> listEllipse = new List<EllipseInfo>(); //声明一个Ellipse类型的list集合。
        private double acclerationY = 0.1;//Y轴加速度。
        private int minStartingSpeed = 1; //开始时最慢的速度。
        private int maxStartingSpeed = 50; //开始时最快速度。
        private double speedRatio = 0.1;  //速度比率：0.1
        private int minEllipses = 20;   //最少椭圆数量。
        private int maxEllipses = 100;  //最多椭圆数量。
        private int ellipseRadius = 10; //椭圆的半径。
        private bool rendering = false;

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (rendering == false)
            {
                listEllipse.Clear();
                canvas.Children.Clear();
                CompositionTarget.Rendering += CompositionTarget_Rendering;
                rendering = true;
            }
        }

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (listEllipse.Count == 0)
            {
                int halfCanvasWidth = (int)canvas.ActualWidth / 2;
                Random random = new Random();
                int ellipseCount = random.Next(minEllipses, maxEllipses + 1);
                for (int i = 0; i < ellipseCount; i++)
                {
                    Ellipse ellipse = new Ellipse();//创建一个椭圆。                   
                    ellipse.Fill = new SolidColorBrush(Colors.LimeGreen);//设置椭圆颜色。
                    ellipse.Width = ellipseRadius;//设置椭圆的宽。
                    ellipse.Height = ellipseRadius;//设置椭圆的高。
                    Canvas.SetLeft(ellipse, halfCanvasWidth + random.Next(-halfCanvasWidth, halfCanvasWidth)); //设置椭圆位置。
                    Canvas.SetTop(ellipse, 0); //设置椭圆位置。
                    canvas.Children.Add(ellipse); //将创建出来的椭圆加入到canvas面板中。
                    EllipseInfo info = new EllipseInfo(ellipse, speedRatio * random.Next(minStartingSpeed, maxStartingSpeed)); //实例化EllipseInfo。
                    listEllipse.Add(info); //将创建好的EllipseInfo加入到listEllipse集合中。
                }
            }
            else
            {
                for (int i = listEllipse.Count - 1; i >= 0; i--)
                {
                    EllipseInfo info = listEllipse[i]; //得到椭圆。                   
                    double top = Canvas.GetTop(info.Ellipse);//得到top坐标值。
                    Canvas.SetTop(info.Ellipse, top + 1 * info.VelocityY);
                    if (top >= canvas.ActualHeight - ellipseRadius * 2 - 10)
                    {
                        listEllipse.Remove(info);
                    }
                    else
                    {
                        info.VelocityY += acclerationY;
                    }
                    if (listEllipse.Count == 0)
                    {
                        StopRendering();
                    }
                }
            }
        }
        //取消关联事件处理程序。
        private void StopRendering()
        {
            CompositionTarget.Rendering -= CompositionTarget_Rendering;
            rendering = false;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            StopRendering();
        }
    }
}
