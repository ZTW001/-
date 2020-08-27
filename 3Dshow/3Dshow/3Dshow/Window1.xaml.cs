using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Media.Media3D;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Diagnostics;
using System.Threading;
using System.Collections;

namespace _3Dshow
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : System.Windows.Window
    {
        #region 全局函数
        // Queue<double>[,] sum_queue;
        double[,] sum_queue = new double[5, 2];
        PerspectiveCamera _camera = null;
        bool ismousedown = false;
        // TimeSpan timspan;
        Point startPoint, currentPoint, endPoint;

        string[] fileList;
        double[,] sum_list;
        int _b = 0;
        DispatcherTimer sub_time = new DispatcherTimer();
        DispatcherTimer sub_time1 = new DispatcherTimer();
        private static DependencyProperty TranslatePosProperty = DependencyProperty.Register(
            "TranslatePos", typeof(double), typeof(Window1), new PropertyMetadata((double)0, new PropertyChangedCallback(DragPosChanged)));
        #endregion

        static void DragPosChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Window1 win = obj as Window1;
            win.PosChanged((double)e.NewValue);
        }

        public Window1()
        {
            InitializeComponent();
            IniComponent();
        }

        private void IniComponent()
        {
            this.GetUserImages();
            _camera = new PerspectiveCamera();
            _camera.Position = new Point3D(0, 0, 10);
            _camera.LookDirection = new Vector3D(0, -0.1, -5);
            _camera.UpDirection = new Vector3D(0, 1, 0);
            _camera.FieldOfView = 45;
            _view.Camera = _camera;
            getview();
            inittran();
        }

        #region  read picture
        private void GetUserImages()
        {
            int a = 5;
            fileList = readFiles(System.IO.Directory.GetCurrentDirectory() + @"\image");
            int b = fileList.Length;
            if (b != 0)
                sum_list = new double[b, a];
        }

        private string[] readFiles(string path)
        {
            return System.IO.Directory.GetFiles(path, "*.jpg");
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化图片
        /// </summary>
        private void getview()
        {
            if (fileList.Length < 1)
                return;
            for (int i = 0; i < fileList.Length; i++)
            {
                ModelVisual3D _visual = new ModelVisual3D();
                BitmapImage bmp;
                ImageSource img;
                DiffuseMaterial dm = new DiffuseMaterial();
                GeometryModel3D geometrymodel3d = new GeometryModel3D();
                Model3DGroup model3dgroupmodel = new Model3DGroup();
                MeshGeometry3D MeshGeometry3D1 = new MeshGeometry3D();
                model3dgroupmodel.Children.Add(geometrymodel3d);
                bmp = new BitmapImage(new Uri(fileList[i]));
                img = bmp;
                dm.Brush = new ImageBrush((ImageSource)(img));
                geometrymodel3d.Material = dm;
                model3dgroupmodel.Children.Add(geometrymodel3d);

                #region light
                MeshGeometry3D1 = new MeshGeometry3D();
                MeshGeometry3D1.Positions = ((Point3DCollection)new Point3DCollectionConverter().ConvertFromString("-1,1,1 -1,-1,1 1,-1,1 1,1,1"));
                MeshGeometry3D1.TriangleIndices = ((Int32Collection)new Int32CollectionConverter().ConvertFromString("0,1,3 1,2,3"));
                MeshGeometry3D1.TextureCoordinates = ((PointCollection)new PointCollectionConverter().ConvertFromString("0,0 0,1 1,1 1,0"));
                geometrymodel3d.Geometry = MeshGeometry3D1;

                AmbientLight AmbientLight1 = new AmbientLight();
                AmbientLight1.Color = Colors.Gray;
                model3dgroupmodel.Children.Add(AmbientLight1);

                DirectionalLight DirectionalLight1 = new DirectionalLight();
                DirectionalLight1.Color = Colors.Gray;
                DirectionalLight1.Direction = ((Vector3D)new Vector3DConverter().ConvertFromString("-1,-3,-2"));
                model3dgroupmodel.Children.Add(DirectionalLight1);

                DirectionalLight1 = new DirectionalLight();
                DirectionalLight1.Color = Colors.Gray;
                DirectionalLight1.Direction = ((Vector3D)new Vector3DConverter().ConvertFromString("1,-2,3"));
                model3dgroupmodel.Children.Add(DirectionalLight1);
                #endregion

                _visual.Content = model3dgroupmodel;

                _view.Children.Add(_visual);
            }
        }
        //c is angel
        //d is scale
        //b is x-translate
        //a is z-translate
        //double _a, _b, _c, _d;
        /// <summary>
        /// 初始化图片的位置位移以及大小偏转
        /// </summary>
        private void inittran()
        {
            double a, b, c, d;
            for (int i = 0; i < _view.Children.Count; i++)
            {
                #region 调整位置
                a = 0;
                b = (-3 + i) * 1.5;
                sum_list[i, 1] = b;
                if (i == 0 || i == 6)
                {
                    d = 0.4;
                }
                else if (i == 1 || i == 5)
                {
                    d = 0.6;
                }
                else if (i == 2 || i == 4)
                {
                    d = 0.8;
                }
                else
                {
                    d = 1; ;
                }
                sum_list[i, 3] = d;
                c = 120 - (40 * i);
                sum_list[i, 2] = c;
                #endregion

                Transform3DGroup tg = new Transform3DGroup();
                tg.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), c)));
                tg.Children.Add(new ScaleTransform3D(d, d, d));
                tg.Children.Add(new TranslateTransform3D(b, 0, a));
                _view.Children[i].Transform = tg;
            }

            #region sum_list region
            sum_list[0, 4] = -2;
            sum_list[1, 4] = -2;
            sum_list[2, 4] = -2;
            sum_list[3, 4] = 1;
            sum_list[4, 4] = 2;
            sum_list[5, 4] = 2;
            sum_list[6, 4] = 2;
            #endregion
        }
        #endregion

        #region temp
        double _pos = 0;

        void PosChanged(double pos)
        {

            double delta = pos - _pos;
            _pos = pos;
            GetTran(delta);
        }
        #endregion

        #region 返回位置transtate

        private void GetTran(double sum)
        {

            for (int i = 0; i < _view.Children.Count; i++)
            {
                #region point move
                #region 调整位置

                sum_list[i, 1] += sum / 270;

                //angel
                if (sum > 0)
                {
                    if (sum_list[i, 4] == -2)
                    {
                        sum_list[i, 3] += sum / 2000;
                    }
                    else if (sum_list[i, 4] == 0)
                    {
                        sum_list[i, 3] -= sum / 2000;
                    }
                    else if (sum_list[i, 4] == 2)
                    {
                        sum_list[i, 3] -= sum / 2000;
                    }

                }
                else
                {
                    if (sum_list[i, 4] == -2)
                    {
                        sum_list[i, 3] += sum / 2000;
                    }
                    else if (sum_list[i, 4] == 0)
                    {
                        sum_list[i, 3] += sum / 2000;
                    }
                    else if (sum_list[i, 4] == 2)
                    {
                        sum_list[i, 3] -= sum / 2000;
                    }

                }


                if (sum > 0)
                {
                    if (sum_list[i, 2] == 0)
                    {
                        sum_list[i, 4] = 2;
                    }
                    sum_list[i, 2] -= sum / 10;
                    if (sum_list[i, 2] == 0)
                    {
                        sum_list[i, 4] = 0;
                    }

                }
                else if (sum < 0)
                {
                    if (sum_list[i, 2] == 0)
                    {
                        sum_list[i, 4] = -2;
                    }
                    sum_list[i, 2] -= sum / 10;
                    if (sum_list[i, 2] == 0)
                    {
                        sum_list[i, 4] = 0;
                    }

                }

                #endregion
                Transform3DGroup tg = new Transform3DGroup();
                tg.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), sum_list[i, 2])));
                tg.Children.Add(new ScaleTransform3D(sum_list[i, 3], sum_list[i, 3], sum_list[i, 3]));
                tg.Children.Add(new TranslateTransform3D(sum_list[i, 1], 0, sum_list[i, 0]));

                _view.Children[i].Transform = tg;
                #endregion
            }
            Volidating();
        }
        #endregion

        #region 校验
        private void Volidating()
        {
            for (int i = 0; i < _view.Children.Count; i++)
            {
                if (sum_list[i, 3] >= 1)
                {
                    sum_list[i, 1] = 0;
                    sum_list[i, 3] = 1;
                    sum_list[i, 2] = 0;
                    sum_list[i, 4] = 0;
                    int sum = 1;
                    for (int j = i - 1; j >= 0; j--)
                    {
                        sum_list[j, 3] = 1 - (0.2) * sum;

                        if (sum_list[j, 3] <= 0)
                        {
                            sum_list[j, 3] = 0;
                        }
                        sum_list[j, 2] = 40 * sum;
                        sum_list[j, 1] = -1.5 * sum;
                        sum++;
                    }
                    sum = 1;
                    for (int j = i + 1; j < _view.Children.Count; j++)
                    {
                        sum_list[j, 3] = 1 - (0.2) * sum;

                        if (sum_list[j, 3] <= 0)
                        {
                            sum_list[j, 3] = 0;
                        }
                        sum_list[j, 2] = 40 * sum * (-1);
                        sum_list[j, 1] = 1.5 * sum;
                        sum++;
                    }

                    break;
                }
            }
        }
        #endregion

        #region mouse
        private void _view_mousedown(object sender, MouseButtonEventArgs e)
        {
            if (sub_time != null)
            {
                sub_time.Stop();
            }
            ismousedown = true;
            startPoint = new Point(Mouse.GetPosition(sender as IInputElement).X, Mouse.GetPosition(sender as IInputElement).Y);
        }

        private void _view_mousemove(object sender, MouseEventArgs e)
        {
            if (!ismousedown)
                return;
            endPoint = e.GetPosition(sender as IInputElement);
            for (int i = 4; i > 0; i--)
            {
                sum_queue[i, 1] = sum_queue[i - 1, 1];
            }
            sum_queue[0, 1] = endPoint.X;
            double _sum = (endPoint.X - startPoint.X);
            GetTran(_sum);
            currentPoint.X = startPoint.X;
            currentPoint.Y = startPoint.Y;
            startPoint.X = endPoint.X;
            startPoint.Y = endPoint.Y;
            sub_time1.Tick += new EventHandler(Tick_Event1);
            sub_time1.Interval = TimeSpan.FromMilliseconds(60);
            sub_time1.Start();
            for (int i = 4; i > 0; i--)
            {
                sum_queue[i, 1] = sum_queue[i - 1, 1];
            }
            sum_queue[0, 1] = endPoint.X;
        }
        double x = 0;
        private void Tick_Event(object sender, EventArgs args)
        {
            double _a = 0;
            _b = 0;
            _a = sum_list[0, 3];
            for (int u = 1; u < _view.Children.Count; u++)
            {
                if (sum_list[u, 3] > _a)
                {
                    _a = sum_list[u, 3];
                    _b = u;
                }
            }
            if (x > 0)
            {
                if (x < 1E-3)
                {
                    sub_time.Stop();
                    for (int i = 4; i >= 0; i--)
                    {
                        sum_queue[i, 1] = 0;
                    }
                    if (sum_list[_b, 4] == 2)
                    {

                        SetAnimation(true, (1 - sum_list[_b, 3]) * 2000);
                    }
                    else if ((sum_list[_b, 4] == -2))
                    {

                        SetAnimation(false, (sum_list[_b, 3] - 1) * 2000);
                    }
                }
                else
                {
                    GetTran(x);
                    x *= 0.9;
                }
            }
            else
            {
                if (x > -1E-3)
                {
                    sub_time.Stop();
                    for (int i = 4; i >= 0; i--)
                    {
                        sum_queue[i, 1] = 0;
                    }
                    if (sum_list[_b, 4] == 2)
                    {

                        SetAnimation(true, (1 - sum_list[_b, 3]) * 2000);
                    }
                    else if ((sum_list[_b, 4] == -2))
                    {

                        SetAnimation(false, (sum_list[_b, 3] - 1) * 2000);
                    }
                }
                else
                {
                    GetTran(x);
                    x *= 0.9;
                }
            }

        }
        private void Tick_Event1(object sender, EventArgs args)
        {

            for (int i = 4; i > 0; i--)
            {
                sum_queue[i, 1] = sum_queue[i - 1, 1];
            }
            sum_queue[0, 1] = endPoint.X;
            sub_time1.Stop();
        }

        private double retdis()
        {
            double sum = 0;
            sum = (sum_queue[0, 1] - sum_queue[4, 1]) * 3;
            return sum;
        }
        private void _view_mouseup(object sender, MouseButtonEventArgs e)
        {
            ismousedown = false;
            endPoint = new Point(Mouse.GetPosition(sender as IInputElement).X, Mouse.GetPosition(sender as IInputElement).Y);
            double _sum = (endPoint.X - currentPoint.X);

            #region 残影
            ismousedown = false;
            x = retdis();
            sub_time.Tick += new EventHandler(Tick_Event);
            sub_time.Interval = TimeSpan.FromMilliseconds(30);
            sub_time.Start();
            #endregion
        }

        #endregion

        /// <summary>
        /// 设置动画
        /// </summary>
        /// <param name="_tf">bool的判断</param>
        /// <param name="_sum">动画的偏移量</param>
        #region
        private void SetAnimation(bool _tf, double _sum)
        {
            _pos = 0;
            _sum = _sum * (-1);
            DoubleAnimation da = new DoubleAnimation(0, _sum, new Duration(TimeSpan.FromMilliseconds(200)));
            Console.Write("_sum is:" + _sum + "\n");
            this.BeginAnimation(Window1.TranslatePosProperty, da);
        }
        #endregion
    }
}
