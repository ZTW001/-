using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Drawing

{
    class DrawingPanel : Panel

    {
        //所有图形
        private List<Visual> visuals = new List<Visual>();
        private List<DrawingVisual> hits = new List<DrawingVisual>();


        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return visuals.Count;
            }
        }


        public void addVisuals(Visual visual)
        {
            visuals.Add(visual);
            base.AddVisualChild(visual);
            base.AddLogicalChild(visual);
        }

        public void DeleteVisual(Visual visual)
        {
            visuals.Remove(visual);
            base.RemoveVisualChild(visual);
            base.RemoveLogicalChild(visual);
        }

        public DrawingVisual GetVisual(Point point)
        {

            HitTestResult hitTestResult = VisualTreeHelper.HitTest(this, point);
            return hitTestResult.VisualHit as DrawingVisual;
        }

        //
        public List<DrawingVisual> GetVisuals(Geometry geometry)
        {
            hits.Clear();
            GeometryHitTestParameters parameters = new GeometryHitTestParameters(geometry);
            HitTestResultCallback callback = new HitTestResultCallback(this.HitTestResultCallback);
            VisualTreeHelper.HitTest(this, null, callback, parameters);
            return hits;
        }

        //HitTest回调函数
        private HitTestResultBehavior HitTestResultCallback(HitTestResult result)
        {
            GeometryHitTestResult geometryHitTest = (GeometryHitTestResult)result;
            DrawingVisual visual = geometryHitTest.VisualHit as DrawingVisual;
            if (visual != null && geometryHitTest.IntersectionDetail == IntersectionDetail.FullyInside)
            {
                hits.Add(visual);
            }
            return HitTestResultBehavior.Continue;
        }
    }
}