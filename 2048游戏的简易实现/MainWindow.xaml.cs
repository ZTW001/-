using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace _2048游戏的简易实现
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        int lblWidth = 105;                     // 方块大小
        int lblPadding = 15;                    // 方块间隙

        int[,] gridData = null;                 // 游戏主数据数组
        Label[,] lblArray = null;               // 用于显示成方块的Label数组

        int currScore = 0;                      // 当前成绩

        bool isStarted = false;                 // 游戏是否已开始

        Random rnd = new Random();             // 随机数
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        ///  显示背景矩形块
        /// </summary>
        private void ShowBackRect()
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    Rectangle rect = new Rectangle();
                    rect.SetValue(Canvas.LeftProperty, (double)((x + 1) * lblPadding + x * lblWidth));
                    rect.SetValue(Canvas.TopProperty, (double)((y + 1) * lblPadding + y * lblWidth));
                    myCanvas.Children.Add(rect);
                }
            }
        }

        /// <summary>
        /// 重载生成新数
        /// </summary>
        /// <returns></returns>
        private bool NewNum()
        {
            int num = rnd.Next(0, 9) > 2 ? 2 : 4;

            int nullnum = 0;
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (gridData[y, x] == 0)
                        nullnum++;
                }
            }

            if (nullnum < 1)
            {
                return false;
            }

            int index = rnd.Next(1, nullnum);
            nullnum = 0;
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (gridData[y, x] == 0)
                    {
                        nullnum++;
                        if (nullnum != index)
                            continue;

                        gridData[y, x] = num;
                    }
                }
            }

            return true;
        }


        /// <summary>
        /// 根据数值生成方块背景色值
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private Brush SetBackground(int num)
        {
            Brush backColor;
            switch (num)
            {
                case 2:
                    backColor = new SolidColorBrush(Color.FromRgb(0xee, 0xe4, 0xda));
                    break;
                case 4:
                    backColor = new SolidColorBrush(Color.FromRgb(0xec, 0xe0, 0xc8));
                    break;
                case 8:
                    backColor = new SolidColorBrush(Color.FromRgb(0xf2, 0xb1, 0x79));
                    break;
                case 16:
                    backColor = new SolidColorBrush(Color.FromRgb(0xf5, 0x95, 0x63));
                    break;
                case 32:
                    backColor = new SolidColorBrush(Color.FromRgb(0xf5, 0x7c, 0x5f));
                    break;
                case 64:
                    backColor = new SolidColorBrush(Color.FromRgb(0xf6, 0x5d, 0x3b));
                    break;
                case 128:
                    backColor = new SolidColorBrush(Color.FromRgb(0xed, 0xce, 0x71));
                    break;
                case 256:
                    backColor = new SolidColorBrush(Color.FromRgb(0xed, 0xcc, 0x61));
                    break;

                case 512:
                    backColor = new SolidColorBrush(Color.FromRgb(0xec, 0xc8, 0x50));
                    break;
                case 1024:
                    backColor = new SolidColorBrush(Color.FromRgb(0xed, 0xc5, 0x3f));
                    break;
                case 2048:
                    backColor = new SolidColorBrush(Color.FromRgb(0xee, 0xc2, 0x2e));
                    break;
                case 4096:
                    backColor = new SolidColorBrush(Color.FromRgb(0xef, 0x85, 0x9c));
                    break;
                default:
                    backColor = new SolidColorBrush(Color.FromRgb(0xcc, 0xc0, 0xb2));
                    break;
            }
            return backColor;
        }

        /// <summary>
        /// 根据数值设置方块字体大小
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private int SetFontSize(int num)
        {
            int iFontsize;
            switch (num)
            {
                case 2:
                case 4:
                case 8:
                    iFontsize = 55;
                    break;

                case 16:
                case 32:
                case 64:
                    iFontsize = 50;
                    break;

                case 128:
                case 256:
                case 512:
                    iFontsize = 40;
                    break;

                case 1024:
                case 2048:
                case 4096:
                    iFontsize = 33;
                    break;

                default:
                    iFontsize = 30;
                    break;
            }
            return iFontsize;
        }

        /// <summary>
        /// 重新显示所有Label
        /// </summary>
        private void ShowAllLabel()
        {
            myCanvas.Children.Clear();
            ShowBackRect();

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (gridData[y, x] != 0)
                    {
                        lblArray[y, x] = new Label();
                        lblArray[y, x].SetValue(Canvas.LeftProperty, lblPadding * (x + 1) + (double)(x * lblWidth));
                        lblArray[y, x].SetValue(Canvas.TopProperty, lblPadding * (y + 1) + (double)(y * lblWidth));
                        lblArray[y, x].SetValue(Label.ContentProperty, gridData[y, x].ToString());
                        lblArray[y, x].SetValue(Label.BackgroundProperty, SetBackground(gridData[y, x]));
                        lblArray[y, x].SetValue(Label.FontSizeProperty, (double)SetFontSize(gridData[y, x]));
                        myCanvas.Children.Add(lblArray[y, x]);
                    }
                }
            }
        }


        private void BtnNewGame_Click(object sender, RoutedEventArgs e)
        {
            if (gridData != null)                       // 初始化主数据数组
                gridData = null;

            gridData = new int[4, 4]
            {
        { 0, 0, 0, 0 },
        { 0, 0, 0, 0 },
        { 0, 0, 0, 0 },
        { 0, 0, 0, 0 }
            };

            if (lblArray != null)                       // 初始化显示用Label数组
                lblArray = null;

            lblArray = new Label[4, 4];

            if (myCanvas.Children.Count > 0)
                myCanvas.Children.Clear();              // 清除界面

            NewNum();                                   // 生成两个新数
            NewNum();

            ShowAllLabel();                             // 刷新游戏方块

            isStarted = true;
            currScore = 0;                              // 初始化当前成绩为0

            lblCurrScore.Content = currScore.ToString();// 更新当前成绩显示
        }

        /// <summary>
        /// 游戏是否结束
        /// </summary>
        /// <returns></returns>
        private bool IsGameOver()
        {
            for (int row = 0; row < 4; row++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (gridData[row, i] == 0)  // 是否有空位
                    {
                        return false;
                    }
                }
                for (int i = 0; i < 3; i++)
                {
                    if (gridData[row, i] == gridData[row, i + 1])
                    {
                        return false;
                    }
                }
            }

            for (int col = 0; col < 4; col++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (gridData[i, col] == 0)
                    {
                        return false;
                    }
                }
                for (int i = 0; i < 3; i++)
                {
                    if (gridData[i, col] == gridData[i + 1, col])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 显示动画结束对话框
        /// </summary>
        private void ShowGameOver()
        {
            MessageBox.Show("游戏已经结束。", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
            isStarted = false;
        }


        private void BtnExitGame_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 动画显示增加得分
        /// </summary>
        /// <param name="addScore"></param>
        private void ShowAddScore(int addScore)
        {
            lblAddScore.Content = "+" + addScore.ToString();

            DoubleAnimation top = new DoubleAnimation();
            DoubleAnimation opacity = new DoubleAnimation();
            opacity.AutoReverse = true;
            opacity.From = 0;
            opacity.To = 1;
            top.From = 0;
            top.To = -40;
            Duration duration = new Duration(TimeSpan.FromMilliseconds(500));
            top.Duration = duration;
            opacity.Duration = duration;

            tt.BeginAnimation(TranslateTransform.YProperty, top);
            lblAddScore.BeginAnimation(Label.OpacityProperty, opacity);
        }

        /// <summary>
        /// 左移
        /// </summary>
        private void MoveLeft()
        {
            int score = 0;

            Storyboard sb1 = new Storyboard();

            // 移去左侧和中间的空块（左移）
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    for (int i = x + 1; i < 4; i++)
                    {
                        if (gridData[y, i] != 0 && gridData[y, x] == 0)
                        {
                            gridData[y, x] = gridData[y, i];

                            if (lblArray[y, i] == null)
                            {
                                lblArray[y, i] = new Label();
                                lblArray[y, i].SetValue(Canvas.LeftProperty, lblPadding * ((i) + 1) + (double)((i) * lblWidth));
                                lblArray[y, i].SetValue(Canvas.TopProperty, lblPadding * (y + 1) + (double)(y * lblWidth));
                                lblArray[y, i].SetValue(Label.ContentProperty, gridData[y, i].ToString());
                                lblArray[y, i].SetValue(Label.BackgroundProperty, SetBackground(gridData[y, i]));
                                lblArray[y, i].SetValue(Button.FontSizeProperty, (double)SetFontSize(gridData[y, i]));
                            }

                            // 左移方块动画
                            DoubleAnimation da1 = null;
                            double from = (double)lblArray[y, i].GetValue(Canvas.LeftProperty);
                            double to = (x + 1) * lblPadding + x * lblWidth;
                            da1 = new DoubleAnimation(
                                from,
                                to,
                                new Duration(TimeSpan.FromMilliseconds(300)));
                            da1.AccelerationRatio = 0.1;
                            da1.DecelerationRatio = 0.1;

                            Storyboard.SetTarget(da1, lblArray[y, i]);
                            Storyboard.SetTargetProperty(da1, new PropertyPath("(Canvas.Left)"));
                            sb1.Children.Add(da1);

                            gridData[y, i] = 0;
                        }
                    }
                }
            }

            // 相邻相同方块合并后加到左侧
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (x + 1 < 4 && gridData[y, x] == gridData[y, x + 1])
                    {
                        // 如果右侧的方块未及时生成
                        if (gridData[y, x + 1] != 0)// && gridData[y,x]!=0)
                        {
                            if (lblArray[y, x + 1] == null)
                            {
                                lblArray[y, x + 1] = new Label();
                                lblArray[y, x + 1].SetValue(Canvas.LeftProperty, lblPadding * ((x + 1) + 1) + (double)((x + 1) * lblWidth));
                                lblArray[y, x + 1].SetValue(Canvas.TopProperty, lblPadding * (y + 1) + (double)(y * lblWidth));
                                lblArray[y, x + 1].SetValue(Label.ContentProperty, gridData[y, x + 1].ToString());
                                lblArray[y, x + 1].SetValue(Label.BackgroundProperty, SetBackground(gridData[y, x + 1]));
                                lblArray[y, x + 1].SetValue(Button.FontSizeProperty, (double)SetFontSize(gridData[y, x + 1]));
                            }

                            // 左移动画
                            DoubleAnimation da2 = null;
                            double from = (double)lblArray[y, x + 1].GetValue(Canvas.LeftProperty);
                            double to = from - lblWidth - lblPadding;
                            da2 = new DoubleAnimation(
                                from,
                                to,
                                new Duration(TimeSpan.FromMilliseconds(200)));
                            da2.AccelerationRatio = 0.1;
                            da2.DecelerationRatio = 0.1;
                            Storyboard.SetTarget(da2, lblArray[y, x + 1]);
                            Storyboard.SetTargetProperty(da2, new PropertyPath("(Canvas.Left)"));
                            sb1.Children.Add(da2);
                        }
                        gridData[y, x] *= 2;
                        gridData[y, x + 1] = 0;

                        score += gridData[y, x];
                    }
                }
            }

            if (score != 0)
            {
                ShowAddScore(score);
                currScore += score;
                lblCurrScore.Content = currScore.ToString();
            }

            // 将合并后出现的中间空方块移去（再次左移一次）
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    for (int i = x + 1; i < 4; i++)
                    {
                        if (gridData[y, i] != 0 && gridData[y, x] == 0)
                        {
                            gridData[y, x] = gridData[y, i];

                            if (lblArray[y, i] == null)
                            {
                                lblArray[y, i] = new Label();
                                lblArray[y, i].SetValue(Canvas.LeftProperty, lblPadding * ((i) + 1) + (double)((i) * lblWidth));
                                lblArray[y, i].SetValue(Canvas.TopProperty, lblPadding * (y + 1) + (double)(y * lblWidth));
                                lblArray[y, i].SetValue(Label.ContentProperty, gridData[y, i].ToString());
                                lblArray[y, i].SetValue(Label.BackgroundProperty, SetBackground(gridData[y, i]));
                                lblArray[y, i].SetValue(Button.FontSizeProperty, (double)SetFontSize(gridData[y, i]));
                            }

                            // 左移动画
                            DoubleAnimation da = null;
                            double from = (double)lblArray[y, i].GetValue(Canvas.LeftProperty);
                            double to = (x + 1) * lblPadding + x * lblWidth;
                            da = new DoubleAnimation(
                                from,
                                to,
                                new Duration(TimeSpan.FromMilliseconds(200)));
                            da.AccelerationRatio = 0.1;
                            da.DecelerationRatio = 0.1;
                            Storyboard.SetTarget(da, lblArray[y, i]);
                            Storyboard.SetTargetProperty(da, new PropertyPath("(Canvas.Left)"));
                            sb1.Children.Add(da);

                            gridData[y, i] = 0;

                            //   isMove = true;
                        }
                    }
                }
            }

            sb1.Completed += Sb1_Completed;     // 所有动画完成后执行事件
            sb1.Begin();
        }

        /// <summary>
        /// 动画完成后运行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sb1_Completed(object sender, EventArgs e)
        {
            // 检查游戏是否结束
            if (IsGameOver())
            {
                ShowGameOver();
            }
            else
            {
                NewNum();
                ShowAllLabel();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isStarted)
                return;

            switch (e.Key)
            {
                case Key.Left:
                    if (!IsGameOver())
                        MoveLeft();
                    else
                        ShowGameOver();
                    break;

                //case Key.Right:
                //    if (!isGameOver())
                //        MoveRight();
                //    else
                //        ShowGameOver();

                //    break;

                //case Key.Up:
                //    if (!isGameOver())
                //        MoveUp();
                //    else
                //        ShowGameOver();

                //    break;

                //case Key.Down:
                //    if (!isGameOver())
                //        MoveDown();
                    //else
                    //    ShowGameOver();
                    //break;
            }
        }
    }
}
