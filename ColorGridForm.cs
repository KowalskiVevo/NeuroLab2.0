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
using System.Data.SQLite;
using Database;

namespace Neuron
{
    public partial class ColorGridForm : Form
    {
        public ColorGrid.ColorGrid colorGrid;
        public List<StudyPair> studyPairs = new List<StudyPair>();
        protected int currentStudyPair = 0;
        protected bool pairsLoaded = false;
        public bool IsClassification = true;
        Database databaseSQLite = new Database();
        LinearNeuronNet net;
        public NeuronGraphics neuronGraphics;

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
            if (!net.StudyPairsLoaded) net.StudyPairs = studyPairs;
            pairsLoaded = true;
            net.GraphicsNeuron.netOptions.InputsCount = net.InputsCount;
            
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
            SaveFileDialog saveFileDlg = new SaveFileDialog();
            saveFileDlg.Filter = "Excel Files (.xls)| *.xls|All files (.)| *.";
            if (saveFileDlg.ShowDialog() == DialogResult.OK && studyPairs.Count != 0)
            {
                for (int i = 0; i < studyPairs.Count; i++)
                {
                    ExcelWorksheet sheet = exFile.Worksheets.Add("Picture" + (i+1));
                    int ii = 0;
                    int jj = 0;
                    for (int j = 0; j < studyPairs[i].inputs.Count; j++)
                    {
                        if (jj>= colorGrid.GridSize.Width)
                        {
                            ii++;
                            jj = 0;
                        }
                        if (studyPairs[i].inputs[j] == 1)
                        {
                            sheet.Cells[ii, jj].Value = "X";
                        }
                        else
                        {
                            sheet.Cells[ii, jj].Value = "-";
                        }
                        jj++;
                    }
                }
                    //for (int i = 0; i < colorGrid.GridSize.Height; i++)
                    //{
                    //    for (int j = 0; j < colorGrid.GridSize.Width; j++)
                    //    {
                    //        sheet.Cells[i, j].Value = colorGrid.Data[j, i].value ? "X" : "-";
                    //    }
                    //}

                exFile.SaveXls(saveFileDlg.FileName);
                //System.Diagnostics.Process.Start(saveFileDlg.FileName);
            }
            else
            {
                MessageBox.Show("Нету образов", "ExcelExport", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExcelImport(object sender, EventArgs e)
        {
            ExcelFile exFile = new ExcelFile();
            OpenFileDialog loadFileDlg = new OpenFileDialog();
            loadFileDlg.Filter = "Excel Files (.xls)| *.xls|All files (.)| *.";
            
            if (loadFileDlg.ShowDialog() == DialogResult.OK)
            {
                studyPairs.Clear();
                try
                {
                    exFile.LoadXls(loadFileDlg.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show("Нету файла", "ExcelImport", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                for (int i = 0; i < exFile.Worksheets.Count; i++)
                {
                    ExcelWorksheet sheet = exFile.Worksheets[i];
                    StudyPair pair = new StudyPair();
                    int ii = 0;
                    int jj = 0;
                    for (int j = 0; j < colorGrid.GridSize.Width * colorGrid.GridSize.Height; j++) 
                    {
                        if (jj >= colorGrid.GridSize.Width)
                        {
                            ii++;
                            jj = 0;
                        }
                        if (sheet.Cells[ii, jj].Value.ToString() == "X")
                        {
                            pair.inputs.Add(1);
                        }
                        else
                        {
                            pair.inputs.Add(0);
                        }
                        jj++;
                    }
                    studyPairs.Add(pair);
                }
                colorGrid.SetData(studyPairs[currentStudyPair].inputs);
                ChangeNetSettings();
                CreatePreview();
            }
            else
            {
                MessageBox.Show("Нету образов", "ExcelImport", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void LoadPairs(object sender, EventArgs e)
        {
            string[] file;
            OpenFileDialog dlg = new OpenFileDialog();

            studyPairs.Clear();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                file = File.ReadAllLines(dlg.FileName, Encoding.Default);

                for (int i = 0; i < file.Length; i++)
                    studyPairs.Add(StudyPair.FromString(file[i]));

                colorGrid.SetData(studyPairs[currentStudyPair].inputs);
                ChangeNetSettings();
                CreatePreview();
            }
        }

        private void picturePreview_MouseDown(object sender, MouseEventArgs e)
        {
            currentStudyPair = e.X / picturePreview.Height;
            colorGrid.SetData(studyPairs[currentStudyPair].inputs);
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

        private void DataBaseImport(object sender, EventArgs e)
        {
            //SaveFileDialog openDlg = new SaveFileDialog();
            SaveMenu saveMenu = new SaveMenu();
            saveMenu.indexSave = 1;
            saveMenu.ShowDialog();
            //saveMenu.ShowDialog();
            if (saveMenu.fileName != null)
            {
                if (studyPairs.Count == 0)
                    MessageBox.Show("Пустой файл", "DataBaseImport", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    ChangeNetSettings();
                    File.WriteAllText("temp.txt", net.StudyPairsToString(studyPairs.Count));

                    byte[] fileData;
                    using (FileStream fs = new FileStream("temp.txt", FileMode.Open))
                    {
                        fileData = new byte[fs.Length];
                        fs.Read(fileData, 0, fileData.Length);
                    }

                    string query = "select max(id) from SaveGraphs";
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

                    query = "INSERT INTO SaveGraphs (id,Name,Graph) values (@id, @Name, @Graph)";
                    myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
                    myCommand.Parameters.AddWithValue("@id", maxID);
                    myCommand.Parameters.AddWithValue("@Name", saveMenu.fileName);
                    myCommand.Parameters.AddWithValue("@Graph", fileData);
                    myCommand.ExecuteNonQuery();
                    databaseSQLite.CloseConnection();

                    neuronGraphics.RecognizeImages(this, EventArgs.Empty);
                    this.Close();
                }
            }
        }

        private void DataBaseExport(object sender, EventArgs e)
        {
            LoadMenu loadMenu = new LoadMenu();
            loadMenu.indexSave = 1;
            loadMenu.ShowDialog();

            string[] file;
            if (loadMenu.fileName != null)
            {
                string query = "select Graph from SaveGraphs where Name=" + "\"" + loadMenu.fileName + "\"";
                databaseSQLite.OpenConnection();
                SQLiteCommand myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
                using (SQLiteDataReader reader = myCommand.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            string data = (string)reader.GetValue(0);
                            File.WriteAllText("temp.txt", data);
                        }
                    }
                }
                databaseSQLite.CloseConnection();
                studyPairs.Clear();
                FileStream fs = new FileStream("temp.txt", FileMode.Open, FileAccess.Read);
                file = File.ReadAllLines("temp.txt", Encoding.Default);
                for (int i = 0; i < file.Length; i++)
                    studyPairs.Add(StudyPair.FromString(file[i]));
                colorGrid.SetData(studyPairs[currentStudyPair].inputs);
                ChangeNetSettings();
                CreatePreview();
                fs.Close();
            }
        }
    }
}
