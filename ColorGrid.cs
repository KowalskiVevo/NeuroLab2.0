using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ColorGrid
{
    public partial class ColorGrid : UserControl
    {
        GridItem[,] data;
        SizeF gridItemSize = new SizeF();
        Size gridSize;
        Graphics memGraphics;
        Bitmap memBitmap;
        PaintEventArgs paintArgs;
        MouseStatus mStatus = new MouseStatus();
        Pen pen = new Pen(Color.Black, 2);
        LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0,0,30,30) , Color.DarkBlue , Color.Yellow , 40);

        public ColorGrid(int width , int height)
        {
            InitializeComponent();
            InitializeGrid(width , height);
            Resize += new EventHandler(ColorGrid_Resize);  
        }

        void ColorGrid_Resize(object sender, EventArgs e)
        {
            RedrawGrid();
            RedrawData();
        }

        public GridItem[,] Data
        {
            get { return data; }
        }

        public Size GridSize
        {
            get { return gridSize; }

            set 
            {
                gridSize = value;
                InitializeGrid(gridSize.Width, gridSize.Height);
                Refresh();
            }
        }

       

        public void Increase()
        {          
            InitializeGrid(gridSize.Width + 1, gridSize.Height + 1);
            Refresh();
        }

        public void Decrease()
        {
            InitializeGrid(gridSize.Width - 1, gridSize.Height - 1);
            Refresh();
        }

        public void InitializeGrid(int width, int height)
        {
            gridSize = new Size(width < 2 ? 2 : width, height < 2 ? 2 : height);
            data = new GridItem[gridSize.Height, gridSize.Width];

            for (int i = 0; i < gridSize.Width; i++)
                for (int j = 0; j < gridSize.Height; j++)
                    data[j, i] = new GridItem();

            RedrawGrid();             
        }

        public Bitmap GetBitmap()
        {
            return memBitmap;
        }

        private void RedrawGrid()
        {
            memBitmap = new Bitmap(Width, Height);

            memGraphics = Graphics.FromImage(memBitmap);
            memGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            memGraphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            memGraphics.FillRectangle(Brushes.White, ClientRectangle);
            brush = new LinearGradientBrush(GetItemRectangleByIndex(new Point(0 , 0)) , Color.Yellow , Color.DarkBlue , 40);
            DrawGrid();
            paintArgs = new PaintEventArgs(CreateGraphics(), ClientRectangle); 
        }
       
        public void SetData(List<float> input)
        {
            int width = (int)Math.Sqrt(input.Count);
            int height = width;
            int index = 0;

            InitializeGrid(width , height);

            for (int i = 0; i < gridSize.Width; i++)
                for (int j = 0; j < gridSize.Height; j++)
                    data[j, i].value = input[index++] > 0 ? true : false;

            RedrawData();
        }

        
        /*public string DataToString(float quit)
        {
            string str = "";

            for (int i = 0; i < gridSize.Width; i++)
                for (int j = 0; j < gridSize.Height; j++)
                    str += data[j, i].value ? "1|" : "0|";


            str = str.Remove(str.Length - 1) + string.Format(";{0};", quit.ToString("0.00")) + Environment.NewLine;
            
            return str;
        }*/

        public void RedrawData()
        {
            for (int i = 0; i < gridSize.Width; i++)
                for (int j = 0; j < gridSize.Height; j++)
                {
                    brush = new LinearGradientBrush(GetItemRectangleByIndex(new Point(j, i)), Color.Yellow, Color.DarkBlue, 40);
                    memGraphics.FillEllipse(data[j, i].value ? brush : Brushes.White, GetItemRectangleByIndex(new Point(j, i)));
                }
            Refresh();
        }

        public void DrawGrid()
        {
            memGraphics.Clear(Color.White);

            gridItemSize = new SizeF((float)Width / gridSize.Width, (float)Height / gridSize.Height);

            for (int i = 0; i < gridSize.Width; i++)
                memGraphics.DrawLine(pen, new PointF(i * gridItemSize.Width , 0), new PointF(i * gridItemSize.Width , Height));
            
            for (int j = 0; j < gridSize.Height; j++)
                memGraphics.DrawLine(pen, new PointF(0 , j * gridItemSize.Height), new PointF(Width , j * gridItemSize.Height));
                    
        }

        private RectangleF GetItemRectangleByIndex(Point index)
        {            
            gridItemSize = new SizeF((float)Width / gridSize.Width, (float)Height / gridSize.Height);
            RectangleF rect = new RectangleF( gridItemSize.Width * index.X + 2 , gridItemSize.Height * index.Y + 2, gridItemSize.Width - 4, gridItemSize.Height - 4 );
            
            return rect;
        }

        private RectangleF GetItemRectangleByMousePosition(PointF mousePosition)
        {
            gridItemSize = new SizeF((float)Width / gridSize.Width, (float)Height / gridSize.Height);

            return GetItemRectangleByIndex(new Point((int)(mousePosition.X / gridItemSize.Width), (int)(mousePosition.Y / gridItemSize.Height)));
        }

        private Point GetIndexByMousePosition(PointF mousePosition)
        {
            gridItemSize = new SizeF((float)Width / gridSize.Width, (float)Height / gridSize.Height);

            return new Point((int)(mousePosition.X / gridItemSize.Width), (int)(mousePosition.Y / gridItemSize.Height));
        }

        protected override void OnPaint(PaintEventArgs e)
        {           
            e.Graphics.DrawImage(memBitmap, new Point());
        }

        public override void Refresh()
        {
            OnPaint(paintArgs);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            mStatus.DownEvent(e);

            Point pt = GetIndexByMousePosition(new PointF(e.X, e.Y));
                        
            if (mStatus.LeftButtonDown) data[pt.X, pt.Y].value = true;
            else if(mStatus.RightButtonDown) data[pt.X, pt.Y].value = false;

           
            brush = new LinearGradientBrush(GetItemRectangleByIndex(new Point(pt.X, pt.Y)), Color.Yellow, Color.DarkBlue, 40);
            memGraphics.FillEllipse(data[pt.X, pt.Y].value ? brush : Brushes.White, GetItemRectangleByMousePosition(new PointF(e.X, e.Y)));
            Refresh();            
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
           /* Point pt = GetIndexByMousePosition(new PointF(e.X, e.Y));

            if (mStatus.LeftButtonDown) data[pt.X, pt.Y].value = true;
            else data[pt.X, pt.Y].value = false;


            if (mStatus.ButtonDown)
            {
                memGraphics.FillEllipse(data[pt.X, pt.Y].value ? Brushes.Yellow : Brushes.White, GetItemRectangleByMousePosition(new PointF(e.X, e.Y)));
                Refresh();
            }*/
            
        }

        

        protected override void OnMouseUp(MouseEventArgs e)
        {
            mStatus.UpEvent(e);
        }
    }

    public class GridItem
    {
        public bool value;

        public float FloatValueClassification
        {
            get 
            { 
                if (value) return 1.0f;
                return 0.0f;
            }
            set 
            {
                if (value > 0) this.value = true;
                else this.value = false;
            }
        }

        public float FloatValueAssociationMemory
        {
            get
            {
                if (value) return 1.0f;
                return -1.0f;
            }
            set
            {
                if (value > 0) this.value = true;
                else this.value = false;
            }
        }

        public GridItem()
        {
            value = false;
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
}
