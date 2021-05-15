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
    public partial class Form1 : Form
    {
        NeuronNet net = new LinearNeuronNet();
        NeuronGraphics nGraphics = new NeuronGraphics();

        public Form1()
        {
            InitializeComponent();

            nGraphics.Dock = DockStyle.Fill;

           
            nGraphics.Parent = this;
            
            nGraphics.Net = net;
            nGraphics.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            net.Calculate();
            nGraphics.Refresh();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            nGraphics.CalculateObjectsPositions();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (nGraphics.loadSaveTasksForm.ConnectionForm.ConnectToDatabase != null)
            nGraphics.loadSaveTasksForm.ConnectionForm.ConnectToDatabase.CloseConnection();
        }
    }
}
