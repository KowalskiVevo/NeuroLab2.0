using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Neuron
{
    public partial class ClassificationGraphics : UserControl
    {
        public class ObjectClass
        {
            public int id; 
            public List<PointF> values = new List<PointF>();
            public List<int> ids = new List<int>();
            public Brush brush = Brushes.Red;
            public static float size = 6;
            public static Brush[] brushes = { Brushes.Red , Brushes.Yellow , Brushes.Green , Brushes.Blue , Brushes.DarkGoldenrod , Brushes.Maroon , Brushes.DarkOrange , Brushes.DarkBlue , Brushes.Cornsilk , Brushes.Coral , Brushes.Black , Brushes.Azure};

            public static int currentID = 0;
            public static int singleValueID = 1;

            public ObjectClass()
            {
                id = currentID++;
                brush = brushes[id];
            }

            public void SetNewValueID()
            {
                ids.Add(singleValueID++);
            }
        }

        enum NetType
        { 
            LINEAR,
            KOHONEN
        }

        Graphics grfx, mainGraphics;
        Bitmap bitmap;
        PointF shiftLR = new PointF(60 , 30) , shiftTB = new PointF(30, 30);
        Point gridLinesCount = new Point(10, 10);
        PointF xBounds = new PointF(0, 2), yBounds = new PointF(0, 2);
        Pen gridPen = new Pen(Color.Black , 1);
        StringFormat strFormat = new StringFormat();
        Font gridFont = new Font("Times New Roman" , 10);
        Brush currentClassBrush = Brushes.Brown;
        List<ObjectClass> classes = new List<ObjectClass>();
        ObjectClass currentClass = new ObjectClass();
        LinearNeuronNet linearNet;
        KohonenNeuronNet kohonenNet;
        NetType type;
        ObjectClass centerClasses = new ObjectClass();
        
        public ClassificationGraphics(LinearNeuronNet net)
        {
            InitializeComponent();

            ReloadBitmap();
            Paint += new PaintEventHandler(ClassificationGraphics_Paint);            
            Resize += new EventHandler(ClassificationGraphics_Resize);
            MouseDown += new MouseEventHandler(ClassificationGraphics_MouseDown);

            gridPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;

            mainGraphics = CreateGraphics();

            this.linearNet = net;
            type = NetType.LINEAR;
        }

        public ClassificationGraphics(KohonenNeuronNet net)
        {
            InitializeComponent();

            ReloadBitmap();
            Paint += new PaintEventHandler(ClassificationGraphics_Paint);
            Resize += new EventHandler(ClassificationGraphics_Resize);
            MouseDown += new MouseEventHandler(ClassificationGraphics_MouseDown);

            gridPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;

            mainGraphics = CreateGraphics();

            this.kohonenNet = net;
            type = NetType.KOHONEN;
        }

        private PointF ValueToScreen(PointF value)
        {
            return new PointF(shiftLR.X + value.X * ((float)(Width - shiftLR.X - shiftLR.Y) / (xBounds.Y - xBounds.X) ), shiftTB.X + (yBounds.Y - value.Y) * ((float)(Height - shiftTB.X - shiftTB.Y) / (yBounds.Y - yBounds.X)));
        }

        private PointF ScreenToValue(PointF screen)
        {
            return new PointF((screen.X - shiftLR.X) / ((float)(Width - shiftLR.X - shiftLR.Y) / (xBounds.Y - xBounds.X)),  yBounds.Y - (screen.Y - shiftTB.X) / ((float)(Height - shiftTB.X - shiftTB.Y) / (yBounds.Y - yBounds.X)));
        }

        private bool IsInWorkArea(PointF pt)
        {
            return pt.X > xBounds.X && pt.X < xBounds.Y && pt.Y > yBounds.X && pt.Y < yBounds.Y;
        }

        void ClassificationGraphics_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (IsInWorkArea(ScreenToValue(new PointF(e.X, e.Y))))
                {
                    currentClass.SetNewValueID();
                    currentClass.values.Add(ScreenToValue(new PointF(e.X, e.Y)));
                    if (IsClassify.Checked) NetClassify();
                    else
                    {
                        DrawObject(new PointF(e.X, e.Y) , null);
                        Refresh();
                    }
                }
            }
        }

        private void ReloadBitmap()
        {
            bitmap = new Bitmap(Width, Height);
            grfx = Graphics.FromImage(bitmap);
            mainGraphics = CreateGraphics();
            DrawGraphics();
            DrawObjects();
        }

        void ClassificationGraphics_Resize(object sender, EventArgs e)
        {
            ReloadBitmap();
            Refresh();
        }      

        void ClassificationGraphics_Paint(object sender, PaintEventArgs e)
        {
            mainGraphics.DrawImage(bitmap, new Point(0, 0));            
        }

        private void DrawGraphics()
        {
            PointF addition = new PointF((xBounds.Y - xBounds.X) / gridLinesCount.X, (yBounds.Y - yBounds.X) / gridLinesCount.Y);
            PointF realAddition = new PointF((float)(Width - shiftLR.X - shiftLR.Y) / gridLinesCount.X , (float)(Height - shiftTB.X - shiftTB.Y) / gridLinesCount.Y);
            PointF gridValues = new PointF(xBounds.X , yBounds.Y);
            grfx.Clear(Color.FromArgb(220 , 220, 220));

            grfx.FillRectangle(Brushes.White, shiftLR.X, shiftTB.X, Width - shiftLR.Y - shiftLR.X, Height - shiftTB.Y - shiftTB.X);
            grfx.DrawRectangle(new Pen(Color.Black , 2), shiftLR.X, shiftTB.X, Width - shiftLR.Y - shiftLR.X, Height - shiftTB.Y - shiftTB.X);

            for (int i = 0; i < gridLinesCount.X + 1; i++)
            {
                grfx.DrawLine(gridPen, shiftLR.X + i * realAddition.X, shiftTB.X, shiftLR.X + i * realAddition.X, Height - shiftTB.Y);
                grfx.DrawString(gridValues.X.ToString("0.00"), gridFont, Brushes.Black, shiftLR.X + i * realAddition.X , Height - shiftTB.Y / 2 , strFormat);
                gridValues.X += addition.X;
            }

            for (int i = 0; i < gridLinesCount.Y + 1; i++)
            {
                grfx.DrawLine(gridPen, shiftLR.X, shiftTB.X + i * realAddition.Y, Width - shiftLR.Y, shiftTB.X + i * realAddition.Y);
                grfx.DrawString(gridValues.Y.ToString("0.00"), gridFont, Brushes.Black, shiftLR.X / 2 , shiftTB.Y + i * realAddition.Y, strFormat);
                gridValues.Y -= addition.Y;
            }
        }

        private void DrawObject(PointF pt , Brush cl)
        {
            if (cl == null) cl = currentClassBrush;
            grfx.FillEllipse(cl, pt.X - ObjectClass.size, pt.Y - ObjectClass.size, 2 * ObjectClass.size, 2 * ObjectClass.size);
            grfx.DrawString(currentClass.ids.Last().ToString(), Font, Brushes.Black, pt.X + 5, pt.Y + 5);
            
        }

        private void DrawObjects()
        {
            PointF screenPoint;

            for (int i = 0; i < classes.Count; i++)
            {
                for (int j = 0; j < classes[i].values.Count; j++)
                {
                    screenPoint = ValueToScreen(classes[i].values[j]);
                    grfx.FillEllipse(classes[i].brush, screenPoint.X - ObjectClass.size, screenPoint.Y - ObjectClass.size, 2 * ObjectClass.size, 2 * ObjectClass.size);
                    grfx.DrawString(classes[i].ids[j].ToString(), Font, Brushes.Black, screenPoint.X + 5, screenPoint.Y + 5);
                }
            }

            for (int j = 0; j < currentClass.values.Count; j++)
            {
                screenPoint = ValueToScreen(currentClass.values[j]);
                grfx.FillEllipse(currentClassBrush, screenPoint.X - ObjectClass.size, screenPoint.Y - ObjectClass.size, 2 * ObjectClass.size, 2 * ObjectClass.size);
                grfx.DrawString(currentClass.ids[j].ToString() , Font , Brushes.Black , screenPoint.X + 5 , screenPoint.Y + 5);
            }

            for (int i = 0; i < centerClasses.values.Count; i++)
            {
                screenPoint = ValueToScreen(centerClasses.values[i]);
                grfx.DrawEllipse(new Pen(Color.Black , 2), screenPoint.X - ObjectClass.size - 2, screenPoint.Y - ObjectClass.size - 2, 2 * ObjectClass.size + 2, 2 * ObjectClass.size + 2);
            }
        }

        public override void Refresh()
        {
            mainGraphics.DrawImage(bitmap, new Point(0, 0)); 
        }

        private void AddingClass(object sender, EventArgs e)
        {
            if (currentClass.values.Count == 0 || type == NetType.KOHONEN) return;

            classes.Add(currentClass);
            currentClass = new ObjectClass();
            DrawGraphics();
            DrawObjects();
            Refresh();
        }

        private int GetObjectsCount()
        {
            int res = 0;

            for (int i = 0; i < classes.Count; i++) res += classes[i].values.Count;            

            return res;
        }

        private void UpdateCenterClasses()
        {
            centerClasses.values.Clear();

            for (int i = 0; i < kohonenNet.LastNeuronGroup.Neurons.Count; i++)
            {
                centerClasses.values.Add(new PointF(kohonenNet.LastNeuronGroup.Neurons[i].sinapses[0].value, kohonenNet.LastNeuronGroup.Neurons[i].sinapses[1].value));
            }

            DrawGraphics();
            DrawObjects();
            Refresh(); 
        }

        private void BuildNet(object sender, EventArgs e)
        {
            if (type == NetType.LINEAR && classes.Count == 0) return;
            if (type == NetType.KOHONEN && currentClass.values.Count == 0) return;

            switch (type)
            {
                case NetType.LINEAR:
                    linearNet.InputsCount = 2;
                    linearNet.SetNeuronsCount(linearNet.NeuronGroups.Count - 1, classes.Count);
                    linearNet.GraphicsNeuron.Refresh();

                    int count = GetObjectsCount();
                    linearNet.StudyPairs.Clear();

                    StudyPair pair;

                    for (int i = 0; i < classes.Count; i++)
                    {
                        for (int j = 0; j < classes[i].values.Count; j++)
                        {
                            pair = new StudyPair();
                            pair.inputs.Add(classes[i].values[j].X);
                            pair.inputs.Add(classes[i].values[j].Y);

                            for (int k = 0; k < classes.Count; k++)
                            {
                                pair.quits.Add(i == k ? 1 : 0);
                            }

                            linearNet.StudyPairs.Add(pair);
                        }
                    }

                    linearNet.StudyPairsLoaded = true;
                    break;
                
                case NetType.KOHONEN:
                    kohonenNet.InputsCount = 2;
                    kohonenNet.StudyPairs.Clear();
                    kohonenNet.GraphicsNeuron.Refresh();

                    foreach (PointF obj in currentClass.values)
                    {
                        pair = new StudyPair();

                        pair.inputs.Add(obj.X);
                        pair.inputs.Add(obj.Y);

                        kohonenNet.StudyPairs.Add(pair);
                    }

                    UpdateCenterClasses();
                    break;
            }
        }

        private void Study(object sender, EventArgs e)
        {
            switch (type)
            {
                case NetType.LINEAR:
                    linearNet.LinearNormalizeInputs();
                    linearNet.Study();
                    break;

                case NetType.KOHONEN:

                    int count = kohonenNet.EraCount / 50;
                    //kohonenNet.NormalizeInputs();
                    
                    for (int i = 0; i < 50; i++)
                    {
                        kohonenNet.StudyByParts(count , i * count);                        
                        UpdateCenterClasses();
                    }

                    kohonenNet.SaveRealQuits();
                    break;
            }
        }

        private void NetClassify()
        {
            if (type == NetType.KOHONEN) return;
            
            linearNet.Inputs[0].value = (currentClass.values[0].X-linearNet.biasX[0])/linearNet.scaleX[0];
            linearNet.Inputs[1].value = (currentClass.values[0].Y-linearNet.biasX[1])/linearNet.scaleX[0];

            linearNet.Calculate();

            int maxID = linearNet.GetNeuronIDMaxOUT();

            classes[maxID].values.Add(currentClass.values[0]);
            DrawObject(ValueToScreen(currentClass.values[0]), classes[maxID].brush);
            currentClass.values.Clear();            
            Refresh();            
        }

        private void DeleteAll(object sender, EventArgs e)
        {
            ObjectClass.currentID = 0;
            ObjectClass.singleValueID = 1;
            currentClass = new ObjectClass();
            centerClasses.values.Clear();
            classes.Clear();
            IsClassify.Checked = false;
            DrawGraphics();
            DrawObjects();
            Refresh();   
        }
    }
}
