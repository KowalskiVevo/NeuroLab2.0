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
    public partial class NetOptions : Form
    {
        NeuronNet net;

        public NetOptions(NeuronNet Net)
        {
            InitializeComponent();
            net = Net;
            inputsCount.Value = Net.InputsCount;
            groupsCount.Value = Net.NeuronGroupsCount;
            groupNumber.Value = 1;
            neuronsCount.Value = Net.NeuronGroups[0].Neurons.Count;
        }

        public int InputsCount
        {
            get 
            {
                return (int)inputsCount.Value;
            }
            set 
            {
                inputsCount.Value = value;
            }
        }

        public void UpdateNetInformation()
        {
            inputsCount.Value = net.InputsCount;
            groupsCount.Value = net.NeuronGroupsCount;
            groupNumber.Value = 1;
            neuronsCount.Value = net.NeuronGroups[0].Neurons.Count;
        }

        private void NetOptions_Load(object sender, EventArgs e)
        {

        }

        private void inputsCount_ValueChanged(object sender, EventArgs e)
        {
            ((NeuronNet)net).InputsCount = (int)inputsCount.Value;
        }

        private void groupsCount_ValueChanged(object sender, EventArgs e)
        {
            ((NeuronNet)net).NeuronGroupsCount = (int)groupsCount.Value;
            groupNumber.Maximum = groupsCount.Value; 
        }

        private void neuronsCount_ValueChanged(object sender, EventArgs e)
        {
            net.SetNeuronsCount((int)groupNumber.Value - 1, (int)neuronsCount.Value);
        }

        private void groupNumber_ValueChanged(object sender, EventArgs e)
        {
            neuronsCount.Value = ((NeuronNet)net).NeuronGroups[(int)groupNumber.Value - 1].Neurons.Count;
        }

        private void eraCount_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void StudyStepText_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                net.E = Single.Parse(StudyStepText.Text);
                net.EraCount = (int)eraCount.Value;
                Sinaps.Interval = new PointF(Single.Parse(IntervalFrom.Text), Single.Parse(IntervalTo.Text));
            }
            catch (Exception)
            {
                
            }
        }        
    }
}
