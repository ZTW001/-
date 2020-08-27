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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MoonSnake
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DrawGrid();

            timer.Interval = new TimeSpan(0, 0, 0, 0, 260);
            //timer.Tick += Timer_Tick;
        }

        public class Fruit
        {

            List<SnakeNode> SnakeNodes = new List<SnakeNode>();        // 蛇身列表
            Fruit fruit;                                            // 水果
            Random rnd = new Random((int)DateTime.Now.Ticks);        // 随机数
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();    // 计时器
            public Point _pos { get; set; }
            public Ellipse _ellipse { get; set; }
            public Canvas _canvas { get; set; }

            public Fruit(Point point, Canvas canvas)
            {
                _pos = point;
                _canvas = canvas;

                _ellipse = new Ellipse
                {
                    Width = 20,
                    Height = 20,
                    Fill = Brushes.Red
                };

                _ellipse.SetValue(Canvas.LeftProperty, _pos.X * 20);
                _ellipse.SetValue(Canvas.TopProperty, _pos.Y * 20);
                _canvas.Children.Add(_ellipse);
            }

            public void SetPostion(Point pos)
            {
                _pos = pos;

                _ellipse.SetValue(Canvas.LeftProperty, _pos.X * 20);
                _ellipse.SetValue(Canvas.TopProperty, _pos.Y * 20);
            }
        }

        public class SnakeNode
        {

            const int CellSize = 20;                // 小格子大小
            const int SnakeHead = 0;                // 蛇头位置（永远位于列表0）
            const int CellWidth = 640 / CellSize;    // 游戏区横格数
            const int CellHeight = 480 / CellSize;    // 游戏区纵格数

            // 蛇身前进方向
            enum Direction
            {
                UP,
                DOWN,
                LEFT,
                RIGHT
            }
            Direction Direct = Direction.UP;

            // 游戏状态
            enum GameState
            {
                NONE,
                GAMEING,
                PAUSE,
                STOP
            }
            GameState CurrGameState = GameState.NONE;
            public Point _pos { get; set; }
            public Rectangle _rect { get; set; }

            public SnakeNode(Point point)
            {
                _pos = point;

                _rect = new Rectangle
                {
                    Width = 20,
                    Height = 20,
                    Stroke = new SolidColorBrush(Colors.DodgerBlue),
                    StrokeThickness = 3,
                    Fill = Brushes.SkyBlue
                };

                _rect.SetValue(Canvas.LeftProperty, _pos.X * 20);
                _rect.SetValue(Canvas.TopProperty, _pos.Y * 20);
            }
        }

        private void DrawGrid()
        {
            Path gridPath = new Path();
            gridPath.Stroke = new SolidColorBrush(Color.FromArgb(255, 50, 50, 50));
            gridPath.StrokeThickness = 1;

            StringBuilder data = new StringBuilder();

            for (int x = 0; x < 640; x += CellSize)
            {
                data.Append($"M{x},0 L{x},480 ");
            }

            for (int y = 0; y < 480; y += CellSize)
            {
                data.Append($"M0,{y} L640,{y} ");
            }

            gridPath.Data = Geometry.Parse(data.ToString());
            myCanvas.Children.Add(gridPath);
        }

        private void GenNewSnakeNode()
        {
            SnakeNode snakeNode = null;
            switch (Direct)
            {
                case Direction.UP:
                    snakeNode = new SnakeNode(new Point(SnakeNodes[SnakeHead]._pos.X,
                        SnakeNodes[SnakeHead]._pos.Y - 1));
                    break;

                case Direction.DOWN:
                    snakeNode = new SnakeNode(new Point(SnakeNodes[SnakeHead]._pos.X,
                        SnakeNodes[SnakeHead]._pos.Y + 1));
                    break;

                case Direction.LEFT:
                    snakeNode = new SnakeNode(new Point(SnakeNodes[SnakeHead]._pos.X - 1,
                        SnakeNodes[SnakeHead]._pos.Y));
                    break;

                case Direction.RIGHT:
                    snakeNode = new SnakeNode(new Point(SnakeNodes[SnakeHead]._pos.X + 1,
                        SnakeNodes[SnakeHead]._pos.Y));
                    break;
            }

            if (snakeNode != null)
            {
                SnakeNodes.Insert(0, snakeNode);
                myCanvas.Children.Add(SnakeNodes[0]._rect);
            }
        }

        private Point SetFruitToRandomPos()
        {
            bool flag = true;
            Point pos = new Point();
            while (flag)
            {
                flag = false;
                pos = new Point(rnd.Next(0, CellWidth - 1), rnd.Next(0, CellHeight - 1));
                foreach (var node in SnakeNodes)
                {
                    if (pos.X == node._pos.X && pos.Y == node._pos.Y)
                    {
                        flag = true;
                        break;
                    }
                }
            }

            return pos;
        }

        private bool IsGameOver()
        {
            if (SnakeNodes[SnakeHead]._pos.X == -1 || SnakeNodes[SnakeHead]._pos.X == CellWidth
                || SnakeNodes[SnakeHead]._pos.Y == -1 || SnakeNodes[SnakeHead]._pos.Y == CellHeight)
            {
                return true;
            }

            foreach (var node in SnakeNodes)
            {
                if (node == SnakeNodes[SnakeHead])
                    continue;

                if (node._pos.X == SnakeNodes[SnakeHead]._pos.X && node._pos.Y == SnakeNodes[SnakeHead]._pos.Y)
                {
                    return true;
                }
            }

            return false;
        }

        private void RemoveFruit()
        {
            if (fruit == null)
            {
                return;
            }

            if (myCanvas.Children.Contains(fruit._ellipse))
            {
                myCanvas.Children.Remove(fruit._ellipse);
            }
        }

        private void MyCanvas_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    if (Direct != Direction.RIGHT)
                    {
                        Direct = Direction.LEFT;
                    }
                    break;

                case Key.Right:
                    if (Direct != Direction.LEFT)
                    {
                        Direct = Direction.RIGHT;
                    }
                    break;

                case Key.Up:
                    if (Direct != Direction.DOWN)
                    {
                        Direct = Direction.UP;
                    }
                    break;

                case Key.Down:
                    if (Direct != Direction.UP)
                    {
                        Direct = Direction.DOWN;
                    }
                    break;

                case Key.Escape:
                    Application.Current.Shutdown();
                    break;

                case Key.Space:
                    if (CurrGameState == GameState.NONE)
                        return;

                    if (CurrGameState == GameState.PAUSE)
                    {
                        CurrGameState = GameState.GAMEING;
                        timer.Start();
                        MenuControl_Pause.Header = "暂停";
                    }
                    else if (CurrGameState == GameState.GAMEING)
                    {
                        CurrGameState = GameState.PAUSE;
                        timer.Stop();
                        MenuControl_Pause.Header = "继续";
                    }
                    break;
            }
        }

        private void StartGame()
        {
            RemoveSnakeNodeAll();
            RemoveFruit();

            int startX = rnd.Next(5, CellWidth - 6);
            int startY = rnd.Next(5, CellHeight - 6);
            Direct = Direction.RIGHT;

            fruit = new Fruit(SetFruitToRandomPos(), myCanvas);

            SnakeNodes = new List<SnakeNode>();
            SnakeNodes.Add(new SnakeNode(new Point(startX, startY)));
            GenNewSnakeNode();
            GenNewSnakeNode();
        }

        private void MenuFile_NewGame_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
            timer.Start();
            CurrGameState = GameState.GAMEING;
            MenuControl_Pause.Header = "暂停";
        }

        private void MenuControl_Pause_Click(object sender, RoutedEventArgs e)
        {
            if (CurrGameState == GameState.GAMEING)
            {
                CurrGameState = GameState.PAUSE;
                timer.Stop();
                MenuControl_Pause.Header = "继续";
            }
            else if (CurrGameState == GameState.PAUSE)
            {
                CurrGameState = GameState.GAMEING;
                timer.Start();
                MenuControl_Pause.Header = "暂停";
            }
        }


        private void MenuFile_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
