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
    public partial class NeuroninputInformation : Form
    {
        NeuronInput neuroninput;

        public NeuroninputInformation(NeuronInput ninput)
        {
            InitializeComponent();
            neuroninput = ninput;
            inputValueText.Text = neuroninput.value.ToString("0.00");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            neuroninput.value = Convert.ToSingle(inputValueText.Text);
            Close();
        }
    }
}
