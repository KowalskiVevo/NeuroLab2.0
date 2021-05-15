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
    public partial class ClassificationGraphicsForm : Form
    {
        ClassificationGraphics clGraphics;
        
        public ClassificationGraphicsForm(LinearNeuronNet net)
        {
            InitializeComponent();
            clGraphics = new ClassificationGraphics(net);
            clGraphics.Dock = DockStyle.Fill;
            clGraphics.Parent = this;            
        }

        public ClassificationGraphicsForm(KohonenNeuronNet net)
        {
            InitializeComponent();
            clGraphics = new ClassificationGraphics(net);
            clGraphics.Dock = DockStyle.Fill;
            clGraphics.Parent = this;            
        }
    }
}