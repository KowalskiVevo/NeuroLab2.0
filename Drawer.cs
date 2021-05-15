using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;

namespace Neuron
{
    public enum typeView
    {
        Line,
        Rectangle
    }
    public class Drawer : UserControl
    {
        public class DrawerFunction
        {
            public Function GraphicFunction;
            public List<PointF> RealPoints = new List<PointF>();
            public List<PointF> ScreenPoints = new List<PointF>();
            public delegate float Function(float x);
            public delegate PointF Translator(PointF pt);
            public string Name;
            public Color GraphicColor;
            public Brush GraphicBrush;
            public static int GraphicsCount = 0;
            public GraphicInfo GraphicInfo;
            private int pointsCount = 100;
            public bool singlePoints = false;
            public bool NeedCreateBuffer = true , ShowGraphic = true;
            public int ID;
            Color[] colors = {Color.Red,Color.DarkBlue,Color.Green, Color.Brown, Color.Navy};
            Brush[] brushes = { Brushes.Red, Brushes.DarkBlue, Brushes.Green, Brushes.Brown, Brushes.Navy };
            public typeView type=typeView.Line;          

            public int PointsCount
            {
                get 
                {
                    return RealPoints.Count;
                }
                set
                {
                    pointsCount = value;
                }
            }

            public DrawerFunction(int id, Function func , String name,typeView type)
            {
                ID = id;
                GraphicFunction = func;
                Name = name;
                this.type = type;
                //GraphicColor = colors[GraphicsCount % colors.Length];
                GraphicColor = colors[id % colors.Length];
                //GraphicBrush = brushes[GraphicsCount++ % brushes.Length];
                GraphicBrush = brushes[id % brushes.Length];
                GraphicInfo = new GraphicInfo(Name , GraphicBrush);
            }

            public DrawerFunction(int id, List<PointF> points, string name,typeView type)
            {
                ID = id;
                RealPoints = points;
                Name = name;
                this.type = type;
                GraphicColor = colors[GraphicsCount % colors.Length];
                GraphicBrush = brushes[GraphicsCount++ % brushes.Length];
                GraphicInfo = new GraphicInfo(Name, GraphicBrush);
                singlePoints = true;
            }

            public void ToScreen(Translator pointToScreen)
            {
                ScreenPoints.Clear();

                for (int i = 0; i < RealPoints.Count; i++)
                {
                    ScreenPoints.Add(pointToScreen(RealPoints[i]));    
                }
            }

            public void CreateBuffer(PointF bounds)
            {
                if (singlePoints || !NeedCreateBuffer) return;

                float addition = (bounds.Y - bounds.X) / pointsCount , x = bounds.X;
                RealPoints.Clear();

                for (int i = 0; i < pointsCount + 1; i++ , x += addition)
                {
                    RealPoints.Add(new PointF(x , GraphicFunction(x)));
                }

                NeedCreateBuffer = false;
            }
        }

        Graphics memGraphics , tempGraphics , axesGraphics;
        Bitmap memBitmap , tempBitmap , axesBitmap;
        Brush backColorBrush;
        const int maxGraphicCount = 10;
        public bool redrawFlag = false;
        float offsetX = 40 , offsetY = 40;
        float X0 , Y0 , SX = 0 , SY = 0 , MX = 1 , MY = 1;
        float maxFunc , minFunc;
        public PointF graphicBounds;
        bool rightButtonDown = false , leftButtonDown = false;
        public bool needCreateBuffer = true;
        bool useTempGraphics = false;
        System.Drawing.Point rightButtonFirstPoint , rightButtonLastPoint , leftButtonFirstPoint , leftButtonLastPoint;
        PointF[] graphicBoundsStack = new PointF [20];
        PointF tempGraphicBounds;
        int stackValue = 0;
        float offset = 0;        
        public List<DrawerFunction> functions = new List<DrawerFunction>();
        PaintEventArgs paintArgs;
        bool needRedrawAxes = true;
        public bool enableShifting = true;
        public bool enableZoom = true;
        public bool Interpolate = false;

             
        public delegate void OnDrawerClick(PointF pt);
        public event OnDrawerClick ClickEvent = null;

        public class GraphicInfo
        {
            public string name;
            public Brush color;

            public GraphicInfo(string Name , Brush brush)
            {
                name = Name;
                color = brush;
            }
        }

        public void Clear()
        {
            functions.Clear();
            needCreateBuffer = true;            
        }

        private bool IsGraphicExists(int id)
        {
            for (int i = 0; i < functions.Count; i++)
            {
                if (functions[i].ID == id) return true;
            }

            return false;
        }

        public void AddGraphic(int id, DrawerFunction.Function F , string name,typeView type)
        {
            if (IsGraphicExists(id)) return;
            functions.Add(new DrawerFunction(id , F , name,type));
            needRedrawAxes = true;
        }

        public void AddSinglePoints(PointF[] points , string name)
        {
            
        }

        public void AddGraphic(int id, List<PointF> points , string name)
        {
            if (IsGraphicExists(id)) return;
            functions.Add(new DrawerFunction(id , points , name,typeView.Line));
            needRedrawAxes = true;
        }

        public List<DrawerFunction> Functions
        {
            get 
            {
                return functions;
            }
        }

        public PointF GraphicBounds
        {
            get
            {
                return graphicBounds;
            }
            set
            {
                graphicBoundsStack[stackValue = 0] = graphicBounds = value;
                EnableCreatingBuffer();
                needRedrawAxes = true;
            }
        }

        public PointF[] GetPoints(int index)
        {
            return functions[index].RealPoints.ToArray();
        }

        public RectangleF[] GetRectangle(int index)
        {
            int count=functions[index].ScreenPoints.Count;
            Size size=new Size(3,3);
            RectangleF[] rect=new RectangleF[functions[index].ScreenPoints.Count];
            for(int i=0;i<count;i++)
            {
                rect[i].Location=functions[index].ScreenPoints[i];
                rect[i].Size=size;
            }
            return rect;
        }

        public Drawer (Control parent , System.Drawing.Rectangle position)
        {           
            BorderStyle = BorderStyle.FixedSingle;
            Location = position.Location;
            Size = position.Size;
            Parent = parent;            
            SetStyle ( ControlStyles.Opaque | ControlStyles.AllPaintingInWmPaint , true );
            graphicBoundsStack[0] = new PointF ( -5 , 5 );
            Paint += new PaintEventHandler ( Drawer_Paint );
            MouseDown += new MouseEventHandler ( Drawer_MouseDown );
            MouseUp += new MouseEventHandler ( Drawer_MouseUp );
            MouseMove += new MouseEventHandler ( Drawer_MouseMove );
            MouseEnter += new EventHandler(Drawer_Mouseinput);
            MouseLeave += new EventHandler(Drawer_MouseLeave);
            Resize += new EventHandler(Drawer_Resize);
            CreateBitmapBuffer();
            graphicBounds = new PointF(-5, 5);               
        }

        private void CreateBitmapBuffer()
        {
            backColorBrush = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.Green, Color.LightBlue, LinearGradientMode.Vertical);
            memBitmap = new Bitmap(Width, Height);
            axesBitmap = new Bitmap(Width , Height);
            X0 = Width / 2;
            Y0 = Height / 2;
            memGraphics = Graphics.FromImage(memBitmap);
            axesGraphics = Graphics.FromImage(axesBitmap);
            memGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            paintArgs = new PaintEventArgs(CreateGraphics(), ClientRectangle);
            needRedrawAxes = true;
            Redraw();
        }

        void Drawer_Resize(object sender, EventArgs e)
        {
            CreateBitmapBuffer();
        }

        void Drawer_MouseLeave(object sender, EventArgs e)
        {
            //tempBitmap = (Bitmap)memBitmap.Clone();
            //tempGraphics = Graphics.FromImage(tempBitmap);
            //Redraw();
        }

        void Drawer_Mouseinput(object sender, EventArgs e)
        {
          
        }
        
        void Drawer_MouseMove ( object sender , MouseEventArgs e )
        {
            if ( enableZoom && rightButtonDown )
            {                
                rightButtonLastPoint = e.Location;
                tempBitmap = (Bitmap)memBitmap.Clone();
                tempGraphics = Graphics.FromImage(tempBitmap);
                tempGraphics.DrawRectangle ( Pens.Brown , rightButtonFirstPoint.X , rightButtonFirstPoint.Y , rightButtonLastPoint.X - rightButtonFirstPoint.X , rightButtonLastPoint.Y - rightButtonFirstPoint.Y );
                useTempGraphics = true;
                Drawer_Paint(this, paintArgs);                
            }

            if (enableShifting && leftButtonDown)
            {
                EnableCreatingBuffer();
                needRedrawAxes = true;
                offset = ScreenToPoint(e.Location).X - ScreenToPoint(leftButtonFirstPoint).X;
                CreateBuffer(new PointF(tempGraphicBounds.X - offset, tempGraphicBounds.Y - offset));
                Redraw();
            }         
            
        }

        private void SetClipRectangle(bool flag)
        {
            if (flag) memGraphics.Clip = new Region(new RectangleF(offsetX, offsetY, Width - 2 * offsetX, Height - 2 * offsetY));
            else memGraphics.Clip = new Region(ClientRectangle);
        }

        public void EnableCreatingBuffer()
        {
            for (int i = 0; i < functions.Count; i++) functions[i].NeedCreateBuffer = true;
            needRedrawAxes = true;
        }

        void Drawer_MouseDown ( object sender , MouseEventArgs e )
        {
            if (ClickEvent != null && e.Button == MouseButtons.Left) ClickEvent(ScreenToPoint(e.Location));

            if ( enableZoom && e.Button == MouseButtons.Right )
            {                
                rightButtonDown = true;
                rightButtonFirstPoint = e.Location;
                useTempGraphics = true;
            }

            if ( enableShifting && e.Button == MouseButtons.Left )
            {                            
                leftButtonDown = true;
                leftButtonFirstPoint = e.Location;
                tempGraphicBounds = graphicBounds;               
            }
        }

        void Drawer_MouseUp ( object sender , MouseEventArgs e )
        {
            if ( enableZoom && e.Button == MouseButtons.Right )
            {
                rightButtonDown = false;
                useTempGraphics = false;

                if (rightButtonFirstPoint.X > rightButtonLastPoint.X)
                {
                    if (stackValue-- == 0)
                        stackValue = 0;

                    CreateBuffer(new PointF(graphicBoundsStack[stackValue].X, graphicBoundsStack[stackValue].Y));
                }
                else
                {
                    graphicBoundsStack[stackValue++] = graphicBounds;
                    int iterationsCount = 25;
                    float offsetLeft = ScreenToPoint(rightButtonFirstPoint).X - graphicBounds.X;
                    float offsetRight = graphicBounds.Y - ScreenToPoint(rightButtonLastPoint).X;
                    offsetLeft /= iterationsCount;
                    offsetRight /= iterationsCount;
                    PointF newBounds = new PointF(graphicBounds.X , graphicBounds.Y);

                    for (int i = 0; i < iterationsCount; i++)
                    {
                        newBounds.X += offsetLeft;
                        newBounds.Y -= offsetRight;
                        CreateBuffer( newBounds );
                        Redraw();
                    }
                }

                needRedrawAxes = true;
                Redraw ();                
            }

            if ( enableShifting && e.Button == MouseButtons.Left )
            {
                leftButtonDown = false;
                leftButtonLastPoint = e.Location;               
            }
        }

        void CreateBuffer(PointF bounds)
        {      
            graphicBounds = new PointF(bounds.X, bounds.Y);
            maxFunc = float.MinValue;
            minFunc = float.MaxValue;

            for (int i = 0; i < functions.Count; i++)
            {
                if (!functions[i].ShowGraphic) continue;

                functions[i].CreateBuffer(bounds);

                for (int j = 0; j < functions[i].PointsCount; j++)
                {
                    maxFunc = Math.Max(maxFunc, functions[i].RealPoints[j].Y);
                    minFunc = Math.Min(minFunc, functions[i].RealPoints[j].Y);
                }               
            }

            if (maxFunc == minFunc) maxFunc += 1; 
            MX = (Width - 2 * offsetX) / (graphicBounds.Y - graphicBounds.X);
            MY = (Height - 2 * offsetY) / (maxFunc - minFunc);
            SY = Height - offsetY - Y0 + minFunc * MY;
            SX = Width - offsetX - X0 + graphicBounds.X * MX;
        }

        PointF PointToScreen ( PointF point )
        {
            return new PointF (X0 + MX * point.X - SX , Y0 - MY * point.Y + SY );
        }

        PointF ScreenToPoint ( System.Drawing.Point screen )
        {
            return new PointF ( ( screen.X - X0 + SX ) / MX , - ( screen.Y - Y0 - SY ) / MY );
        }

        void DrawGraphic()
        {
            try
            {
                SetClipRectangle(false);
                memGraphics.DrawImage(axesBitmap, new Point(0, 0));
                SetClipRectangle(true);

                for (int k = 0; k < functions.Count; k++)
                {
                    if (!functions[k].ShowGraphic) continue;

                    functions[k].ToScreen(PointToScreen);///????????????????
                    if(functions[k].type==typeView.Line)
                        memGraphics.DrawLines(new Pen(functions[k].GraphicColor, 2), functions[k].ScreenPoints.ToArray());
                    else if(functions[k].type==typeView.Rectangle)
                        memGraphics.DrawRectangles(new Pen(functions[k].GraphicColor, 2), GetRectangle(k));
                }
            }
            catch (Exception e)
            { }
        }

        void DrawAxes ( System.Drawing.Point partitionCount )
        {
            if (!needRedrawAxes) return;

            needRedrawAxes = false;            
            axesGraphics.Clear(Color.WhiteSmoke);
            float xAddition = ( Width - 2 * offsetX ) / partitionCount.X;
            float yAddition = ( Height - 2 * offsetY ) / partitionCount.Y;
            StringFormat strFormat = new StringFormat ();
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;

            Pen axePen = new Pen(Color.Black , 1 );
            axePen.DashStyle = DashStyle.DashDotDot;
            float xReal = graphicBounds.X , xRealAddition = ( graphicBounds.Y - graphicBounds.X ) / partitionCount.X;
            float yReal = maxFunc , yRealAddition = ( maxFunc - minFunc ) / partitionCount.Y;
            float x = offsetX, y = offsetY;

            for ( int i = 0 ; i < partitionCount.X + 1; i++, x += xAddition , xReal += xRealAddition )
            {
                axesGraphics.DrawLine(axePen, x, offsetY, x, Height - offsetY);
                axesGraphics.DrawString(xReal.ToString("0.00"), new Font("Arial", 8), Brushes.Black, new PointF(x, Height - offsetY + 20), strFormat);
            }

            for ( int i = 0 ; i < partitionCount.Y + 1; i++ , y += yAddition , yReal -= yRealAddition )
            {
                axesGraphics.DrawLine(axePen, new PointF(offsetX, y), new PointF(Width - offsetX, y));
                axesGraphics.DrawString(yReal.ToString("0.00"), new Font("Arial", 8), Brushes.Black, new PointF(offsetX - 20, y), strFormat);
            }

            axesGraphics.DrawRectangle(new Pen(Color.Black, 2), offsetX, offsetY, Width - 2 * offsetX, Height - 2 * offsetY);

            DrawGraphicInfo();           
        }

        private void DrawGraphicInfo()
        {
            StringFormat strFormat = new StringFormat();
            strFormat.LineAlignment = StringAlignment.Center;
            SizeF size;
            float offset = 0;

            for (int i = 0; i < functions.Count; i++)
            {
                size = axesGraphics.MeasureString(functions[i].GraphicInfo.name, new Font("Times New Roman", 10));
                axesGraphics.DrawString(functions[i].GraphicInfo.name, new Font("Times New Roman", 10), Brushes.Black, new PointF(offsetX + offset + 20, offsetY / 2), strFormat);
                axesGraphics.FillRectangle(functions[i].GraphicInfo.color, new RectangleF(offsetX + offset, offsetY / 2 - size.Height / 2, size.Height, size.Height));
                offset += size.Width + 30;
            }
        }

        public override void Refresh()
        {
            Drawer_Paint(this, paintArgs);  
        }

        public void Redraw ()
        {
            CreateBuffer(graphicBounds);           

            DrawAxes ( new Point ( 10 , 10 ) );
            DrawGraphic ();
            Drawer_Paint(this, paintArgs);           
        }
       

        void Drawer_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(useTempGraphics ? tempBitmap : memBitmap, new Point(0, 0));            
        }
    }
}
