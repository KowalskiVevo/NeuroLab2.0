using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using _DGraphics;
using Parser;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace Neuron
{
    public partial class RegressionForm : Form
    {
        Drawer drawer2D;
        Graphics3D drawer3D;
        Parser.Parser parser = new Parser.Parser();
        Parser.Parser.Variable[] variables = new Parser.Parser.Variable[2];
        List<PointF> points = new List<PointF>();
        Random rand = new Random();
        float shift = 0.5f;
        bool pointsCreated = false, netCreated = false;
        _DGraphics.Graphics3D.Function3D mainFunction , netFunction , missFunction;

        //LinearNeuronNet net;
        NeuronNet net;
        public enum FunctionType
        { 
            ONE_ARGUMENT_FUNCTION,
            TWO_ARGUMENT_FUNCTION
        }
 
        FunctionType type;

        

        //public RegressionForm(FunctionType type , LinearNeuronNet net)
        public RegressionForm(FunctionType type, NeuronNet net)
        {
            InitializeComponent();
            this.net = net;
            this.type = type;
            mainFunction = new Graphics3D.Function3D(1 , Function3D);
            missFunction = new Graphics3D.Function3D(2 , Function3DWithMiss);
            netFunction = new Graphics3D.Function3D(3 , Function3DNet);
            CreateVisualisationControl();
        }

        private float Function2D(float x)
        {
            if (variables[0] != null) variables[0].value = x;
            float d = parser.Calculate();
            return parser.Calculate();
        }

        private float Function2DWithMiss(float x)
        {
            if (variables[0] != null) variables[0].value = x;
            
            return parser.Calculate() + shift * (float)(rand.NextDouble() - 0.5);
        }

        private float Function2DNet(float x)
        {
            net.Inputs[0].value = (x-net.biasX[0])/net.scaleX[0];
            net.Calculate();

            return net.LastNeuronGroup.Neurons[0].OUT * net.scaleY[0]+net.biasY[0];
        }

        private float Function3D(float x , float y)
        {
            if (variables[0] != null) variables[0].value = x;
            if (variables[1] != null) variables[1].value = y;

            return parser.Calculate();
        }

        private float Function3DWithMiss(float x, float y)
        {
            if (variables[0] != null) variables[0].value = x;
            if (variables[1] != null) variables[1].value = y;

            return parser.Calculate() + shift * (float)(rand.NextDouble() - 0.5);
        }

        private float Function3DNet(float x, float y)
        {
            net.Inputs[0].value = (x-net.biasX[0])/net.scaleX[0];
            net.Inputs[1].value = (y-net.biasX[0])/net.scaleX[0];

            net.Calculate();

            return net.LastNeuronGroup.Neurons[0].OUT * net.scaleY[0]+net.biasY[0];
        }

        private void CreateVisualisationControl()
        {
            switch (type)
            {
                case FunctionType.ONE_ARGUMENT_FUNCTION:
                    Z0.Enabled = ZN.Enabled = PointCountZ.Enabled = false;
                    Text = "F(X) = " + FunctionTxt.Text;
                    parser.InputString = FunctionTxt.Text;
                    variables[0] = parser.GetVariable("x");
                    drawer2D = new Drawer(DrawerContainer.Panel1, DrawerContainer.Panel1.ClientRectangle);
                    drawer2D.Dock = DockStyle.Fill;
                    drawer2D.AddGraphic(1 , Function2D, "Исходная функция",typeView.Line);
                    drawer2D.Redraw();
                    break;

                case FunctionType.TWO_ARGUMENT_FUNCTION:
                    Z0.Enabled = ZN.Enabled = PointCountZ.Enabled = true;
                    Text = "F(X,Y) = " + FunctionTxt.Text;
                    parser.InputString = FunctionTxt.Text;
                    variables[0] = parser.GetVariable("x");
                    variables[1] = parser.GetVariable("y");
                    drawer3D = new Graphics3D();
                    drawer3D.AddGraphic(mainFunction);
                    drawer3D.Parent = DrawerContainer.Panel1;                    
                    break;
            }
        }
        private void FunctionText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                switch (type)
                {
                    case FunctionType.ONE_ARGUMENT_FUNCTION:
                        parser.InputString = FunctionTxt.Text;
                        variables[0] = parser.GetVariable("x");
                        drawer2D.EnableCreatingBuffer();
                        drawer2D.Redraw();
                        break;

                    case FunctionType.TWO_ARGUMENT_FUNCTION:
                        parser.InputString = FunctionTxt.Text;
                        variables[0] = parser.GetVariable("x");
                        variables[1] = parser.GetVariable("y");
                        drawer3D.EnableCreateBuffers();
                        drawer3D.RecreateGraphic();
                        break;
                }

                Text = FunctionTxt.Text;
            }
        }

        private void ChangeIntervals(object sender, EventArgs e)
        {
            switch (type)
            {
                case FunctionType.ONE_ARGUMENT_FUNCTION:
                    drawer2D.GraphicBounds = new PointF(Single.Parse(X0.Text) , Single.Parse(XN.Text));
                    drawer2D.Redraw();
                    break;

                case FunctionType.TWO_ARGUMENT_FUNCTION:
                    drawer3D.XBounds = new Microsoft.DirectX.Vector2(Single.Parse(X0.Text), Single.Parse(XN.Text));
                    drawer3D.ZBounds = new Microsoft.DirectX.Vector2(Single.Parse(Z0.Text), Single.Parse(ZN.Text));
                    
                    drawer3D.EnableCreateBuffers();
                    drawer3D.RecreateGraphic();
                    
                    break;
            }
        }

        private void BuildByPOints(object sender, EventArgs e)
        {            
            switch (type)
            { 
                case FunctionType.ONE_ARGUMENT_FUNCTION:
            
                    drawer2D.AddGraphic(1 , Function2D, "Исходная функция",typeView.Line);
                    drawer2D.AddGraphic(2 , Function2DWithMiss , "Выборка",typeView.Rectangle);
                    drawer2D.Functions[1].PointsCount = Int32.Parse(PointCountX.Text);
                    drawer2D.EnableCreatingBuffer();
                    drawer2D.Redraw();
                    break;

                case FunctionType.TWO_ARGUMENT_FUNCTION:
                                      
                    drawer3D.AddGraphic(missFunction);
                    drawer3D.EnableCreateBuffers();
                    drawer3D.RecreateGraphic();
                    break;
            }

            pointsCreated = true;
        }

        private void PointsCountChanged(object sender, EventArgs e)
        {
            shift = Single.Parse(ShiftValue.Text);
            BuildByPOints(this , EventArgs.Empty);
        }

        private void BuildNet(object sender, EventArgs e)
        {
            if (!pointsCreated) return;

            StudyPair pair;
            int pointCount = Int32.Parse(PointCountX.Text);

            switch (type)
            { 
                case FunctionType.ONE_ARGUMENT_FUNCTION:
                    net.StudyPairs.Clear();
                    net.InputsCount = 1;
                    net.OutputsCount = 1;
                    
                    PointF [] pts = drawer2D.GetPoints(1);                   

                    for (int i = 0; i < pts.Length; i++)
                    { 
                        pair = new StudyPair();
                        pair.inputs.Add(pts[i].X);
                        pair.quits.Add(pts[i].Y);

                        net.StudyPairs.Add(pair);
                    }
                    net.LinearNormalizeInputs();
                    net.LinearNormalizeOutputs();
                    drawer2D.AddGraphic(3 , Function2DNet , "Выход сети",typeView.Line);
                    drawer2D.Functions[2].NeedCreateBuffer = true;
                    drawer2D.Redraw();
                    break;

                case FunctionType.TWO_ARGUMENT_FUNCTION:
                    int pointCountX = Int32.Parse(PointCountX.Text);
                    int pointCountY = Int32.Parse(PointCountZ.Text);
                    net.StudyPairs.Clear();
                    net.InputsCount = 2;
                    net.OutputsCount = 1;
                    Vector3[] p = drawer3D.GetGraphicPoints(1, pointCountX, pointCountY);
                   
                    for (int i = 0; i < p.Length; i++)
                    {                        
                        pair = new StudyPair();
                        pair.inputs.Add(p[i].X);
                        pair.inputs.Add(p[i].Z);
                        pair.quits.Add(p[i].Y);

                        net.StudyPairs.Add(pair);
                    }
                    net.LinearNormalizeInputs();
                    net.LinearNormalizeOutputs();    
                    drawer3D.AddGraphic(netFunction);
                    drawer3D.EnableCreateBuffers();
                    drawer3D.RecreateGraphic();
                    break;
            }

            net.StudyPairsLoaded = true;
            netCreated = true;
        }

        private void StudyNet(object sender, EventArgs e)
        {
            if (!netCreated) return;

            int count = net.EraCount / 100;
            if (count == 0) count = 1;

            int pointCount = Int32.Parse(PointCountX.Text);
            StudyProgress.Value = 0;
            net.errors.Clear();
            switch (type)
            {
                case FunctionType.ONE_ARGUMENT_FUNCTION:
                    for (int i = 0; i < 100; i++)
                    {                        
                        net.StudyByParts(count , i * count);
                        drawer2D.Functions[2].NeedCreateBuffer = true;
                        drawer2D.Redraw();
                        StudyProgress.Value++;
                    }
                    break;

                case FunctionType.TWO_ARGUMENT_FUNCTION:
                    for (int i = 0; i < 100; i++)
                    {
                        net.StudyByParts(count , i * count);
                        drawer3D.EnableCreateBuffers();
                        drawer3D.Functions[2].NeedCreateBuffer = true;
                        drawer3D.RecreateGraphic();
                        StudyProgress.Value++;
                    }                    
                    break;
            }
 //           ShowErrorForm errorForm = new ShowErrorForm(net.ToStandartSize());
 //           errorForm.drawer.GraphicBounds = new PointF(0, net.errors.Last().X + 1);
 //           errorForm.Show();
 //           errorForm.drawer.Redraw();
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            switch (type)
            {
                case FunctionType.ONE_ARGUMENT_FUNCTION:
                    if (drawer2D.Functions.Count > 0) drawer2D.Functions[0].ShowGraphic = SourceFunction.Checked;
                    if (drawer2D.Functions.Count > 1) drawer2D.Functions[1].ShowGraphic = RandomFunction.Checked;
                    if (drawer2D.Functions.Count > 2) drawer2D.Functions[2].ShowGraphic = NetOutput.Checked;
                    drawer2D.Redraw();
                    break;
                case FunctionType.TWO_ARGUMENT_FUNCTION:
                    if (drawer3D.FunctionsCount > 0) drawer3D.Functions[0].ShowGraphic = SourceFunction.Checked;
                    if (drawer3D.FunctionsCount > 1) drawer3D.Functions[1].ShowGraphic = RandomFunction.Checked;
                    if (drawer3D.FunctionsCount > 2) drawer3D.Functions[2].ShowGraphic = NetOutput.Checked;
                    drawer3D.EnableCreateBuffers();
                    drawer3D.RecreateGraphic();
                    break;
            }
        }
    }
}
