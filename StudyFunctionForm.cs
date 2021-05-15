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
    public partial class StudyFunctionForm : Form
    {
        Drawer drawer;
        NeuronNet net;

        public StudyFunctionForm(NeuronNet net)
        {
            InitializeComponent();
            this.net = net;
            drawer = new Drawer(pictureBox1, pictureBox1.ClientRectangle);
            drawer.GraphicBounds = new PointF(0 , net.EraCount);
            drawer.AddGraphic(1, net.StudyFunction, "Расписание обучения",typeView.Line);
            drawer.Redraw();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                net.parser.InputString = textBox1.Text;
                net.VariableN = net.parser.GetVariable("n");
                net.VariableI = net.parser.GetVariable("i");
                net.VariableD = net.parser.GetVariable("d");
                net.VariableM = net.parser.GetVariable("m");
                if (net.VariableM != null) net.VariableM.value = Single.Parse(textBox3.Text);
                if (net.VariableD != null) net.VariableD.value = Single.Parse(textBox2.Text);
                drawer.EnableCreatingBuffer();
                drawer.Redraw();
                drawer.Refresh();
            }
        }
    }
}
