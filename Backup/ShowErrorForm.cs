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
    public partial class ShowErrorForm : Form
    {
        public Drawer drawer;

        public ShowErrorForm(List<PointF> points)
        {
            InitializeComponent();
            drawer = new Drawer(this, ClientRectangle);
            drawer.enableShifting = drawer.enableZoom = false;
            drawer.Interpolate = false;
            drawer.AddGraphic(1 , points, "Характеристика работы сети");       
        }

        private void ShowErrorForm_Load(object sender, EventArgs e)
        {           
            
        }


    }
}
