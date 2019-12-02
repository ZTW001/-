using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFPathReverse
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string data = this.path0.Data.ToString();
            var result = ConvertReverseData(data);
            Path newpath = new Path();
            newpath.Data = PathGeometry.CreateFromGeometry(Geometry.Parse(result));
            newpath.HorizontalAlignment = HorizontalAlignment.Center;
            newpath.VerticalAlignment = VerticalAlignment.Center;
            newpath.Stretch = this.path0.Stretch;
            newpath.Stroke = new SolidColorBrush(Colors.Red);
            newpath.StrokeThickness = 2;
            newpath.Width = this.path0.Width;
            newpath.Height = this.path0.Height;
            canvas.Children.Add(newpath);
        }

        /// <summary>
        /// 反向Data数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string ConvertReverseData(string data)
        {
            data = data.Replace("M", "").Replace(" ", "/");
            Regex regex = new Regex("[a-z]", RegexOptions.IgnoreCase);
            MatchCollection mc = regex.Matches(data);
            //item1 从上一个位置到当前位置开始的字符 (match.Index=原始字符串中发现捕获的子字符串的第一个字符的位置。)
            //item2 当前发现的匹配符号(L C Z M)
            List<Tuple<string, string>> tmpList = new List<Tuple<string, string>>();
            int curPostion = 0;
            for (int i = 0; i < mc.Count; i++)
            {
                Match match = mc[i];
                if (match.Index != curPostion)
                {
                    string str = data.Substring(curPostion, match.Index - curPostion);
                    tmpList.Add(new Tuple<string, string>(str, match.Value));
                }
                curPostion = match.Index + match.Length;
                if (i + 1 == mc.Count)//last 
                {
                    tmpList.Add(new Tuple<string, string>(data.Substring(curPostion), match.Value));
                }
            }
            //char[] spChar = new char[2] { 'C', 'L' };
            //var tmpList = data.Split(spChar);
            List<string[]> spList = new List<string[]>();
            for (int i = 0; i < tmpList.Count; i++)
            {
                var cList = tmpList[i].Item1.Split('/');
                spList.Add(cList);
            }
            List<string> strList = new List<string>();
            for (int i = spList.Count - 1; i >= 0; i--)
            {
                string[] clist = spList[i];
                for (int j = clist.Length - 1; j >= 0; j--)
                {
                    if (j == clist.Length - 2)//对于第二个元素增加 L或者C的标识
                    {
                        var pointWord = tmpList[i - 1].Item2;//获取标识
                        strList.Add(pointWord + clist[j]);
                    }
                    else
                    {
                        strList.Add(clist[j]);
                    }
                }
            }
            string reverseData = "M" + string.Join(" ", strList);
            return reverseData;

        }

        private void btnPositive_Click(object sender, RoutedEventArgs e)
        {
            MatrixStory(0, this.path0.Data.ToString());
        }

        private void btnRevPositive_Click(object sender, RoutedEventArgs e)
        {
            string data = this.path0.Data.ToString();
            var result = ConvertReverseData(data);
            MatrixStory(1, result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orientation">0正向 1反向</param>
        /// <param name="data">路径数据</param>
        private void MatrixStory(int orientation, string data)
        {
            Border border = new Border();
            border.Width = 10;
            border.Height = 10;
            border.Visibility = Visibility.Collapsed;
            if (orientation == 0)
            {
                border.Background = new SolidColorBrush(Colors.Blue);
            }
            else
            {
                border.Background = new SolidColorBrush(Colors.Green);
            }

            this.canvas.Children.Add(border);
            Canvas.SetLeft(border, -border.Width / 2);
            Canvas.SetTop(border, -border.Height / 2);
            border.RenderTransformOrigin = new Point(0.5, 0.5);

            MatrixTransform matrix = new MatrixTransform();
            TransformGroup groups = new TransformGroup();
            groups.Children.Add(matrix);
            border.RenderTransform = groups;
            //NameScope.SetNameScope(this, new NameScope());
            string registname = "matrix" + Guid.NewGuid().ToString().Replace("-", "");
            this.RegisterName(registname, matrix);
            MatrixAnimationUsingPath matrixAnimation = new MatrixAnimationUsingPath();
            matrixAnimation.PathGeometry = PathGeometry.CreateFromGeometry(Geometry.Parse(data));
            matrixAnimation.Duration = new Duration(TimeSpan.FromSeconds(5));
            matrixAnimation.DoesRotateWithTangent = true;//旋转
            //matrixAnimation.FillBehavior = FillBehavior.Stop;
            Storyboard story = new Storyboard();
            story.Children.Add(matrixAnimation);
            Storyboard.SetTargetName(matrixAnimation, registname);
            Storyboard.SetTargetProperty(matrixAnimation, new PropertyPath(MatrixTransform.MatrixProperty));

            #region 控制显示与隐藏
            ObjectAnimationUsingKeyFrames ObjectAnimation = new ObjectAnimationUsingKeyFrames();
            ObjectAnimation.Duration = matrixAnimation.Duration;
            DiscreteObjectKeyFrame kf1 = new DiscreteObjectKeyFrame(Visibility.Visible, TimeSpan.FromMilliseconds(1));
            ObjectAnimation.KeyFrames.Add(kf1);
            story.Children.Add(ObjectAnimation);
            //Storyboard.SetTargetName(border, border.Name);
            Storyboard.SetTargetProperty(ObjectAnimation, new PropertyPath(UIElement.VisibilityProperty));
            #endregion
            story.FillBehavior = FillBehavior.Stop;
            story.Begin(border, true);
        }
    }
}