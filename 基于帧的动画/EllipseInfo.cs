using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;

namespace 基于帧的动画
{
    class EllipseInfo
    {
        public Ellipse Ellipse { get; set; }

        /// <summary>
        /// Y轴的速度。
        /// </summary>
        public double VelocityY { get; set; }

        public EllipseInfo(Ellipse _ellipse, double _velocityY)
        {
            Ellipse = _ellipse;
            VelocityY = _velocityY;
        }
    }
}
