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

namespace 变换形状
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
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //伪元素设置RotateTransform属性。
            ////声明一个RotateTransform对象。
            //RotateTransform rtf = new RotateTransform(30);
            ////设置RenderTransformOrigin属性。
            //btn1.RenderTransformOrigin = new Point(0.5, 0.5);
            ////将元素的RenderTransform属性赋值为声明的RotateTransform对象。
            //btn1.RenderTransform = rtf;

            //为元素设置ScaleTransform属性。
            ////声明ScaleTransform对象。
            //ScaleTransform stf = new ScaleTransform();
            ////设置ScaleX属性。
            //stf.ScaleX = 2;
            ////设置ScaleY属性。
            //stf.ScaleY = 2;
            ////设置元素的RenderTransformOrigin属性。
            //btn1.RenderTransformOrigin = new Point(0.5, 0.5);
            ////为元素的RenderTransform属性赋值。
            //btn1.RenderTransform = stf;

            //为元素设置RotateTransform属性和ScaleTransform属性。
            //声明TransformGroup对象。
            TransformGroup tfg = new TransformGroup();
            //声明TransformGroup杜对象。
            RotateTransform rtf = new RotateTransform(30);
            //将RotateTransform对象加入到TransformGroup对象中。
            tfg.Children.Add(rtf);
            //声明ScaleTransform对象。
            ScaleTransform stf = new ScaleTransform();
            //设置ScaleX属性。
            stf.ScaleX = 2;
            //设置ScaleY属性。
            stf.ScaleY = 2;
            //将ScaleTransform对象加入到TransformGroup中。
            tfg.Children.Add(stf);
            btn1.RenderTransformOrigin = new Point(0.5, 0.5);
            //为元素的RenderTransform属性赋值为TransformGroup对象(TransformGroup对象中有ScaleTransform对象和RotateTransform对象)。
            btn1.RenderTransform = tfg;
        }
    }
}
