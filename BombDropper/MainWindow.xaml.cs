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

namespace BombDropper
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
    }
    //private void canvasBackGround_SizeChanged(object sender,SizeChangedEventArgs e)
    //{
    //    //set the clipping region to match the current display  region of the Canvas.
    //    RectangleGeometry rect = new RectangleGeometry();
    //    rect.Rect = new Rect(0,0,CanvaBackground.ActualWidth,canvasBackground.ActualHeight);
    //    CanvasBackground.Clip = rect;

    //}
}
