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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// VisualLayer.xaml 的交互逻辑
    /// </summary>
    public partial class VisualLayer : Window
    {

        private Brush drawingBrush = Brushes.AliceBlue;
        private Brush selectedDrawingBrush = Brushes.LightGoldenrodYellow;
        private Pen drawingPen = new Pen(Brushes.SteelBlue, 3);
        private Size squareSize = new Size(30, 30);
        private DrawingVisual drawingVisual;
        private Vector clickOffset;
        private bool isDragging;
        private DrawingVisual selectedVisual;
        private bool isMultiSelecting = false;
        private Point selectionSquareTopLeft;
        private Brush selectionSquareBrush = Brushes.Transparent;
        private Pen selectionSquarePen = new Pen(Brushes.Black, 2);
        private DrawingVisual selectionSquare;


        public VisualLayer()
        {
            InitializeComponent();
            DrawingVisual v = new DrawingVisual();
            DrawingSquare(v, new Point(10, 10), false);
        }


        private void DrawingSquare(DrawingVisual v, Point topLeftCorner, bool isSelected)
        {
            using (DrawingContext dc = v.RenderOpen())
            {
                Brush brush = drawingBrush;
                if (isSelected) brush = selectedDrawingBrush;

                dc.DrawRectangle(brush, drawingPen, new Rect(topLeftCorner, squareSize));

            }
        }


        private void drawingSurface_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pointClicked = e.GetPosition(drawingSurface);
            if (cmdAdd.IsChecked == true)
            {
                DrawingVisual drawingVisual = new DrawingVisual();
                DrawingSquare(drawingVisual, pointClicked, false);
                drawingSurface.addVisuals(drawingVisual);
            }
            else if (cmdDelete.IsChecked == true)
            {
                DrawingVisual drawingVisual = drawingSurface.GetVisual(pointClicked);
                if (drawingVisual != null) drawingSurface.DeleteVisual(drawingVisual);
            }
            else if (cmdSelectMove.IsChecked == true)
            {
                DrawingVisual visual = drawingSurface.GetVisual(pointClicked);
                if (visual != null)
                {
                    Point topLeftCorner = new Point(
                        visual.ContentBounds.TopLeft.X + drawingPen.Thickness / 2,
                        visual.ContentBounds.TopLeft.Y + drawingPen.Thickness / 2);
                    DrawingSquare(visual, topLeftCorner, true);

                    clickOffset = topLeftCorner - pointClicked;
                    isDragging = true;

                    if (selectedVisual != null && selectedVisual != visual)
                    {
                        // The selection has changed. Clear the previous selection.
                        ClearSelection();
                    }
                    selectedVisual = visual;
                }

            }
            else if (cmdSelecMultipe.IsChecked == true)
            {
                selectionSquare = new DrawingVisual();

                drawingSurface.addVisuals(selectionSquare);

                selectionSquareTopLeft = pointClicked;
                isMultiSelecting = true;

                // Make sure we get the MouseLeftButtonUp event even if the user
                // moves off the Canvas. Otherwise, two selection squares could be drawn at once.
                drawingSurface.CaptureMouse();
            }
        }

        private void ClearSelection()
        {
            Point topLeftCorner = new Point(
                        selectedVisual.ContentBounds.TopLeft.X + drawingPen.Thickness / 2,
                        selectedVisual.ContentBounds.TopLeft.Y + drawingPen.Thickness / 2);
            DrawingSquare(selectedVisual, topLeftCorner, false);
            selectedVisual = null;
        }

        private void drawingSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point pointDragged = e.GetPosition(drawingSurface) + clickOffset;
                DrawingSquare(selectedVisual, pointDragged, true);

            }
            else if (isMultiSelecting)
            {
                Point pointDragged = e.GetPosition(drawingSurface);
                DrawSelectionSquare(selectionSquareTopLeft, pointDragged);
            }
        }

        private void DrawSelectionSquare(Point point1, Point point2)
        {
            selectionSquarePen.DashStyle = DashStyles.Dash;
            using (DrawingContext dc = selectionSquare.RenderOpen())
            {
                dc.DrawRectangle(selectionSquareBrush, selectionSquarePen,
                    new Rect(point1, point2));
            }
        }

        private void drawingSurface_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            if (isMultiSelecting)
            {
                RectangleGeometry geometry = new RectangleGeometry(
                    new Rect(selectionSquareTopLeft, e.GetPosition(drawingSurface)));
                List<DrawingVisual> visualsInRegion =
                    drawingSurface.GetVisuals(geometry);
                MessageBox.Show(String.Format("You selected {0} square(s).", visualsInRegion.Count));

                isMultiSelecting = false;
                drawingSurface.DeleteVisual(selectionSquare);
                drawingSurface.ReleaseMouseCapture();
            }
        }
    }
}