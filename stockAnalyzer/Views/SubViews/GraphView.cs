using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stockAnalyzer
{
    public partial class GraphView : UserControl
    {
        private List<GdiDataType> listGdiData = new List<GdiDataType>();
        private Point dMousePos = new Point();
        private Point prevMousePos = new Point();

        //
        // Constructor
        //
        #region CONSTRUCTOR_DESTRUCTOR
        public GraphView()
        {
            InitializeComponent();
        } // Constructor
        #endregion CONSTRUCTOR_DESTRUCTOR

        //
        // UI Events
        //
        #region UI_EVENTS
        private void Render(object sender, PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;
            gfx.SmoothingMode = SmoothingMode.AntiAlias;

            // Flip along X-Axis
            gfx.Transform = new Matrix(1, 0, 0, -1, 0, 0);

            // Move the origin to the center vertically
            gfx.TranslateTransform(0 + this.dMousePos.X, this.Height + this.dMousePos.Y, MatrixOrder.Append);

            // For all graph data types
            foreach (GdiDataType gdiData in this.listGdiData)
            {
                // Test
                //gfx.DrawEllipse(gdiData.pen, 0, 0, 20, 20);

                // Rescale Y to pixels
                if (gdiData.dataPoints != null)
                {
                    List<PointF> listPoints = new List<PointF>();
                    foreach(GdiDataPointType dataPoint in gdiData.dataPoints)
                    {
                        listPoints.Add(new PointF(dataPoint.point.X * this.Width, dataPoint.point.Y * this.Height));
                    }
                    gfx.DrawLines(gdiData.pen, listPoints.ToArray());
                }
            }
        } // Render()

        private void GraphView_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.dMousePos = new Point(this.dMousePos.X + e.Location.X - this.prevMousePos.X, this.dMousePos.Y + e.Location.Y - this.prevMousePos.Y);
            }
            else if (e.Button == MouseButtons.Right)
            {
                this.dMousePos = new Point();
            }
            this.prevMousePos = e.Location;
        } // GraphView_MouseMove()

        private void GraphView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.dMousePos = new Point();
        } // GraphView_MouseDoubleClick()

        private void timer60Hz_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        } // timer60Hz_Tick()
        #endregion UI_EVENTS

        //
        // Public Methods
        //
        #region PUBLIC_METHODS
        public void AddData(List<GraphDataType> listGraphData)
        {
            // Create the display object
            foreach (GraphDataType data in listGraphData)
            {
                GdiDataType gdiData = new GdiDataType();
                gdiData.name = data.name;
                gdiData.pen = data.pen;

                foreach (GraphDataPointType dataPoint in data.GetPoints())
                {
                    GdiDataPointType gdiDataPoint = new GdiDataPointType();
                    gdiDataPoint.data = dataPoint;
                    gdiDataPoint.point = new PointF(dataPoint.x, dataPoint.y);
                    gdiData.dataPoints.Add(gdiDataPoint);
                }

                // Add to local copy of all data types for this graph
                this.listGdiData.Add(gdiData);
            }
        } // AddData()
        #endregion PUBLIC_METHODS

    } // GraphView
} // NS
