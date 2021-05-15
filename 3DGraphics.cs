using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace _DGraphics
{
    public partial class Graphics3D : UserControl
    {
        public class Function3D
        {
            public delegate float GraphicFunction3D(float x , float y);
            public GraphicFunction3D GraphicFunction;
            public Mesh GraphicMesh;
            public CustomVertex.PositionNormalColored[] vertex;
            public short[] indices;
            public bool NeedCreateBuffer = true , ShowGraphic = true;
            public int ID;
            public static Color[] colors = { Color.Green , Color.DarkOrange , Color.Yellow , Color.Violet , Color.Teal , Color.Snow};
            
            public Function3D(int id, GraphicFunction3D func)
            {
                ID = id;
                GraphicFunction = func;
            }

            public void CreateMesh(Device device, int NumberFaces, int NumberVertices)
            {
                if (GraphicMesh == null) 
                    GraphicMesh = new Mesh(NumberFaces, NumberVertices, MeshFlags.Managed, CustomVertex.PositionNormalColored.Format, device);
                GraphicMesh.SetVertexBufferData(vertex, LockFlags.None);
                GraphicMesh.SetIndexBufferData(indices, LockFlags.None);
                GraphicMesh.ComputeNormals();
            }

            public void AllocateMemory(int quadsCount)
            {
                if (vertex == null)
                {
                    vertex = new CustomVertex.PositionNormalColored[4 * quadsCount];
                    for (int i = 0; i < vertex.Length; i++) vertex[i] = new CustomVertex.PositionNormalColored(new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0);
                }
                if (indices == null) indices = new short[6 * quadsCount];               
            }

            public Vector3[] GetPoints(Vector2 xBounds, Vector2 zBounds, int countX, int countY)
            {
                Vector3 [] values = new Vector3[(countX + 1) * (countY + 1)];
                float addX = (xBounds.Y - xBounds.X) / countX ,
                      addY = (zBounds.Y - zBounds.X)/ countY;
                int index = 0;
                float x = xBounds.X , z = zBounds.X;

                for (int i = 0; i < countX + 1; i++ , x += addX, z = zBounds.X)
                {
                    for (int j = 0; j < countY + 1; j++ , z += addY)
                    {
                        values[index++] = new Vector3(x, GraphicFunction(x, z), z);
                    }
                }
                
                return values;
            }
        }

        public class GraphicParameters
        {
            public int StepCount;
            public float FuncMin, FuncMax;
            public float StepX, StepZ;
            public float ShiftX, ShiftZ;
            public float RealXPos, RealZPos, FuncValue;
            public int QuadCountX, QuadCountZ, QuadCount;
            public float TextureIncrementX, TextureIncrementZ;
            public float TexturePositionX, TexturePositionZ;
            public int NumberFaces;
            public int NumberVertices;

            public GraphicParameters(int stepCount)
            {
                StepCount = stepCount;
            }

            public void CalculateParameters(Vector2 xBounds, Vector2 zBounds)
            {
                StepX = (xBounds.Y - xBounds.X) / StepCount;
                StepZ = (zBounds.Y - zBounds.X) / StepCount;
                ShiftX = StepX / 2;

                ShiftZ = StepZ / 2;
                QuadCountX = stepCount;
                QuadCountZ = stepCount;
                QuadCount = QuadCountX * QuadCountZ;
                TextureIncrementX = 2.0f / QuadCountX;
                TextureIncrementZ = 2.0f / QuadCountZ;
                NumberFaces = 2 * QuadCount;
                NumberVertices = 3 * NumberFaces;
                TexturePositionX = 0;
                TexturePositionZ = 2;
            }
        }

        Device device;
        PresentParameters presentParams = new PresentParameters();
        Vector3 cameraPosition, cameraDirection , cameraUpVector;
        PaintEventArgs paintEventArgs;
        short [] indices = new short[3];
        CustomVertex.PositionColored [] vertex = new CustomVertex.PositionColored[3];
        Mesh boundBox;
        const int maxValuesCount = 15;
        Mesh [] xValues = new Mesh[maxValuesCount];
        Mesh [] yValues = new Mesh [maxValuesCount];
        Mesh [] zValues = new Mesh [maxValuesCount];
        bool leftButtonPressed = false;
        Vector3 newRotate = new Vector3(0 , 0 , 0) , oldRotate = new Vector3(Geometry.DegreeToRadian(-90f) , Geometry.DegreeToRadian(90f) , 0);
        Point startPoint = new Point();
        float scale = 1f;
        Vector2 xBounds , zBounds, yBounds;
        Color backColor = Color.DarkBlue;
        Vector2 xRealBound = new Vector2(-6, 6),
                 zRealBound = new Vector2(-6, 6),
                 yRealBound = new Vector2(-6, 6);
        GraphicType type = GraphicType.Graphic;
        float realStepX , realStepZ;
        const int stepCount = 60;
        float boxShift = 0.2f;
        Texture boxTexture;
        Bitmap boxBitmap = new Bitmap(400 , 400);
        Function3D[] functions = new Function3D[50];
        int functionsCount = 0;

        Vector2 quadsCount = new Vector2(50 , 50);
        float[,] diagrammValues;
        Mesh[,] diagramm;
        CustomVertex.PositionColored[] axesPoints = new CustomVertex.PositionColored[6];
        GraphicParameters parameters;

        private float DefaultFunction ( float x , float y )
        {
           return (float)Math.Exp(-x * x / 2 - y * y / 2);
        }

        enum GraphicType
        { 
            Diagramm = 1,
            Graphic = 2
        }

        public Color BackColorValue
        {
            get
            {
                return backColor;
            }

            set
            {
                backColor = value;
                Invalidate();
            }
        }

        public Function3D[] Functions
        {
            get
            {
                return functions;
            }            
        }

        public Vector2 XBounds
        {
            get 
            { 
                return xBounds; 
            }
            set 
            {
                xBounds = value;
                CreateAxesLines();
            }
        }

        public Vector2 ZBounds
        {
            get
            {
                return zBounds;
            }
            set
            {
                zBounds = value;
                CreateAxesLines();
            }
        }

        public void RenewBounds()
        {
            CreateMesh();
            Invalidate();
        }

        public Graphics3D ()
        {
            InitializeComponent();

            SetStyle(ControlStyles.Opaque |  ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint , true);            
            cameraPosition = new Vector3(0 , 0 , -12);
            cameraDirection = new Vector3(0 , 0 , 0);
            cameraUpVector = new Vector3(0 , 1 , 0);
            xBounds = new Vector2(-5 , 5f);
            zBounds = new Vector2(-5 , 5);
            realStepX = ( xRealBound.Y - xRealBound.X ) / stepCount;
            realStepZ = ( zRealBound.Y - zRealBound.X ) / stepCount;
            parameters = new GraphicParameters(stepCount);
            parameters.CalculateParameters(xBounds , zBounds);
            Dock = DockStyle.Fill;
            InitializeGraphics();
        }

        public int FunctionsCount
        {
            get 
            { 
                return functionsCount; 
            }
        }

        public Vector3[] GetGraphicPoints(int index, int countX, int countY)
        {
            return functions[index].GetPoints(xBounds , zBounds, countX , countY);
        }

        public bool IsGraphicExists(int id)
        {
            for (int i = 0; i < functionsCount; i++)
            {
                if (functions[i].ID == id) return true;
            }

            return false;
        }

        public void AddGraphic(Function3D func)
        {
            if (IsGraphicExists(func.ID)) return;
            functions[functionsCount++] = new Function3D(func.ID, func.GraphicFunction);
        }

        public void SetDiagrammValues(float[,] values)
        {
            quadsCount = new Vector2(values.GetLength(0) , values.GetLength(1));
            diagramm = new Mesh[values.GetLength(0), values.GetLength(1)];
            diagrammValues = values;
            type = GraphicType.Diagramm;

            CreateDiagramm();
            Invalidate();
        }

        private void CreateDiagramm()
        {
            SizeF quadSize = new SizeF((xRealBound.Y - xRealBound.X) / quadsCount.X, (zRealBound.Y - zRealBound.X) / quadsCount.Y);
            CustomVertex.PositionColored[] tmpVertex;
            Mesh tmpMesh;

            for(int i = 0; i < quadsCount.X; i++)
            {
                for (int j = 0; j < quadsCount.Y; j++)
                {
                    diagramm[i , j] = Mesh.Box(device, quadSize.Width, diagrammValues[i, j], quadSize.Height);
                    tmpMesh = diagramm[i, j].Clone(diagramm[i, j].Options.Value, CustomVertex.PositionColored.Format, device);
                    tmpVertex = (CustomVertex.PositionColored[])tmpMesh.VertexBuffer.Lock(0, typeof(CustomVertex.PositionColored), LockFlags.None, tmpMesh.NumberVertices);

                    for (int vert = 0; vert < tmpVertex.Length; vert++) tmpVertex[vert].Color = Color.FromArgb((int)(diagrammValues[i, j] * 255), (int)((1.0f - diagrammValues[i, j]) * 255), 0).ToArgb();

                    tmpMesh.VertexBuffer.Unlock();
                    diagramm[i, j].Dispose();
                    diagramm[i, j] = tmpMesh;

                    diagramm[i, j] = diagramm[i, j].Clone(diagramm[i, j].Options.Value, CustomVertex.PositionNormalColored.Format, device);
                    diagramm[i, j].ComputeNormals();
                }
            }
            
        }

        public void CloseGraphics()
        {
            if (diagramm == null) return;

            for (int i = 0; i < diagramm.GetLength(0) ; i++)
            {
                for (int j = 0; j < diagramm.GetLength(1); j++)
                {
                    if (diagramm[i, j] != null) diagramm[i, j].Dispose();
                }
            }
        }

        private void DrawDiagramm()
        { 
            SizeF quadSize = new SizeF((xRealBound.Y - xRealBound.X) / quadsCount.X, (zRealBound.Y - zRealBound.X) / quadsCount.Y);
            float limit = 0.1f;

            for (int i = 0; i < quadsCount.X; i++)
            {
                for (int j = 0; j < quadsCount.Y; j++)
                {
                    if (diagrammValues[i, j] < limit) continue;
                    device.Transform.World = Matrix.Scaling(new Vector3(scale, scale, scale)) * Matrix.RotationY(Geometry.DegreeToRadian(180f)) * Matrix.Translation(new Vector3(quadSize.Width * i + xRealBound.X + boxShift, diagrammValues[i , j] / 2, quadSize.Height * j + zRealBound.X + boxShift));
                    diagramm[i, j].DrawSubset(0);                    
                }
            }
        }

        private void InitializeGraphics ()
        {
            presentParams.BackBufferCount = 1;
            presentParams.BackBufferFormat = Format.R5G6B5;
            presentParams.BackBufferHeight = Height;
            presentParams.BackBufferWidth = Width;
            presentParams.Windowed = true;
            presentParams.SwapEffect = SwapEffect.Discard;
            presentParams.EnableAutoDepthStencil = true;
            presentParams.AutoDepthStencilFormat = DepthFormat.D16;

            device = new Device(0 , DeviceType.Hardware , this , CreateFlags.SoftwareVertexProcessing , presentParams);
            paintEventArgs = new PaintEventArgs(CreateGraphics(), ClientRectangle);
            boundBox = Rectangle(xRealBound.Y - xRealBound.X + 2 * boxShift, zRealBound.Y - zRealBound.X + 2 * boxShift);
            SetCamera();
            
            CreateMesh();
            CreateBoxTexture(10, 10);
            //CreateAxesLines();
            Refresh();                
        }

        public void SetCamera ()
        {
            device.Transform.Projection = Matrix.PerspectiveFovLH(Geometry.DegreeToRadian(90f) , Width / Height , 1 , 100);
            cameraDirection = new Vector3(0 , 0 , 0);
            device.Transform.View = Matrix.LookAtLH(cameraPosition , cameraDirection , cameraUpVector);
            SetLight();            
        }

        public void SetLight ()
        {
            device.RenderState.Lighting = true;
            device.RenderState.CullMode = Cull.None;
            
            device.Lights [0].Type = LightType.Directional;
            device.Lights [0].Direction = new Vector3(0 , 0 , -1);
            device.Lights [0].Position = new Vector3(0 , 5 , -10);
            device.Lights [0].Diffuse = Color.Coral;
            device.Lights [0].Update();
            device.Lights [0].Enabled = true;

            device.Lights [1].Type = LightType.Spot;
            device.Lights [1].Direction = new Vector3(0 , -2 , 5);
            device.Lights [1].Position = new Vector3(0 , 5 , 5);
            device.Lights [1].Diffuse = Color.Gold;
            device.Lights [1].Specular = Color.ForestGreen;
            device.Lights [1].Enabled = true;
            device.Lights [1].Update();

            device.Lights [2].Type = LightType.Directional;
            device.Lights [2].Direction = new Vector3(0 , -2 , 0);
            device.Lights [2].Position = new Vector3(0 , 0 , 5);
            device.Lights [2].Diffuse = Color.White;
            device.Lights [2].Enabled = true;
            device.Lights [2].Update();

            device.Lights [3].Type = LightType.Directional;
            device.Lights [3].Direction = new Vector3(0 , -2 , 0);
            device.Lights [3].Position = new Vector3(-5 , 5 , 5);
            device.Lights [3].Diffuse = Color.White;
            device.Lights [3].Enabled = true;
            device.Lights [3].Update();
        }

        public void RecreateGraphic()
        {
            parameters.CalculateParameters(xBounds, zBounds);
            CreateMesh();
            Refresh();
        }

        private void CreateMesh ()
        {            
            FillBuffers();

            for (int i = 0; i < functionsCount; i++) functions[i].CreateMesh(device, parameters.NumberFaces, parameters.NumberVertices);                        
        }

        private void CreateAxesValues ()
        {
            int curr = 0;
            int valuesCount = 10;
            float incX = (xBounds.Y - xBounds.X) / valuesCount,
                  incZ = (zBounds.Y - zBounds.X) / valuesCount,
                  incY = (yBounds.Y - yBounds.X) / valuesCount;
            
            float x = xBounds.X, z = zBounds.X, y = yBounds.X;

            for (int i = 0; i < valuesCount; i++, x += incX)
            {
                xValues[curr++] = Mesh.TextFromFont(device , new System.Drawing.Font("Times New Roman" , 2) , x.ToString("0.00") , 0.2f , 0.05f);               
            }

            curr = 0;

            for (int i = 0; i < valuesCount; i++, z += incZ)
            {
                zValues [curr++] = Mesh.TextFromFont(device , new System.Drawing.Font("Times New Roman" , 2) , z.ToString("0.00") , 0.2f , 0.05f);
            }

            curr = 0;

            for (int i = 0; i < valuesCount; i++, y += incY)
            {
                yValues [curr++] = Mesh.TextFromFont(device , new System.Drawing.Font("Times New Roman" , 2) , y.ToString("0.00") , 0.2f , 0.05f);
            }
        }

        public void CreateAxesLines()
        {
            Vector3 center = new Vector3(Math.Abs(xBounds.X) - Math.Abs(xBounds.Y), Math.Abs(yBounds.X) - Math.Abs(yBounds.Y), Math.Abs(zBounds.X) - Math.Abs(zBounds.Y));
            axesPoints[0] = new CustomVertex.PositionColored(center.X, center.Y, center.Z, Color.Red.ToArgb());
            axesPoints[1] = new CustomVertex.PositionColored(center.X, center.Y + 100, center.Z, Color.Red.ToArgb());
            axesPoints[2] = new CustomVertex.PositionColored(center.X, center.Y, center.Z, Color.Green.ToArgb());
            axesPoints[3] = new CustomVertex.PositionColored(center.X, center.Y, center.Z + 100, Color.Green.ToArgb());
            axesPoints[4] = new CustomVertex.PositionColored(center.X, center.Y, center.Z, Color.Blue.ToArgb());
            axesPoints[5] = new CustomVertex.PositionColored(center.X + 100, center.Y, center.Z, Color.Blue.ToArgb());  
        }

        private void CreateBoxTexture(int X, int Y)
        {
            Graphics graphics = Graphics.FromImage(boxBitmap);
            graphics.Clear(Color.FromArgb(230 , 230 , 230));
            Pen pen = new Pen(Color.Black , 1);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            float addX = (float)boxBitmap.Width / X , addY = (float)boxBitmap.Height / Y;
            float x = 0 , y = 0;

            for (int i = 0; i < X; i++ , x += addX)
            {
                graphics.DrawLine(pen , x , 0 , x , boxBitmap.Height);
            }

            for (int i = 0; i < Y; i++ , y += addY)
            {
                graphics.DrawLine(pen, 0, y, boxBitmap.Width, y);
            }

            boxBitmap.Save("BoxBitmap.bmp");

            boxTexture = TextureLoader.FromFile(device, "BoxBitmap.bmp");           
        }

        private void DrawBoundBox ()
        {
            device.SetTexture(0 , boxTexture);
            device.RenderState.Lighting = false;
            device.Transform.World = Matrix.Translation(0 , yRealBound.X - boxShift , 0) * Matrix.Scaling(new Vector3(scale , scale , scale)) * Matrix.RotationY(Geometry.DegreeToRadian(180f));
            boundBox.DrawSubset(0);
            device.Transform.World = Matrix.RotationZ(Geometry.DegreeToRadian(90f)) * Matrix.Translation(xRealBound.X - boxShift , 0 , 0) * Matrix.Scaling(new Vector3(scale , scale , scale)) * Matrix.RotationY(Geometry.DegreeToRadian(180f));
            boundBox.DrawSubset(0);
            device.Transform.World = Matrix.RotationX(Geometry.DegreeToRadian(90f)) * Matrix.Translation(0 , 0 , zRealBound.X - boxShift ) * Matrix.Scaling(new Vector3(scale , scale , scale)) * Matrix.RotationY(Geometry.DegreeToRadian(180f));
            boundBox.DrawSubset(0);
            device.RenderState.Lighting = true;
        }

        private void DrawAxesValues ()
        {
            float incX = ( xRealBound.Y - xRealBound.X ) / 10 ,
                  incZ = ( zRealBound.Y - zRealBound.X ) / 10 ,
                  incY = ( yRealBound.Y - yRealBound.X ) / 10;

            int curr = 0;

            for ( float x = xRealBound.X; x < xRealBound.Y; x += incX )
            {                
                device.Transform.World = Matrix.Scaling(new Vector3(scale * 0.4f , scale * 0.4f , scale * 0.4f)) * Matrix.RotationY(Geometry.DegreeToRadian(180f)) * Matrix.Translation(new Vector3(-x + 0.5f , yRealBound.X - 0.5f , -zRealBound.X + 0.5f));
                xValues[curr++].DrawSubset(0);
            }

            curr = 0;

            for ( float z = zRealBound.X; z < zRealBound.Y; z += incZ  )
            {
                device.Transform.World = Matrix.Scaling(new Vector3(scale * 0.4f , scale * 0.4f , scale * 0.4f)) * Matrix.RotationY(Geometry.DegreeToRadian(180f)) * Matrix.Translation(new Vector3(xRealBound.X - 0.5f , yRealBound.X - 0.5f , -z + 0.5f));
                zValues [curr++].DrawSubset(0);
            }

            curr = 0;

            for ( float y = yRealBound.X; y < yRealBound.Y; y += incY )
            {
                device.Transform.World = Matrix.Scaling(new Vector3(scale * 0.4f , scale * 0.4f , scale * 0.4f)) * Matrix.RotationY(Geometry.DegreeToRadian(180f)) * Matrix.Translation(new Vector3(-xRealBound.X + 1.0f , y - 0.5f , -zRealBound.X + 0.5f));
                yValues [curr++].DrawSubset(0);
            }
        }

        private Mesh Rectangle ( float width , float height )
        {
            Mesh outMesh = new Mesh(2, 6, MeshFlags.Managed, CustomVertex.PositionColoredTextured.Format, device);
            CustomVertex.PositionColoredTextured[] points = new CustomVertex.PositionColoredTextured[4];
            short [] curIndices = new short[6];
            int cl = Color.FromArgb(230 , 230 , 230).ToArgb();

            points[0] = new CustomVertex.PositionColoredTextured(new Vector3(-width / 2, 0, -height / 2), cl, 0, 1);
            points[1] = new CustomVertex.PositionColoredTextured(new Vector3(-width / 2, 0, height / 2), cl, 0, 0);
            points[2] = new CustomVertex.PositionColoredTextured(new Vector3(width / 2, 0, height / 2), cl, 1, 0);
            points[3] = new CustomVertex.PositionColoredTextured(new Vector3(width / 2, 0, -height / 2), cl, 1, 1);

            curIndices [0] = 0;
            curIndices [1] = 1;
            curIndices [2] = 2;
            curIndices [3] = 0;
            curIndices [4] = 2;
            curIndices [5] = 3;

            outMesh.SetVertexBufferData(points , LockFlags.None);
            outMesh.SetIndexBufferData(curIndices , LockFlags.None);                      
           
            return outMesh;
        }

        private void DrawAxesLines()
        {
            device.RenderState.Lighting = false;
            device.VertexFormat = CustomVertex.PositionColored.Format;
            device.DrawUserPrimitives(PrimitiveType.LineList, 3, axesPoints);
            device.RenderState.Lighting = true;
        }

        private void DrawFunctions()
        {
            device.SetTexture(0 , null);

            for (int i = 0; i < functionsCount; i++)
            {
                if (functions[i].ShowGraphic) functions[i].GraphicMesh.DrawSubset(0);
            }
        }

        protected override void OnPaint ( PaintEventArgs e )
        {
            device.Clear(ClearFlags.ZBuffer | ClearFlags.Target , backColor , 1 , 0);

            device.BeginScene();
            device.Transform.World = Matrix.Scaling(new Vector3(scale , scale , scale)) * Matrix.RotationY(Geometry.DegreeToRadian(180f));
            DrawFunctions();
            DrawBoundBox();
            device.EndScene();            
            device.Present();
        }        

        public void EnableCreateBuffers()
        {
            for (int i = 0; i < functionsCount; i++)
            {
                functions[i].NeedCreateBuffer = true;
            }
        }

       

        private void FillBuffers ()
        {            
            int CurrentVertex = 0;
            int CurrentIndex = 0;
            parameters.FuncMin = float.MaxValue;
            parameters.FuncMax = float.MinValue;

            for (int k = 0; k < functionsCount; k++, CurrentIndex = CurrentVertex = 0)
            {
                functions[k].AllocateMemory(parameters.QuadCount);
                if (!functions[k].ShowGraphic) continue;

                parameters.RealZPos = zRealBound.Y;

                float zPos = zBounds.Y;
                float xPos = xBounds.X;

                for (int z = 0; z < parameters.QuadCountZ; z++, zPos -= parameters.StepZ, parameters.RealZPos -= realStepZ, parameters.TexturePositionZ -= parameters.TextureIncrementZ)
                {
                    parameters.RealXPos = xRealBound.X;
                    xPos = xBounds.X;

                    for (int x = 0; x < parameters.QuadCountX; x++, xPos += parameters.StepX, parameters.RealXPos += realStepX, CurrentIndex += 6, CurrentVertex += 4, parameters.TexturePositionX += parameters.TextureIncrementX)
                    {
                        parameters.FuncValue = functions[k].GraphicFunction(xPos, zPos);
                        parameters.FuncMax = Math.Max(parameters.FuncMax, parameters.FuncValue);
                        parameters.FuncMin = Math.Min(parameters.FuncMin, parameters.FuncValue);

                        if (!functions[k].NeedCreateBuffer) continue;

                        //left up
                        functions[k].vertex[CurrentVertex].X = parameters.RealXPos;  //;new CustomVertex.PositionNormalColored(new Vector3(parameters.RealXPos, parameters.FuncValue, parameters.RealZPos), new Vector3(), /*new Vector2(TexturePositionX , TexturePositionZ)*/ Function3D.colors[2 * k].ToArgb());
                        functions[k].vertex[CurrentVertex].Y = parameters.FuncValue;
                        functions[k].vertex[CurrentVertex].Z = parameters.RealZPos;
                        functions[k].vertex[CurrentVertex].Color = Function3D.colors[2 * k].ToArgb();

                        //right up
                        functions[k].vertex[CurrentVertex + 1].X = parameters.RealXPos + realStepX; // new CustomVertex.PositionNormalColored(new Vector3(parameters.RealXPos + realStepX, functions[k].GraphicFunction(xPos + parameters.StepX, zPos), parameters.RealZPos), new Vector3(), /*new Vector2(TexturePositionX + TextureIncrementX , TexturePositionZ)*/Function3D.colors[2 * k + 1].ToArgb());
                        functions[k].vertex[CurrentVertex + 1].Y = functions[k].GraphicFunction(xPos + parameters.StepX, zPos);
                        functions[k].vertex[CurrentVertex + 1].Z = parameters.RealZPos;
                        functions[k].vertex[CurrentVertex + 1].Color = Function3D.colors[2 * k + 1].ToArgb();

                        //left down
                        functions[k].vertex[CurrentVertex + 2].X = parameters.RealXPos;// new CustomVertex.PositionNormalColored(new Vector3(parameters.RealXPos, functions[k].GraphicFunction(xPos, zPos - parameters.StepZ), parameters.RealZPos - realStepZ), new Vector3(),/*new Vector2(TexturePositionX , TexturePositionZ - TextureIncrementZ)*/Function3D.colors[2 * k + 1].ToArgb());
                        functions[k].vertex[CurrentVertex + 2].Y = functions[k].GraphicFunction(xPos, zPos - parameters.StepZ);
                        functions[k].vertex[CurrentVertex + 2].Z = parameters.RealZPos - realStepZ;
                        functions[k].vertex[CurrentVertex + 2].Color = Function3D.colors[2 * k + 1].ToArgb();
                       
                        //right down
                        functions[k].vertex[CurrentVertex + 3].X = parameters.RealXPos + realStepX; //new CustomVertex.PositionNormalColored(new Vector3(parameters.RealXPos + realStepX, functions[k].GraphicFunction(xPos + parameters.StepX, zPos - parameters.StepZ), parameters.RealZPos - realStepZ), new Vector3(), /*new Vector2(TexturePositionX + TextureIncrementX , TexturePositionZ - TextureIncrementZ)*/Function3D.colors[2 * k].ToArgb());
                        functions[k].vertex[CurrentVertex + 3].Y = functions[k].GraphicFunction(xPos + parameters.StepX, zPos - parameters.StepZ);
                        functions[k].vertex[CurrentVertex + 3].Z = parameters.RealZPos - realStepZ;
                        functions[k].vertex[CurrentVertex + 3].Color = Function3D.colors[2 * k].ToArgb();

                        functions[k].indices[CurrentIndex] = (short)CurrentVertex;
                        functions[k].indices[CurrentIndex + 1] = (short)(CurrentVertex + 1);
                        functions[k].indices[CurrentIndex + 2] = (short)(CurrentVertex + 3);
                        functions[k].indices[CurrentIndex + 3] = (short)CurrentVertex;
                        functions[k].indices[CurrentIndex + 4] = (short)(CurrentVertex + 3);
                        functions[k].indices[CurrentIndex + 5] = (short)(CurrentVertex + 2);
                    }
                }
            }


            float koeff = (yRealBound.Y - yRealBound.X) / (parameters.FuncMax - parameters.FuncMin);
            
            
            for (int f = 0; f < functionsCount; f++)
            {
                if (functions[f].NeedCreateBuffer)
                {
                    for (int i = 0; i < functions[f].vertex.Length; i++)
                    {
                        functions[f].vertex[i].Y *= koeff;
                        functions[f].vertex[i].Y += yRealBound.X - parameters.FuncMin * koeff;
                    }

                    functions[f].NeedCreateBuffer = false;
                }
            }


            yBounds = new Vector2(parameters.FuncMin, parameters.FuncMax);
            //CreateAxesLines();
           
        }

        private void Graphics3D_MouseDown ( object sender , MouseEventArgs e )
        {
            if ( e.Button == MouseButtons.Left )
            {
                leftButtonPressed = true;
                startPoint = e.Location;
            }
        }

        private void Graphics3D_MouseMove ( object sender , MouseEventArgs e )
        {
            if ( leftButtonPressed )
            {
                newRotate.X = oldRotate.X + ( float )( startPoint.X - e.X ) / 100;
                newRotate.Y = oldRotate.Y + ( float )( startPoint.Y - e.Y ) / 100;               
               
                Coord.RotateNormalCoordinates(ref cameraPosition , newRotate.X , newRotate.Y);
                device.Transform.View = Matrix.LookAtLH(cameraPosition , cameraDirection , cameraUpVector);
                Refresh();
            }
        }

        private void Graphics3D_MouseUp ( object sender , MouseEventArgs e )
        {
            if ( e.Button == MouseButtons.Left )
            {
                leftButtonPressed = false;
                oldRotate = newRotate;
            }
        }

        void Graphics3D_MouseWheel ( object sender , System.Windows.Forms.MouseEventArgs e )
        {
            if ( e.Delta > 0 )
            {
                scale += 0.05f;
            }
            else
            {
                scale -= 0.05f;
            }
            
            Invalidate();
        }

        public override void Refresh()
        {
            OnPaint(paintEventArgs);
        }

        private void RecreateMeshes()
        {
            for (int i = 0; i < functionsCount; i++) functions[i].GraphicMesh = null;
        }

        private void Graphics3D_Resize ( object sender , EventArgs e )
        {
            EnableCreateBuffers();
            RecreateMeshes();
            InitializeGraphics();
            if (diagrammValues != null) CreateDiagramm();
            Refresh();
        }

        

        
    }
}
