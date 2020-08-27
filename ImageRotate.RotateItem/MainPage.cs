using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRotate.RotateItem
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            var Picture1 = new RotateItem();
            Picture1.Image.Source = new BitmapImage(new Uri("Marigold.jpg", UriKind.Relative));
            Picture1.SetValue(Canvas.LeftProperty, 100.00);
            Picture1.SetValue(Canvas.TopProperty, 100.00);
            Picture1.CanvasCenter.X = (double)Picture1.GetValue(Canvas.LeftProperty) + Picture1.Width / 2;
            Picture1.CanvasCenter.Y = (double)Picture1.GetValue(Canvas.TopProperty) + Picture1.Height / 2;
            Picture1.RotateItemCanvas.Angle = -15;
            LayoutRoot.Children.Add(Picture1);
        }
    }
}
