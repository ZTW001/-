using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WpfAnimationTest

{
    public partial class MainWindow
    {
        private Rectangle _testRect;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindowLoaded;
        }
        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            BarDescriptor barOne = new BarDescriptor
            {
                RectangleX = 0,
                RectangleY = 0,
                RectangleWidth = 800,
                RectangleHeight = 600,
                AnimationTimeInSeconds = 5,
                BarBaseRedLevel = 0,
                BarBaseGreenLevel = 0.5f,
                BarBaseBlueLevel = 0,
                GradientStartX = 0,
                GradientStartY = 0.5f,
                GradientEndX = 1,
                GradientEndY = 0.5f
            };

            BarDescriptor barTwo = new BarDescriptor
            {
                RectangleX = 0,
                RectangleY = 0,
                RectangleWidth = 800,
                RectangleHeight = 600,
                AnimationTimeInSeconds = 4,
                BarBaseRedLevel = 0,
                BarBaseGreenLevel = 0.5f,
                BarBaseBlueLevel = 0,
                GradientStartX = 1,
                GradientStartY = 0,
                GradientEndX = 0,
                GradientEndY = 1
            };

            BarDescriptor barThree = new BarDescriptor
            {
                RectangleX = 0,
                RectangleY = 0,
                RectangleWidth = 800,
                RectangleHeight = 600,
                AnimationTimeInSeconds = 3,
                BarBaseRedLevel = 0,
                BarBaseGreenLevel = 0.5f,
                BarBaseBlueLevel = 0,
                GradientStartX = 0,
                GradientStartY = 0,
                GradientEndX = 1,
                GradientEndY = 1
            };

            BarDescriptor barFour = new BarDescriptor
            {
                RectangleX = 0,
                RectangleY = 0,
                RectangleWidth = 800,
                RectangleHeight = 600,
                AnimationTimeInSeconds = 6,
                BarBaseRedLevel = 0,
                BarBaseGreenLevel = 0.5f,
                BarBaseBlueLevel = 0,
                GradientStartX = 0.5f,
                GradientStartY = 0,
                GradientEndX = 0.5f,
                GradientEndY = 1
            };

            CreateRectangleAnimatedRectangle(barOne);
            CreateRectangleAnimatedRectangle(barTwo);
            CreateRectangleAnimatedRectangle(barThree);
            CreateRectangleAnimatedRectangle(barFour);
        }
        private void CreateRectangleAnimatedRectangle(BarDescriptor inputParameters)
        {
            DoubleAnimation firstStopAnim = new DoubleAnimation(0.01, 0.95, new Duration(new TimeSpan(0, 0, 0, 5)));
            DoubleAnimation secondStopAnim = new DoubleAnimation(0.02, 0.96, new Duration(new TimeSpan(0, 0, 0, 5)));
            DoubleAnimation thirdStopAnim = new DoubleAnimation(0.03, 0.97, new Duration(new TimeSpan(0, 0, 0, 5)));
            DoubleAnimation fourthStopAnim = new DoubleAnimation(0.04, 0.98, new Duration(new TimeSpan(0, 0, 0, 5)));
            DoubleAnimation fifthStopAnim = new DoubleAnimation(0.05, 0.99, new Duration(new TimeSpan(0, 0, 0, 5)));

            firstStopAnim.AutoReverse = true;
            secondStopAnim.AutoReverse = true;
            thirdStopAnim.AutoReverse = true;
            fourthStopAnim.AutoReverse = true;
            fifthStopAnim.AutoReverse = true;

            firstStopAnim.BeginTime = new TimeSpan(0);
            secondStopAnim.BeginTime = new TimeSpan(0);
            thirdStopAnim.BeginTime = new TimeSpan(0);
            fourthStopAnim.BeginTime = new TimeSpan(0);
            fifthStopAnim.BeginTime = new TimeSpan(0);

            firstStopAnim.EasingFunction = new CubicEase();
            secondStopAnim.EasingFunction = new CubicEase();
            thirdStopAnim.EasingFunction = new CubicEase();
            fourthStopAnim.EasingFunction = new CubicEase();
            fifthStopAnim.EasingFunction = new CubicEase();

            GradientStopCollection gradientStops = new GradientStopCollection
            {
                new GradientStop(Color.FromScRgb(0.5f,inputParameters.BarBaseRedLevel,inputParameters.BarBaseGreenLevel,inputParameters.BarBaseBlueLevel), 0.0),
                new GradientStop(Color.FromScRgb(0.0f,inputParameters.BarBaseRedLevel,inputParameters.BarBaseGreenLevel,inputParameters.BarBaseBlueLevel), 0.01),
                new GradientStop(Color.FromScRgb(0.5f,inputParameters.BarBaseRedLevel,inputParameters.BarBaseGreenLevel,inputParameters.BarBaseBlueLevel), 0.02),
                new GradientStop(Color.FromScRgb(1.0f,inputParameters.BarBaseRedLevel,inputParameters.BarBaseGreenLevel,inputParameters.BarBaseBlueLevel), 0.03),
                new GradientStop(Color.FromScRgb(0.5f,inputParameters.BarBaseRedLevel,inputParameters.BarBaseGreenLevel,inputParameters.BarBaseBlueLevel), 0.04),
                new GradientStop(Color.FromScRgb(0.0f,inputParameters.BarBaseRedLevel,inputParameters.BarBaseGreenLevel,inputParameters.BarBaseBlueLevel), 0.05),
                new GradientStop(Color.FromScRgb(0.5f,inputParameters.BarBaseRedLevel,inputParameters.BarBaseGreenLevel,inputParameters.BarBaseBlueLevel), 1.0),
            };

            String slotOneName = RandomName();
            String slotTwoName = RandomName();
            String slotThreeName = RandomName();
            String slotFourName = RandomName();
            String slotFiveName = RandomName();

            RegisterName(slotOneName, gradientStops[1]);
            RegisterName(slotTwoName, gradientStops[2]);
            RegisterName(slotThreeName, gradientStops[3]);
            RegisterName(slotFourName, gradientStops[4]);
            RegisterName(slotFiveName, gradientStops[5]);

            LinearGradientBrush gradientBrush = new LinearGradientBrush(gradientStops, new Point(inputParameters.GradientStartX,
            inputParameters.GradientStartY), new Point(inputParameters.GradientEndX, inputParameters.GradientEndY));
            Storyboard.SetTargetName(firstStopAnim, slotOneName);
            Storyboard.SetTargetProperty(firstStopAnim, new PropertyPath(GradientStop.OffsetProperty));

            Storyboard.SetTargetName(secondStopAnim, slotTwoName);
            Storyboard.SetTargetProperty(secondStopAnim, new PropertyPath(GradientStop.OffsetProperty));

            Storyboard.SetTargetName(thirdStopAnim, slotThreeName);
            Storyboard.SetTargetProperty(thirdStopAnim, new PropertyPath(GradientStop.OffsetProperty));

            Storyboard.SetTargetName(fourthStopAnim, slotFourName);
            Storyboard.SetTargetProperty(fourthStopAnim, new PropertyPath(GradientStop.OffsetProperty));

            Storyboard.SetTargetName(fifthStopAnim, slotFiveName);
            Storyboard.SetTargetProperty(fifthStopAnim, new PropertyPath(GradientStop.OffsetProperty));
            Storyboard gradientAnimation = new Storyboard { RepeatBehavior = RepeatBehavior.Forever };

            gradientAnimation.Children.Add(firstStopAnim);
            gradientAnimation.Children.Add(secondStopAnim);
            gradientAnimation.Children.Add(thirdStopAnim);
            gradientAnimation.Children.Add(fourthStopAnim);
            gradientAnimation.Children.Add(fifthStopAnim);

            _testRect = new Rectangle       
            {
                Width = inputParameters.RectangleWidth,
                Height = inputParameters.RectangleHeight,
                Stroke = Brushes.White,
                StrokeThickness = 1,
                Fill = gradientBrush
            };

            Root.Children.Add(_testRect);
            Canvas.SetLeft(_testRect, inputParameters.RectangleX);
            Canvas.SetTop(_testRect, inputParameters.RectangleY);
            gradientAnimation.Begin(this);
        }
        private string RandomName()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const int nameLen = 8;
            var random = new Random();
            string temp = Guid.NewGuid().ToString().
               Replace("-", String.Empty);
            temp = Regex.Replace(temp, @"[\d-]",
               string.Empty).ToUpper();
            return new string(Enumerable.Repeat(chars, nameLen).Select
               (s => s[random.Next(s.Length)]).ToArray()) + temp;
        }
    }
}
