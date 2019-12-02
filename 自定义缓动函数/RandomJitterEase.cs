using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Animation;

namespace 自定义缓动函数
{
    class RandomJitterEase : EasingFunctionBase
    {
        //声明一个Random类，用于声明随机数。
        Random rand = new Random();
        /// <summary>
        /// 重写EaseCore方法。 
        /// </summary>
        /// <param name="normalizedTime"></param>
        /// <returns></returns>
        protected override double EaseInCore(double normalizedTime)
        {
            //几乎所有逻辑代码都在EaseInCore方法中运行。该方法接受一个规范化的时间值，本质上是表示动画进度从
            //0到1之间的值，当动画开始时，规范化的时间值是0，它从该点开始增加，直到在动画结束点达到1.
            //在动画运行期间，每次更新动画的值时，WPF都会调用EaseInCore方法，确切的调用频率取决于动画的帧率。
            if (normalizedTime == 1)
            {
                return 1;
            }
            else
            {
                return Math.Abs(normalizedTime - (double)rand.Next(0, 10) / (2010 - Jitter));
            }
        }

        protected override System.Windows.Freezable CreateInstanceCore()
        {
            return new RandomJitterEase();
        }

        //定义一个依赖属性。
        public static readonly DependencyProperty JitterProperty;

        //在静态方法中注册依赖属性。
        static RandomJitterEase()
        {
            JitterProperty = DependencyProperty.Register("Jitter", typeof(int), typeof(RandomJitterEase), new UIPropertyMetadata(1000), new ValidateValueCallback(ValidateJitter));
        }

        public int Jitter
        {
            get { return (int)GetValue(JitterProperty); }
            set { SetValue(JitterProperty, value); }
        }

        //此方法用于判断值。
        private static bool ValidateJitter(object value)
        {
            int jitterValue = (int)value;
            if (jitterValue <= 2000 && jitterValue >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
