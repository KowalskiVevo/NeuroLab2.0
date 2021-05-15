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
    public partial class NeuronInformation : Form
    {
        Neuron neuronInformation;
        Drawer drawer;

        float f(float x)
        {
            return (float)Math.Sin(x);
        }


        public NeuronInformation(Neuron neuron)
        {
            InitializeComponent();
            neuronInformation = neuron;
            
            functionBox.SelectedIndex = (int)neuronInformation.activationFunction;
            //functionType.SelectedIndex = (int)neuronInformation.neuronType;
            drawer = new Drawer(this, new Rectangle(10, functionBox.Location.Y + functionBox.Height + 20, Width - 30, Height - functionBox.Height - 100));
            drawer.Interpolate = false;
            drawer.AddGraphic(1 , neuronInformation.ActivateFunction, functionBox.SelectedText,typeView.Line);
            drawer.Redraw();            
        }

        private void functionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            neuronInformation.activationFunction = (ActivationFunction)functionBox.SelectedIndex;

            if (drawer != null) { drawer.Functions[0].NeedCreateBuffer = true; drawer.Redraw(); }
        }

        private void functionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //neuronInformation.neuronType = (NeuronType)functionType.SelectedIndex;

            //if (drawer != null) { drawer.needCreateBuffer = true; drawer.Redraw(); }
        }
    }
}
