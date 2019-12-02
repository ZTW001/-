using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WpfAnimationTest

{

    public class BarDescriptor

    {

        public int RectangleX { get; set; }

        public int RectangleY { get; set; }

        public int RectangleWidth { get; set; }

        public int RectangleHeight { get; set; }

        public int AnimationTimeInSeconds { get; set; }

        // 0.0 to 1.0

        public float BarBaseRedLevel { get; set; }

        public float BarBaseGreenLevel { get; set; }

        public float BarBaseBlueLevel { get; set; }

        public float GradientStartX { get; set; }

        public float GradientStartY { get; set; }

        public float GradientEndX { get; set; }

        public float GradientEndY { get; set; }

    }

}

