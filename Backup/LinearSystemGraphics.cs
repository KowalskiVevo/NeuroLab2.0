using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Neuron
{
    public partial class LinearSystemGraphics : Form
    {
        int gCount = 0;
        private Drawer drawerError;
        private Drawer drawerState;

        public LinearSystemGraphics()
        {
            gCount = 0;
            InitializeComponent();
            drawerError = new Drawer(splitContainer.Panel1,splitContainer.Panel1.ClientRectangle);
            drawerState = new Drawer(splitContainer.Panel2,splitContainer.Panel2.ClientRectangle);
        }

        public void CreateErrorGraphic(List<PointF> ErrorPoints)
        {
            drawerError.GraphicBounds = new PointF(0, ErrorPoints.Count);
            drawerError.AddGraphic(1, ErrorPoints, "Ошибка сети");
            drawerError.Redraw();
        }

        public void SetGraphicBounds(PointF bounds)
        {
            drawerState.GraphicBounds = bounds;
        }

        public void AddGraphic(List<PointF> Points, String sInfo)
        {
            drawerState.AddGraphic(gCount++, Points, sInfo);
            drawerState.Redraw();
        }

        public void Clear()
        {
            gCount = 0;
            drawerError.Clear();
            drawerState.Clear();
        }
    }
}
