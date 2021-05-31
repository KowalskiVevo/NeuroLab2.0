using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.SQLite;
using System.IO;
using Database;
//using ServiceStack.Text;
using CsvHelper;
using System.Globalization;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using Vse.Web.Serialization;
using System.Text.Encodings.Web;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Threading;
using Newtonsoft.Json.Serialization;
using static Neuron.Root;

namespace Neuron
{
    public partial class NeuronGraphics : UserControl
    {
        NeuronNet net;
        float inputSize = 16 , neuronSize = 20;
        Neuron choosedNeuron = null;
        NeuronInput choosedinputNeuron = null;
        Graphics memGraphics;
        Bitmap memBitmap;
        PaintEventArgs paintArgs;
        public NetOptions netOptions;
        Database databaseSQLite = new Database();

        Rectangle currentRect = new Rectangle();
        MouseStatus mStatus = new MouseStatus();
        bool selectionRectangle = false;
        MenuStrip mainMenu = new MenuStrip();
        NeuronNetType type = NeuronNetType.LINEAR;
        Task currentTask = Task.NONE;
        public LoadSaveTasks loadSaveTasksForm;

        public bool RestartCGF = false;

        public enum NeuronNetType
        { 
            LINEAR = 0, 
            KOHONEN = 1,
            HOPFIELD = 2,
            RBF = 3,
            SHUFFLE = 4
        }

        public enum Task
        { 
            NONE,
            RECOGNIZE_IMAGE,
            RESTORE_IMAGE,
            CLASSIFICATION,
            CLASTERIZATION,
            REGRESSION2D,
            REGRESSION3D,
            REGRESSION2D_RBF,
            REGRESSION3D_RBF
        }

        public NeuronGraphics()
        {
            InitializeComponent();

            ChangeBimapSettings();
            splitContainer1.Panel1.Paint += new PaintEventHandler(NeuronGraphics_Paint);
            splitContainer1.Panel1.MouseDoubleClick += OnMouseDoubleClick;
            splitContainer1.Panel1.MouseDown += OnMouseDown;
            splitContainer1.Panel1.MouseMove += OnMouseMove;
            splitContainer1.Panel1.MouseUp += OnMouseUp;
            splitContainer1.Panel1.MouseWheel += new MouseEventHandler(Panel1_MouseWheel);
            
            //Location = new Point(10,10);
            Dock = DockStyle.Fill;
            loadSaveTasksForm = new LoadSaveTasks();
        }

        void Panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            float width = splitContainer1.Panel1.Width / (net.NeuronGroupsCount + 1); 
            int layer = (int)(e.X / width);
            int add = e.Delta > 0 ? 1 : -1;

            if (layer >= net.NeuronGroupsCount) return;

            if (layer == 0) net.InputsCount += add;
            else net.SetNeuronsCount(layer - 1, net.NeuronGroups[layer - 1].Neurons.Count + add);

            netOptions.UpdateNetInformation();
        }

        private void ChangeBimapSettings()
        {
            memBitmap = new Bitmap(splitContainer1.Panel1.Width, splitContainer1.Panel1.Height);
            memGraphics = Graphics.FromImage(memBitmap);
            memGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            memGraphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            paintArgs = new PaintEventArgs(splitContainer1.Panel1.CreateGraphics(), splitContainer1.Panel1.ClientRectangle);
        }

        void NeuronGraphics_Paint(object sender, PaintEventArgs e)
        {            
            Draw();
            e.Graphics.DrawImage(memBitmap, new Point(0, 0));        
        }

        public NeuronNet Net
        {
            get 
            {
                if (net != null) return net;
                return null;
            }
            set 
            {
                net = value;
                net.GraphicsNeuron = this;
                net.InitializeParser();
                if(netOptions != null) netOptions.Close();
                netOptions = new NetOptions(net);
                netOptions.Show();
                CalculateObjectsPositions();
            }
        }

        public void CalculateObjectsPositions()
        {
            if (net != null)
            {                
                int columnCount = net.NeuronGroups.Count + 1;
                float inputXPos, inputYPos;
                float XInc = inputXPos = (float)splitContainer1.Panel1.Width / (columnCount + 1);
                float inputYInc = inputYPos = (float)splitContainer1.Panel1.Height / (net.PaintedInputsCount + 1);
                float neuronGroupXPos;
                float neuronGroupYPos;
                float neuronGroupYInc;
               
                for (int i = 0; i < net.PaintedInputsCount; i++, inputYPos += inputYInc)
                {
                    net.Inputs[i].Position.X = inputXPos;
                    net.Inputs[i].Position.Y = inputYPos;
                    net.Inputs[i].wasPainted = true;
                }

                for (int group = 0; group < net.NeuronGroups.Count; group++)
                {
                    neuronGroupXPos = 2 * XInc + XInc * group;
                    neuronGroupYInc = neuronGroupYPos = (float)splitContainer1.Panel1.Height / (net.NeuronGroups[group].PaintedNeuronsCount + 1);

                    for (int neuron = 0; neuron < net.NeuronGroups[group].PaintedNeuronsCount; neuron++, neuronGroupYPos += neuronGroupYInc)
                    {
                        net.NeuronGroups[group].Neurons[neuron].Position.X = neuronGroupXPos;
                        net.NeuronGroups[group].Neurons[neuron].Position.Y = neuronGroupYPos;
                        net.NeuronGroups[group].Neurons[neuron].wasPainted = true;                       
                    }
                }             
            }
           
            NeuronGraphics_Paint(this, paintArgs);
        }

        public override void Refresh()
        {
            NeuronGraphics_Paint(this, paintArgs); 
        }

        private void Draw()
        {
            int objID;
            PointF zero = new PointF();
            Pen pen = new Pen(Color.Black , 2);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            memGraphics.Clear(Color.WhiteSmoke);
            StringFormat str = new StringFormat();
            str.Alignment = StringAlignment.Center;
            str.LineAlignment = StringAlignment.Center;

            if (net != null)
            {
                if (type == NeuronNetType.HOPFIELD)
                {
                    DrawHopfieldNet();
                    if (selectionRectangle) memGraphics.DrawRectangle(Pens.Black, currentRect);
                    return;
                }

                PointF inputPosition;

                for (int group = 0; group < net.NeuronGroups.Count; group++)
                {
                    for (int neuron = 0; neuron < net.NeuronGroups[group].PaintedNeuronsCount; neuron++)
                    {
                        for (int sinaps = 0; sinaps < net.NeuronGroups[group].Neurons[neuron].sinapses.Count; sinaps++)
                        {
                            inputPosition = net.NeuronGroups[group].Neurons[neuron].sinapses[sinaps].frominput == null ? net.NeuronGroups[group].Neurons[neuron].sinapses[sinaps].fromNeuron.Position : net.NeuronGroups[group].Neurons[neuron].sinapses[sinaps].frominput.Position;
                            if (PointF.Equals(inputPosition, zero)) continue;
                            memGraphics.DrawLine(new Pen(Color.Black, 2), inputPosition, net.NeuronGroups[group].Neurons[neuron].Position);

                            //memGraphics.DrawString(net.NeuronGroups[group].Neurons[neuron].sinapses[sinaps].value.ToString("0.000"), new Font("Arial", 8), Brushes.Black, new PointF((inputPosition.X + net.NeuronGroups[group].Neurons[neuron].Position.X) / 2, (inputPosition.Y + net.NeuronGroups[group].Neurons[neuron].Position.Y) / 2));
                        }

                        memGraphics.FillRectangle(net.NeuronGroups[group].Neurons[neuron].active ? Brushes.Red : Brushes.Gray, new RectangleF(net.NeuronGroups[group].Neurons[neuron].Position.X - neuronSize / 2, net.NeuronGroups[group].Neurons[neuron].Position.Y - neuronSize / 2, neuronSize, neuronSize));
                        memGraphics.DrawString(String.Format( "({0} | {1})" , net.NeuronGroups[group].Neurons[neuron].NET.ToString("0.00"), net.NeuronGroups[group].Neurons[neuron].OUT.ToString("0.00") ), new Font("Arial", 8), Brushes.Black, new PointF(net.NeuronGroups[group].Neurons[neuron].Position.X, net.NeuronGroups[group].Neurons[neuron].Position.Y - neuronSize * 1.5f));
                        objID = neuron + 1;

                        if (!net.NeuronGroups[group].allNeuronsWasPainted && neuron == net.NeuronGroups[group].PaintedNeuronsCount - 1)
                        {
                            objID = net.NeuronGroups[group].Neurons.Count;
                            memGraphics.DrawLine(pen, net.NeuronGroups[group].Neurons[neuron].Position.X - 50, (net.NeuronGroups[group].Neurons[neuron].Position.Y + net.NeuronGroups[group].Neurons[neuron - 1].Position.Y) / 2, net.NeuronGroups[group].Neurons[neuron].Position.X + 50, (net.NeuronGroups[group].Neurons[neuron].Position.Y + net.NeuronGroups[group].Neurons[neuron - 1].Position.Y) / 2);
                        }

                        memGraphics.DrawString(objID.ToString(), new Font("Arial", 10), Brushes.Black, net.NeuronGroups[group].Neurons[neuron].Position.X, net.NeuronGroups[group].Neurons[neuron].Position.Y + 20 , str); 
                    }
                }                

                for (int i = 0; i < net.PaintedInputsCount; i++)
                {
                    memGraphics.FillEllipse(Brushes.Green, new RectangleF(net.Inputs[i].Position.X - inputSize / 2, net.Inputs[i].Position.Y - inputSize / 2, inputSize, inputSize));
                    memGraphics.DrawString(net.Inputs[i].value.ToString("0.00"), new Font("Arial", 8), Brushes.Black, new PointF(net.Inputs[i].Position.X - inputSize * 1.5f, net.Inputs[i].Position.Y - inputSize * 1.5f));
                    objID = i + 1;

                    if (!net.allInputsWasPainted && i == net.PaintedInputsCount - 1)
                    {
                        objID = net.InputsCount;
                        memGraphics.DrawLine(pen, net.Inputs[i].Position.X - 50, (net.Inputs[i].Position.Y + net.Inputs[i - 1].Position.Y) / 2, net.Inputs[i].Position.X + 50, (net.Inputs[i].Position.Y + net.Inputs[i - 1].Position.Y) / 2);
                    }
                    
                    memGraphics.DrawString(objID.ToString(), new Font("Arial", 10), Brushes.Black, new PointF(net.Inputs[i].Position.X, net.Inputs[i].Position.Y + 20) , str);
                    
                }
            }

            if (type == NeuronNetType.SHUFFLE)
                DrawShuffleNet();

            if(selectionRectangle) memGraphics.DrawRectangle(Pens.Black, currentRect);
        }

        private void DrawHopfieldNet()
        {
            Pen pen = new Pen(Color.Black, 2);
            StringFormat str = new StringFormat();
            str.Alignment = StringAlignment.Center;
            str.LineAlignment = StringAlignment.Center;
            float centerX = (net.Inputs[0].Position.X + net.LastNeuronGroup.Neurons[0].Position.X) / 2;
            memGraphics.DrawLine(pen, new PointF(centerX, net.LastNeuronGroup.Neurons[0].Position.Y - 50), new PointF(centerX, net.LastNeuronGroup.Neurons[net.LastNeuronGroup.PaintedNeuronsCount - 1].Position.Y));
            int objID = 1;

            for (int i = 0; i < net.PaintedInputsCount; i++ , objID++)
            {
                memGraphics.DrawLine(pen, net.Inputs[i].Position, new PointF(net.LastNeuronGroup.Neurons[i].Position.X + 80, net.LastNeuronGroup.Neurons[i].Position.Y));
                memGraphics.DrawLine(pen, new PointF(net.LastNeuronGroup.Neurons[i].Position.X + 50, net.LastNeuronGroup.Neurons[i].Position.Y), new PointF(net.LastNeuronGroup.Neurons[i].Position.X + 50, net.LastNeuronGroup.Neurons[i].Position.Y - 50));
                memGraphics.DrawLine(pen, new PointF(net.LastNeuronGroup.Neurons[i].Position.X + 50, net.LastNeuronGroup.Neurons[i].Position.Y - 50) , new PointF(centerX, net.LastNeuronGroup.Neurons[i].Position.Y - 50));
                
                memGraphics.FillEllipse(Brushes.Green, new RectangleF(net.Inputs[i].Position.X - inputSize / 2, net.Inputs[i].Position.Y - inputSize / 2, inputSize, inputSize));
                memGraphics.FillRectangle(Brushes.Gray, net.LastNeuronGroup.Neurons[i].Position.X - neuronSize / 2, net.LastNeuronGroup.Neurons[i].Position.Y - neuronSize / 2, neuronSize, neuronSize);
                if (!net.allInputsWasPainted && i == net.PaintedInputsCount - 1) objID = net.InputsCount; 
                memGraphics.DrawString(objID.ToString(), new Font("Arial", 10), Brushes.Black, new PointF(net.Inputs[i].Position.X, net.Inputs[i].Position.Y + 20), str);
                memGraphics.DrawString(objID.ToString(), new Font("Arial", 10), Brushes.Black, net.LastNeuronGroup.Neurons[i].Position.X, net.LastNeuronGroup.Neurons[i].Position.Y + 20, str); 
                
            }
        }

        private void DrawShuffleNet()
        {
            Pen pen = new Pen(Color.Black, 2);
            StringFormat str = new StringFormat();
            str.Alignment = StringAlignment.Center;
            str.LineAlignment = StringAlignment.Center;
            float centerX = (net.Inputs[0].Position.X);
            memGraphics.DrawLine(pen, new PointF(centerX, net.LastNeuronGroup.Neurons[0].Position.Y - 50), new PointF(centerX, net.LastNeuronGroup.Neurons[net.LastNeuronGroup.PaintedNeuronsCount - 1].Position.Y));
            int objID = 1;

            for (int i = 0; i < net.PaintedInputsCount; i++, objID++)
            {
                if (i < net.NeuronGroups[0].Neurons.Count)
                    memGraphics.DrawLine(pen, net.NeuronGroups[0].Neurons[i].Position, new PointF(net.LastNeuronGroup.Neurons[i].Position.X + 80, net.LastNeuronGroup.Neurons[i].Position.Y));

                memGraphics.DrawLine(pen, new PointF(net.LastNeuronGroup.Neurons[i].Position.X + 50, net.LastNeuronGroup.Neurons[i].Position.Y), new PointF(net.LastNeuronGroup.Neurons[i].Position.X + 50, net.LastNeuronGroup.Neurons[i].Position.Y - 50));
                memGraphics.DrawLine(pen, new PointF(net.LastNeuronGroup.Neurons[i].Position.X + 50, net.LastNeuronGroup.Neurons[i].Position.Y - 50), new PointF(centerX, net.LastNeuronGroup.Neurons[i].Position.Y - 50));

                memGraphics.FillEllipse(Brushes.Green, new RectangleF(net.Inputs[i].Position.X - inputSize / 2, net.Inputs[i].Position.Y - inputSize / 2, inputSize, inputSize));
                memGraphics.FillRectangle(Brushes.Gray, net.LastNeuronGroup.Neurons[i].Position.X - neuronSize / 2, net.LastNeuronGroup.Neurons[i].Position.Y - neuronSize / 2, neuronSize, neuronSize);

                if (!net.allInputsWasPainted && i == net.PaintedInputsCount - 1) objID = net.InputsCount;
                memGraphics.DrawString(objID.ToString(), new Font("Arial", 10), Brushes.Black, new PointF(net.Inputs[i].Position.X, net.Inputs[i].Position.Y + 20), str);
                memGraphics.DrawString(objID.ToString(), new Font("Arial", 10), Brushes.Black, net.LastNeuronGroup.Neurons[i].Position.X, net.LastNeuronGroup.Neurons[i].Position.Y + 20, str);

            }
        }

        private Neuron GetNeuronByPosition(PointF e)
        {
            for (int i = 0; i < net.NeuronGroups.Count; i++)
            {
                for (int j = 0; j < net.NeuronGroups[i].Neurons.Count; j++)
                {
                    if (e.X > net.NeuronGroups[i].Neurons[j].Position.X - neuronSize / 2 && e.X < net.NeuronGroups[i].Neurons[j].Position.X + neuronSize / 2 &&
                       e.Y > net.NeuronGroups[i].Neurons[j].Position.Y - neuronSize / 2 && e.Y < net.NeuronGroups[i].Neurons[j].Position.Y + neuronSize / 2)
                    {
                        return net.NeuronGroups[i].Neurons[j];
                    }
                }
            }

            return null;
        }

        private NeuronInput GetinputByPosition(PointF e)
        {
            for (int i = 0; i < net.Inputs.Count; i++)
            {
                if (e.X > net.Inputs[i].Position.X - inputSize / 2 && e.X < net.Inputs[i].Position.X + inputSize / 2 &&
                    e.Y > net.Inputs[i].Position.Y - inputSize / 2 && e.Y < net.Inputs[i].Position.Y + inputSize / 2)
                {                     
                    return net.Inputs[i];
                }
            }

            return null;
        }

        protected void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if ((choosedinputNeuron = GetinputByPosition(e.Location)) != null)
                {
                    NeuroninputInformation dlg = new NeuroninputInformation(choosedinputNeuron);
                    dlg.ShowDialog();
                    Refresh();
                }
                else if( ( choosedNeuron = GetNeuronByPosition(e.Location) ) != null )
                {
                    NeuronInformation dlg = new NeuronInformation(choosedNeuron);
                    dlg.ShowDialog();
                }
            }
        }

        protected void OnMouseDown(object sender, MouseEventArgs e)
        {
            mStatus.DownEvent(e);
            ((Control)sender).Focus();

            if (e.Button == MouseButtons.Left)
            {
               if( ( choosedinputNeuron = GetinputByPosition(e.Location) ) == null ) choosedNeuron = GetNeuronByPosition(e.Location);
               
               currentRect.Location = new Point(e.X , e.Y);
               selectionRectangle = true;
            }
        }

        protected void OnMouseUp(object sender, MouseEventArgs e)
        {            
            if (e.Button == MouseButtons.Left)
            {
                choosedinputNeuron = null;
                choosedNeuron = null;

                net.SetSelection(net.GetNeuronsByRectangle(currentRect));
                selectionRectangle = false;
                currentRect = new Rectangle();
                Refresh();
            }

            mStatus.UpEvent(e);
        }

        protected void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (mStatus.LeftButtonDown)
            {
                currentRect.Size = new Size(e.X - currentRect.X, e.Y - currentRect.Y);
                Refresh();
            }

            if (choosedinputNeuron != null)
            {
                choosedinputNeuron.positionChanged = true;
                choosedinputNeuron.Position = e.Location;
                NeuronGraphics_Paint(this , paintArgs);
            }
            if (choosedNeuron != null)
            {
                choosedNeuron.positionChanged = true;
                choosedNeuron.Position = e.Location;
                NeuronGraphics_Paint(this, paintArgs);
            }
        }        

        private void CalculateNet(object sender, EventArgs e)
        {
            net.Calculate();
            Refresh();
        }

        private void SetStudyPairs(object sender, EventArgs e)
        {
            StudyPairModifier dlg = new StudyPairModifier(net.StudyPairs, net);
            if (dlg.ShowDialog() == DialogResult.OK) netOptions.InputsCount = net.InputsCount;
        }

        private void NeuronGroupOptions(object sender, EventArgs e)
        {
            Neuron example = new Neuron(net.currentSelection);
            NeuronInformation nInfo = new NeuronInformation(example);
            nInfo.ShowDialog();

            net.SetSelectionGroupParameters(example);
            net.ClearSelectionGroup();
            Refresh();
        }

        private void RandomWeights(object sender, EventArgs e)
        {
            net.SetSinapses();
            Refresh();
        }

        private void StudyNet(object sender, EventArgs e)
        {
            if (net.StudyPairsLoaded)
            {
                StudyStatus.Maximum = net.EraCount;
                StudyStatus.Value = 0;
                net.Study();
                Refresh();                
            }
        }

        private void CreateNetTree()
        {
            TreeNode node , sinapsNode;
            NetTree.Nodes.Clear();
            NetTree.Nodes.Add("Нейронная сеть");
            NetTree.Nodes[0].Nodes.Add("Входы (" + net.InputsCount.ToString() + ")");
            NetTree.Nodes[0].Nodes.Add("Нейронные слои (" + net.NeuronGroupsCount.ToString() + ")");

            for (int i = 0; i < net.InputsCount; i++)
            {
                NetTree.Nodes[0].Nodes[0].Nodes.Add("Значение - " + net.Inputs[i].value.ToString("0.00"));
            }

            for (int i = 0; i < net.NeuronGroupsCount; i++)
            {
                node = NetTree.Nodes[0].Nodes[1].Nodes.Add("Слой - " + (i + 1).ToString() + string.Format(" ({0})" , net.NeuronGroups[i].Neurons.Count));

                for (int j = 0; j < net.NeuronGroups[i].Neurons.Count; j++)
                {
                    node.Nodes.Add("Нейрон - " + (j + 1).ToString());
                    node.Nodes[j].Nodes.Add("Активационная функция - " + net.NeuronGroups[i].Neurons[j].activationFunction.ToString());
                    node.Nodes[j].Nodes.Add("OUT - " + net.NeuronGroups[i].Neurons[j].OUT.ToString("0.00"));
                    sinapsNode = node.Nodes[j].Nodes.Add("Синапсы нейрона" + string.Format(" ({0})" , net.NeuronGroups[i].Neurons[j].sinapses.Count));

                    for (int k = 0; k < net.NeuronGroups[i].Neurons[j].sinapses.Count; k++)
                    {
                        sinapsNode.Nodes.Add("Вес синапса - " + net.NeuronGroups[i].Neurons[j].sinapses[k].value.ToString("0.00"));    
                    }
                }
            }

            NetTree.ExpandAll();
        }

        public void RecognizeImages(object sender, EventArgs e)
        {

            LinearNetType(this, EventArgs.Empty);

            ColorGridForm colorForm = new ColorGridForm((LinearNeuronNet)net);
            colorForm.neuronGraphics = this;
            colorForm.Show();
        }

        public void ClassificationObjects(object sender, EventArgs e)
        {
            LinearNetType(this, EventArgs.Empty);

            ClassificationGraphicsForm cfForm = new ClassificationGraphicsForm((LinearNeuronNet)net);
            cfForm.Show();
        }

        private void операцииToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public void Regression2D(object sender, EventArgs e)
        {
            LinearNetType(this, EventArgs.Empty);

            RegressionForm form = new RegressionForm(RegressionForm.FunctionType.ONE_ARGUMENT_FUNCTION, (LinearNeuronNet)net);
            form.Show();
        }

        public void Regression3D(object sender, EventArgs e)
        {
            LinearNetType(this, EventArgs.Empty);

            RegressionForm form = new RegressionForm(RegressionForm.FunctionType.TWO_ARGUMENT_FUNCTION, (LinearNeuronNet)net);
            form.Show();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            CreateNetTree();
        }

        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            ChangeBimapSettings();
            CalculateObjectsPositions();            
        }

        public void LinearNetType(object sender, EventArgs e)
        {
            loadSaveTasksForm.Net = net = new LinearNeuronNet();
            type = NeuronNetType.LINEAR;
            this.Net = net;
            Refresh();
        }

        public void KohonenType(object sender, EventArgs e)
        {
            loadSaveTasksForm.Net = net = new KohonenNeuronNet();
            type = NeuronNetType.KOHONEN;
            this.Net = net;
            Refresh();
        }

        public void Clasterization(object sender, EventArgs e)
        {
            KohonenType(this, EventArgs.Empty);

            ClassificationGraphicsForm cfForm = new ClassificationGraphicsForm((KohonenNeuronNet)net);
            cfForm.Show();
        }

        public void NetView(object sender, EventArgs e)
        {
            if (net.InputsCount != 2 || net.OutputsCount != 1) return;
            if (type != NeuronNetType.LINEAR) return;

            NetViewForm netViewForm = new NetViewForm(((LinearNeuronNet)net).GetNetView());
            netViewForm.ShowDialog();
        }

        private void StudyFunction(object sender, EventArgs e)
        {
            StudyFunctionForm form = new StudyFunctionForm(net);
            form.Show();
        }

        public void HopfieldType(object sender, EventArgs e)
        {
            loadSaveTasksForm.Net = net = new HopfieldNeuronNet();
            type = NeuronNetType.HOPFIELD;
            this.Net = net;
            Refresh();
        }

        public void AssociationMemory(object sender, EventArgs e)
        {
            HopfieldType(this, EventArgs.Empty);

            AssociationMemoryForm form = new AssociationMemoryForm((HopfieldNeuronNet)net);
            form.Show();
        }

        private string GetCurrentTask()
        {
            switch (currentTask)
            {
                case Task.NONE: return "Задача не определена";
                case Task.CLASSIFICATION: return "Классификация объектов";
                case Task.CLASTERIZATION: return "Задача кластер-анализа";
                case Task.RECOGNIZE_IMAGE: return "Распознавание образов";
                case Task.REGRESSION2D: return "Регрессия функции одной переменной";
                case Task.REGRESSION3D: return "Регрессия функции двух переменных";
                case Task.RESTORE_IMAGE: return "Восстановление образов";
            }

            return "Задача не определена";
        }

        private string GetCurrentNetType()
        {
            switch (type)
            {
                case NeuronNetType.LINEAR: return "Многослойный персептрон";
                case NeuronNetType.KOHONEN: return "Сеть Кохонена";
                case NeuronNetType.HOPFIELD: return "Сеть Хопфилда";
            }

            return "";
        }

        private void LoadStudyExamples(object sender, EventArgs e)
        {
            loadSaveTasksForm.Save = false;
            loadSaveTasksForm.Net = net;
            //if (loadSaveTasksForm.ConnectToDatabase())
            //{
            //    loadSaveTasksForm.LoadExamples();
            //    loadSaveTasksForm.ShowDialog();
            //}
            loadSaveTasksForm.LoadExamples();
            loadSaveTasksForm.ShowDialog();
        }

        private void SaveStudyExamples(object sender, EventArgs e)
        {
            loadSaveTasksForm.Save = true;
            loadSaveTasksForm.Net = net;           
            //if (loadSaveTasksForm.ConnectToDatabase()) 
            loadSaveTasksForm.ShowDialog();
        }

        private void задатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParameterListForm paramForm = new ParameterListForm(net);
            paramForm.Show();
        }

        //Сохранение
        private void SerializeNet(object sender, EventArgs e)
        {
            BinaryFormatter bf = new BinaryFormatter();
            //SaveFileDialog openDlg = new SaveFileDialog();
            SaveMenu saveMenu = new SaveMenu();
            saveMenu.indexSave = 0;
            saveMenu.ShowDialog();
            if (saveMenu.fileName != null)
            {
                FileStream fd = new FileStream("temp.dat", FileMode.Create, FileAccess.Write);
                net.AccessChangeNet = false;
                bf.Serialize(fd, (NeuronNet)net);
                fd.Close();

                byte[] fileData;
                using (FileStream fs = new FileStream("temp.dat", FileMode.Open))
                {
                    fileData = new byte[fs.Length];
                    fs.Read(fileData, 0, fileData.Length);
                }

                string query = "select max(id) from SaveFiles";
                databaseSQLite.OpenConnection();
                SQLiteCommand myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
                int maxID;
                if (myCommand.ExecuteScalar() == DBNull.Value)
                {
                    maxID = 0;
                }
                else
                {
                    maxID = Convert.ToInt32(myCommand.ExecuteScalar()) + 1;
                }

                query = "INSERT INTO SaveFiles (id,Name,File) values (@id, @Name, @File)";
                myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
                myCommand.Parameters.AddWithValue("@id", maxID);
                myCommand.Parameters.AddWithValue("@Name", saveMenu.fileName);
                myCommand.Parameters.AddWithValue("@File", fileData);
                myCommand.ExecuteNonQuery();
                databaseSQLite.CloseConnection();
            }   
        }

        //Загрузка
        private void DeserializeNet(object sender, EventArgs e)
        {
            BinaryFormatter bf = new BinaryFormatter();
            //OpenFileDialog openDlg = new OpenFileDialog();
            LoadMenu loadMenu = new LoadMenu();
            loadMenu.indexSave = 0;
            loadMenu.ShowDialog();
            if (loadMenu.fileName != null)
            {
                string query = "select File from SaveFiles where Name=" + "\"" + loadMenu.fileName + "\"";
                databaseSQLite.OpenConnection();
                SQLiteCommand myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
                List<SaveFiles> list = new List<SaveFiles>();

                using (SQLiteDataReader reader = myCommand.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            byte[] data = (byte[])reader.GetValue(0);
                            SaveFiles saveFile1 = new SaveFiles(data);
                            list.Add(saveFile1);
                        }
                    }
                }
                databaseSQLite.CloseConnection();
                using (FileStream fd = new FileStream("temp.dat", FileMode.OpenOrCreate))
                {
                    fd.Write(list[0].File, 0, list[0].File.Length);
                }

                FileStream fs = new FileStream("temp.dat", FileMode.Open, FileAccess.Read);
                Net = (NeuronNet)bf.Deserialize(fs);
                net.AccessChangeNet = true;
                fs.Close();
            }
        }

        public void RBFNetType(object sender, EventArgs e)
        {
            loadSaveTasksForm.Net = net = new RBFNeuralNet();
            type = NeuronNetType.RBF;
            this.Net = net;
            Refresh();
        }

        private void rBFToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RBFNetType(this, EventArgs.Empty);

            RegressionForm form = new RegressionForm(RegressionForm.FunctionType.ONE_ARGUMENT_FUNCTION, (RBFNeuralNet)net);
            form.Show();
        }

        private void rBFToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            RBFNetType(this, EventArgs.Empty);

            RegressionForm form = new RegressionForm(RegressionForm.FunctionType.TWO_ARGUMENT_FUNCTION, (RBFNeuralNet)net);
            form.Show();
        }

        public void ShuffleNetType(object sender, EventArgs e)
        {
            loadSaveTasksForm.Net = net = new LinearSystemTask();
            type = NeuronNetType.SHUFFLE;
            this.Net = net;
            Refresh();
        }

        //Импорт файла
        private void ImportFileDat(object sender, EventArgs e)
        {
            BinaryFormatter bf = new BinaryFormatter();
            SaveFileDialog openDlg = new SaveFileDialog();
            openDlg.Filter = "Data Files (.dat)| *.dat|All files (.)| *.";

            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(openDlg.FileName, FileMode.Create, FileAccess.Write);
                net.AccessChangeNet = false;
                bf.Serialize(fs, (NeuronNet)net);
                fs.Close();
            }
        }

        //Экспорт файла
        private void ExportFileDat(object sender, EventArgs e)
        {
            BinaryFormatter bf = new BinaryFormatter();
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "Data Files (.dat)| *.dat|All files (.)| *.";

            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(openDlg.FileName, FileMode.Open, FileAccess.Read);
                Net = (NeuronNet)bf.Deserialize(fs);
                net.AccessChangeNet = true;
                fs.Close(); 
            }
        }

        //
        private void ImportExcel(object sender, EventArgs e)
        {
            //string email = "dimkkov@gmail.com";
            //BinaryFormatter bf = new BinaryFormatter();
            SaveFileDialog openDlg = new SaveFileDialog();
            openDlg.Filter = "CSV Files (.csv)| *.csv|All files (.)| *.";
            //string csvContent = "";
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                net.AccessChangeNet = false;
                File.Delete(@"NodeJS\todos.json");
                //File.Delete(@"NodeJS\todos.csv");
                string json = JsonConvert.SerializeObject(net, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                Console.WriteLine(json);
                File.WriteAllText(@"NodeJS\todos.json", json);

                Process.Start("cmd", @"/C cd " + Application.StartupPath + "/NodeJS & node ./jsontocsv " + openDlg.FileName);
                //File.Delete(@"NodeJS\todos.json");
            }
        }

        private void ExportExcel(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "CSV Files (.csv)| *.csv|All files (.)| *.";
            if (openDlg.ShowDialog() == DialogResult.OK)    
            {
                File.Delete(@"NodeJS\test.json");
                Process.Start("cmd", @"/C cd " + Application.StartupPath + "/NodeJS & node ./csvtojson " + openDlg.FileName);
                Thread.Sleep(3000);
                string json = File.ReadAllText(@"NodeJS\test.json");
                ITraceWriter writer = new MemoryTraceWriter();
                JsonSerializerSettings serSettings = new JsonSerializerSettings();
                serSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                serSettings.TraceWriter = writer;

                //var neuronNet = JsonConvert.DeserializeObject<List<string>>(json.ToString(), serSettings);
                //var neuronNet = JsonConvert.DeserializeObject<List<NeuronNet>>(json.ToString(), serSettings);
                var test = JsonConvert.DeserializeObject<List<Root2>>(json.ToString(), serSettings);
                net = AcceptJson(test[0]);
                net.AccessChangeNet = true;
                //Console.WriteLine(writer);
            }
        }

        private NeuronNet AcceptJson(Root2 root2)
        {
            NeuronNet net = new NeuronNet();
            for (int i = 0; i < root2.inputss.Count; i++)
            {
                PointF pointF = new PointF();
                pointF.X = (float)root2.inputss[i].Position.X;
                pointF.Y = (float)root2.inputss[i].Position.Y;
                NeuronInput neuron = new NeuronInput((float)root2.inputss[i].value, root2.inputss[i].Name, pointF);
                neuron.positionChanged = root2.inputss[i].positionChanged;
                neuron.wasPainted = root2.inputss[i].wasPainted;
                net.inputss.Add(neuron);
            }
            for (int i = 0; i < root2.NeuronGroups.Count; i++)
            {
                NeuronGroup neurongroup = new NeuronGroup();
                neurongroup.Neurons = root2.NeuronGroups[i].Neurons;
                neurongroup.SecondActivate = root2.NeuronGroups[i].SecondActivate;
                neurongroup.SumForSoftMax = (float)root2.NeuronGroups[i].SumForSoftMax;
                neurongroup.allNeuronsWasPainted = root2.NeuronGroups[i].allNeuronsWasPainted;
                net.NeuronGroups.Add(neurongroup);
            }
            for(int i = 0; i < root2.studyPairss.Count; i++)
            {
                StudyPair studyPair = new StudyPair();
                for (int j = 0;j< root2.studyPairss[i].inputs.Count; j++)
                {
                    studyPair.inputs.Add((float)root2.studyPairss[i].inputs[j]);
                }
                for (int j = 0; j < root2.studyPairss[i].quits.Count; j++)
                {
                    studyPair.quits.Add((float)root2.studyPairss[i].quits[j]);
                }
                for (int j = 0; j < root2.studyPairss[i].realQuits.Count; j++)
                {
                    studyPair.realQuits.Add((float)root2.studyPairss[i].realQuits[j]);
                }
                net.studyPairss.Add(studyPair);
            }
            net.E = (float)root2.E;
            net.moment = (float)root2.moment;
            for (int i = 0; i < root2.errors.Count; i++)
            {
                PointF pointF = new PointF();
                pointF.X = (float)root2.errors[i].X;
                pointF.Y = (float)root2.errors[i].Y;
                net.errors.Add(pointF);
            }
            for (int i = 0; i < root2.normalizedErrors.Count; i++)
            {
                PointF pointF = new PointF();
                pointF.X = (float)root2.normalizedErrors[i].X;
                pointF.Y = (float)root2.normalizedErrors[i].Y;
                net.normalizedErrors.Add(pointF);
            }
            net.EraCount = root2.EraCount;
            net.currentSelection = new NeuronGroup(0);
            net.recognitionResults = new List<RecognitionResult>();
            net.StudyPairsLoaded = root2.StudyPairsLoaded;
            net.InputsSum = (float)root2.InputsSum;
            net.allInputsWasPainted = root2.allInputsWasPainted;
            net.minError = (float)root2.minError;
            net.NormalizeOutputValue = (float)root2.NormalizeOutputValue;
            net.biasX = (float[])root2.biasX;
            net.biasY = (float[])root2.biasY;
            net.scaleX = (float[])root2.scaleX;
            net.scaleY = (float[])root2.scaleY;
            net.StudyLimit = (float)root2.StudyLimit;
            net.AccessChangeNet = root2.AccessChangeNet;
            return net;
        }

        private void ImportJson(object sender, EventArgs e)
        {
            SaveFileDialog openDlg = new SaveFileDialog();
            openDlg.Filter = "JSon Files (.json)| *.json|All files (.)| *.";
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                net.AccessChangeNet = false;
                //File.Delete(@"NodeJS\todos.json");
                //File.Delete(@"NodeJS\todos.csv");
                string json = JsonConvert.SerializeObject(net, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                Console.WriteLine(json);
                File.WriteAllText(openDlg.FileName, json);

                //Process.Start("cmd", @"/C cd " + Application.StartupPath + "/NodeJS & node ./jsontocsv " + openDlg.FileName);
                //File.Delete(@"NodeJS\todos.json");
            }
        }

        private void ExportJson(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "JSon Files (.json)| *.json|All files (.)| *.";
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                //File.Delete(@"NodeJS\test.json");
                ///Process.Start("cmd", @"/C cd " + Application.StartupPath + "/NodeJS & node ./csvtojson " + openDlg.FileName);
                //Thread.Sleep(3000);
                string json = File.ReadAllText(openDlg.FileName);
                ITraceWriter writer = new MemoryTraceWriter();
                JsonSerializerSettings serSettings = new JsonSerializerSettings();
                serSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                serSettings.TraceWriter = writer;

                //var neuronNet = JsonConvert.DeserializeObject<List<string>>(json.ToString(), serSettings);
                var test = JsonConvert.DeserializeObject<Root2>(json, serSettings);
                //var test = JsonConvert.DeserializeObject<List<Root2>>(json.ToString(), serSettings);
                //net = test;
                net = AcceptJson(test);
                net.AccessChangeNet = true;
                //Console.WriteLine(writer);
            }
        }

        private void перезапускToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void системыЛинейныхУравненийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShuffleNetType(this, EventArgs.Empty);

            type = NeuronNetType.SHUFFLE;
            LinearSystemTaskForm LSTForm = new LinearSystemTaskForm((LinearSystemTask)net);
            LSTForm.Show();
        }
      
    }

    public class MouseStatus
    {
        public bool LeftButtonDown;
        public bool RightButtonDown;
        public bool ButtonDown;

        public MouseStatus()
        { }

        public void DownEvent(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) LeftButtonDown = true;
            if (e.Button == MouseButtons.Right) RightButtonDown = true;
            ButtonDown = true;
        }

        public void UpEvent(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) LeftButtonDown = false;
            if (e.Button == MouseButtons.Right) RightButtonDown = false;

            if (!LeftButtonDown && !RightButtonDown) ButtonDown = false;
        }
    }

    public class SaveFiles
    {
        public SaveFiles(byte[] file)
        {
            File = file;
        }
        public byte[] File { get; private set; }
    }
}
