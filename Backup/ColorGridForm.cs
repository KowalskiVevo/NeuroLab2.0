using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ColorGrid;
using GemBox.Spreadsheet;

namespace Neuron
{
    public partial class ColorGridForm : Form
    {
        public ColorGrid.ColorGrid colorGrid;
        public List<StudyPair> studyPairs = new List<StudyPair>();
        protected int currentStudyPair = 0;
        protected bool pairsLoaded = false;
        public bool IsClassification = true;
        LinearNeuronNet net;

        public ColorGridForm(LinearNeuronNet _net)
        {
            InitializeComponent();
            net = _net;
            BuildExistingStudyPairs();
            colorGrid = new ColorGrid.ColorGrid(8,8);
            colorGrid.Dock = DockStyle.Fill;
            colorGrid.Parent = Container.Panel1;
            colorGrid.DrawGrid();
            colorGrid.Refresh();
        }

        public ColorGridForm()
        {
            InitializeComponent();
            colorGrid = new ColorGrid.ColorGrid(8, 8);
            colorGrid.Dock = DockStyle.Fill;
            colorGrid.Parent = Container.Panel1;
            colorGrid.DrawGrid();
            colorGrid.Refresh();
        }

        protected virtual void BuildExistingStudyPairs()
        {
            if (!net.StudyPairsLoaded) return;

            colorGrid.GridSize = new Size((int)Math.Sqrt(net.InputsCount), (int)Math.Sqrt(net.InputsCount));
            studyPairs = net.StudyPairs;
            pairsLoaded = true;
            CreatePreview();
        }

        protected void CreatePreview()
        {
            Bitmap currentBitmap = new Bitmap(picturePreview.Width * studyPairs.Count , picturePreview.Height);
            Graphics graphics = Graphics.FromImage(currentBitmap);
            
            picturePreview.Width = picturePreview.Height * studyPairs.Count;

            for (int i = 0; i < studyPairs.Count; i++)
            {
                colorGrid.SetData(studyPairs[i].inputs);
                graphics.DrawImage(colorGrid.GetBitmap() , new Rectangle(i * picturePreview.Height , 0 , picturePreview.Height , picturePreview.Height));
            }

            picturePreview.Image = currentBitmap;
        }

        protected virtual void ChangeNetSettings()
        {
            net.LastNeuronGroup.SetNeuronCount(studyPairs[currentStudyPair].quits.Count);
            net.InputsCount = studyPairs[currentStudyPair].inputs.Count;
            if(!net.StudyPairsLoaded) net.StudyPairs = studyPairs;
            pairsLoaded = true;
            net.GraphicsNeuron.netOptions.InputsCount = net.InputsCount;
        }

        private void LoadPairs(object sender, EventArgs e)
        {           
            string[] file;
            OpenFileDialog dlg = new OpenFileDialog();
            
            studyPairs.Clear();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                file = File.ReadAllLines(dlg.FileName , Encoding.Default);

                for (int i = 0; i < file.Length; i++)
                    studyPairs.Add(StudyPair.FromString(file[i]));

                colorGrid.SetData(studyPairs[currentStudyPair].inputs);
                ChangeNetSettings();
                CreatePreview();
            }
        }
        
        private void NextPicture(object sender, EventArgs e)
        {
            if (pairsLoaded)
            {
                currentStudyPair = ++currentStudyPair % studyPairs.Count;
                colorGrid.SetData(studyPairs[currentStudyPair].inputs);
            }
        }

        private void PrevPicture(object sender, EventArgs e)
        {
            if (pairsLoaded)
            {
                currentStudyPair = Math.Abs(--currentStudyPair % studyPairs.Count);
                colorGrid.SetData(studyPairs[currentStudyPair].inputs);
            }
        }

        protected virtual void RecognizePicture(object sender, EventArgs e)
        {
            if (pairsLoaded)
            {
                StudyPair pair = net.Recognize(colorGrid.Data);
                string resultString = "";

                for (int i = 0; i < net.recognitionResults.Count; i++)
                {
                    resultString += string.Format("Образ : {0} Вероятность : {1}%", net.recognitionResults[i].pair.name, net.recognitionResults[i].result) + Environment.NewLine;
                }

                MessageBox.Show(resultString);
            }
        }

        protected virtual void Study(object sender, EventArgs e)
        {
            if (pairsLoaded)
            {
                net.Study();
                net.GraphicsNeuron.Refresh();
            }
        }

        private void Increase(object sender, EventArgs e)
        {
            colorGrid.Increase();
        }

        private void Decrease(object sender, EventArgs e)
        {
            colorGrid.Decrease();
        }

        private void ExcelExport(object sender, EventArgs e)
        {
            ExcelFile exFile = new ExcelFile();
            ExcelWorksheet sheet = exFile.Worksheets.Add("Picture");
            SaveFileDialog saveFileDlg = new SaveFileDialog();

            if (saveFileDlg.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < colorGrid.GridSize.Height; i++)
                {
                    for (int j = 0; j < colorGrid.GridSize.Width; j++)
                    {
                        sheet.Cells[i, j].Value = colorGrid.Data[j, i].value ? "X" : "-";
                    }
                }

                exFile.SaveXls(saveFileDlg.FileName);
                System.Diagnostics.Process.Start(saveFileDlg.FileName);
            }
        }

        private void picturePreview_MouseDown(object sender, MouseEventArgs e)
        {
            currentStudyPair = e.X / picturePreview.Height;
            colorGrid.SetData(studyPairs[currentStudyPair].inputs);
        }

        protected virtual void Save(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();            
           // string[] s;
                        
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //s = File.ReadAllLines(dlg.FileName);                               
                File.WriteAllText(dlg.FileName, net.StudyPairsToString(studyPairs.Count));
            }
            
        }

        private void AddPicture(object sender, EventArgs e)
        {
            StudyPair pair = new StudyPair();
            ObjectNameForm form = new ObjectNameForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                pair.name = form.NewName;

                for (int i = 0; i < colorGrid.Data.GetLength(0); i++)
                {
                    for (int j = 0; j < colorGrid.Data.GetLength(1); j++)
                    {
                        if(IsClassification) pair.inputs.Add(colorGrid.Data[j, i].FloatValueClassification);
                        else pair.inputs.Add(colorGrid.Data[j, i].FloatValueAssociationMemory);
                    }
                }

                studyPairs.Add(pair);

                for (int i = 0; i < studyPairs.Count; i++)
                {
                    studyPairs[i].quits.Clear();

                    for (int j = 0; j < studyPairs.Count; j++)
                    {
                        studyPairs[i].quits.Add(i == j ? 1 : 0);
                    }
                }
                                
                CreatePreview();
            }
        }

        private void BuildNet(object sender, EventArgs e)
        {
            ChangeNetSettings();
        }

        private void DeleteAll(object sender, EventArgs e)
        {
            studyPairs.Clear();
            picturePreview.Image = picturePreview.InitialImage;
        }

        private void ChangeImage(object sender, EventArgs e)
        {
            if (currentStudyPair >= studyPairs.Count) return;
            float value;

            for (int i = 0; i < colorGrid.Data.GetLength(0); i++)
            {
                for (int j = 0; j < colorGrid.Data.GetLength(1); j++)
                {
                    if (IsClassification) value = colorGrid.Data[j, i].FloatValueClassification;
                    else value = colorGrid.Data[j, i].FloatValueAssociationMemory;
                    studyPairs[currentStudyPair].inputs[i * colorGrid.Data.GetLength(0) + j] = value;
                }
            }

            CreatePreview();
        }

        private void импортироватьИзБазыДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuildExistingStudyPairs();
        }
    }
}
