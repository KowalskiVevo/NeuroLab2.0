using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Neuron
{
    public partial class LinearSystemTaskForm : Form
    {
        int EqvNum = 0;
        int VarNum = 0;
        int MaxEpoch = 5000;
        float MaxError = 0.0005f;

        LinearSystemTask LSTNet;
        //NeuronNet net;
/*
        public LinearSystemTaskForm()
        {
            InitializeComponent();
            InitTables(2, 2);
        }
*/
        public LinearSystemTaskForm(LinearSystemTask net)
        {
            InitializeComponent();
            InitTables(2, 2);

            LSTNet = net;
        }

        private void InitTables(int EN, int VN)
        {
            int i = 0, j = 0;
            string name = "";

            DataGridMain.Rows.Clear();
            DataGridMain.RowsDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;

            DataGridMain.Columns.Clear();  
            DataGridMain.Columns.Add("No", "№");
            DataGridMain.Columns[0].Width = 50;

            VarNum = VN;
            for (i = 0; i < VN; i++)
            {
                name = "X" + (i + 1).ToString();
                DataGridMain.Columns.Add(name, name);
                DataGridMain.Columns[i + 1].Width = 50;
            }

            DataGridMain.Columns.Add("Equal", "");
            DataGridMain.Columns[DataGridMain.ColumnCount - 1].Width = 40;
            DataGridMain.Columns.Add("B", "B");
            DataGridMain.Columns[DataGridMain.ColumnCount - 1].Width = 40;

            EqvNum = EN;
            for (i = 0; i < EN; i++)
            {
                DataGridMain.Rows.Add();
                DataGridMain.Rows[i].Cells[0].Value = (i + 1).ToString();
                DataGridMain.Rows[i].Cells[VarNum + 1].Value = "=";
            }

            InitIOTable(EN, VN);

            CheckTables();
        }

        private void InitIOTable(int EN, int VN)
        {
            int i = 0;
            string name = "";

            DataGridInput.Rows.Clear();
            DataGridInput.RowsDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;

            DataGridInput.Columns.Clear();
            DataGridInput.Columns.Add("Type", "Тип");
            DataGridInput.Columns[0].Width = 50;

            VarNum = VN;
            for (i = 0; i < VN; i++)
            {
                name = "X" + (i + 1).ToString();
                DataGridInput.Columns.Add(name, name);
                DataGridInput.Columns[i + 1].Width = 50;
            }
            DataGridInput.Rows.Add(); DataGridInput.Rows[0].Cells[0].Value = "Вход";
            DataGridInput.Rows.Add(); DataGridInput.Rows[1].Cells[0].Value = "Выход";
            CheckTables();
        }

        private void CheckTables()
        {
            int i = 0, j = 0;
            for (i = 0; i < EqvNum; i++)
                for (j = 1; j < VarNum + 3; j++)
                {
                    if (DataGridMain.Rows[i].Cells[j].Value == null)
                            DataGridMain.Rows[i].Cells[j].Value = 1.0f.ToString();
                }

            for (j = 1; j < VarNum + 1; j++)
            {
                if (DataGridInput.Rows[0].Cells[j].Value == null)
                    DataGridInput.Rows[0].Cells[j].Value = 1.0f.ToString();
            }
        }

        private void Solver()
        {
            Matrix A = new Matrix(EqvNum, VarNum);
            Matrix B = new Matrix(1, EqvNum);
            Matrix X = new Matrix(1, VarNum);

            int i = 0, j = 0;
            for (i = 0; i < EqvNum; i++)
            {
                for (j = 0; j < VarNum; j++)
                {
                    A[i, j] = System.Convert.ToSingle(DataGridMain.Rows[i].Cells[j + 1].Value.ToString());
                }
                B[0, i] = System.Convert.ToSingle(DataGridMain.Rows[i].Cells[VarNum + 2].Value.ToString());
            }

            for (i = 0; i < VarNum; i++)
            {
                X[0, i] = System.Convert.ToSingle(DataGridInput.Rows[0].Cells[i + 1].Value.ToString());
            }

            SolveTask(A, B, X);
        }

        private void SolveTask(Matrix A, Matrix B, Matrix X)
        {
            LinearSystemGraphics lstgraphic = new LinearSystemGraphics();
            LSTNet.InitTask(A, B);
            LSTNet.GraphicsNeuron.Refresh();

            LSTNet.InitInputs(X);
            LSTNet.epochNum = 1;

            List<PointF> errors = new List<PointF>();
            List<PointF>[] states = new List<PointF>[VarNum];

            int i = 0;
            for (i = 0; i < VarNum; i++)
                states[i] = new List<PointF>();

            int N = 0;
            float ERR = 1.0f;

            InitIOTable(EqvNum, VarNum);
            while (ERR > (float)Math.Pow(MaxError, 2.0))
            {
                N++;
                ERR = LSTNet.Calculate();
                DataGridInput.Rows.Add();
                DataGridInput.Rows[N + 1].Cells[0].Value = N.ToString();

                for (i = 0; i < VarNum; i++)
                {
                    DataGridInput.Rows[N + 1].Cells[i + 1].Value = LSTNet.GetOutput()[0, i];
                    states[i].Add(new PointF(N, LSTNet.GetOutput()[0, i]));
                }
                errors.Add(new PointF(N, ERR));

                if (N > MaxEpoch)
                {
                    System.Windows.Forms.MessageBox.Show("Превышено максимальное количество эпох");
                    break;
                }
            }

            for (i = 0; i < VarNum; i++)
                DataGridInput.Rows[1].Cells[i + 1].Value = LSTNet.GetOutput()[0, i];

            textBoxFactEpoch.Text = LSTNet.GetEpoches().ToString();
            textBoxFactError.Text = ((float)Math.Sqrt(ERR)).ToString();

            lstgraphic.Clear();
            lstgraphic.CreateErrorGraphic(errors);
            lstgraphic.SetGraphicBounds(new PointF(1, N));
            for (i = 0; i < VarNum; i++)
                lstgraphic.AddGraphic(states[i], "X" + (i + 1).ToString());

            lstgraphic.Show();
        }

        private void построитьСетьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Matrix A = new Matrix(EqvNum, VarNum);
            Matrix B = new Matrix(1, EqvNum);
            Matrix X = new Matrix(1, VarNum);

            int i = 0, j = 0;
            for (i = 0; i < EqvNum; i++)
            {
                for (j = 0; j < VarNum; j++)
                {
                    A[i, j] = System.Convert.ToSingle(DataGridMain.Rows[i].Cells[j + 1].Value.ToString());
                }
                B[0, i] = System.Convert.ToSingle(DataGridMain.Rows[i].Cells[VarNum + 2].Value.ToString());
            }

            for (i = 0; i < VarNum; i++)
            {
                X[0, i] = System.Convert.ToSingle(DataGridInput.Rows[0].Cells[i + 1].Value.ToString());
            }

            LSTNet.InitTask(A, B);
            LSTNet.GraphicsNeuron.Refresh();
        }

        private void начатьРешениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Solver();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Solver();
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            DataGridMain.Rows[EqvNum].Cells[0].Value = (EqvNum + 1).ToString();
            DataGridMain.Rows[EqvNum].Cells[VarNum + 1].Value = "=";
            EqvNum++;
            textBoxEqvNum.Text = EqvNum.ToString();

            CheckTables();
        }
        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            EqvNum--;
            for (int i = 0; i < EqvNum; i++)
                DataGridMain.Rows[i].Cells[0].Value = (i + 1).ToString();

            textBoxEqvNum.Text = EqvNum.ToString();
            CheckTables();
        }

        private void textBoxVarNum_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBoxVarNum.Text.Length != 0)
                {
                    VarNum = int.Parse(textBoxVarNum.Text);
                    InitTables(EqvNum, VarNum);
                }
            }
            catch (System.FormatException exc)
            {
                System.Windows.Forms.MessageBox.Show(exc.Message, "Ошибка в введенных данных");
            }
        }
        private void textBoxEqvNum_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBoxEqvNum.Text.Length != 0)
                {
                    EqvNum = int.Parse(textBoxEqvNum.Text);
                    InitTables(EqvNum, VarNum);
                }
            }
            catch (System.FormatException exc)
            {
                System.Windows.Forms.MessageBox.Show(exc.Message, "Ошибка в введенных данных");
            }
        }
        private void textBoxMaxError_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBoxMaxError.Text.Length != 0)
                {
                    MaxError = float.Parse(textBoxMaxError.Text);
                }
            }
            catch (System.FormatException exc)
            {
                System.Windows.Forms.MessageBox.Show(exc.Message, "Ошибка в введенных данных");
            }
        }
        private void textBoxMaxEpoch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBoxMaxEpoch.Text.Length != 0)
                {
                    MaxEpoch = int.Parse(textBoxMaxEpoch.Text);
                }
            }
            catch (System.FormatException exc)
            {
                System.Windows.Forms.MessageBox.Show(exc.Message,"Ошибка в введенных данных");
            }
        }

        private void сохранитьЗадачуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog openFileDialog = new SaveFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            StreamWriter SW = new StreamWriter(openFileDialog.OpenFile());
            SW.WriteLine(EqvNum.ToString());
            SW.WriteLine(VarNum.ToString());

            String data;
            int i = 0, j = 0;
            for (i = 0; i < DataGridMain.ColumnCount; i++)
            {
                for (j = 0; j < DataGridMain.RowCount - 1; j++)
                {
                    data = DataGridMain[i, j].Value.ToString();
                    SW.WriteLine(data);
                }
            }
            SW.Flush();
            SW.Close();
        }

        private void загрузитьЗадачуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            StreamReader SR = new StreamReader(openFileDialog.OpenFile());

            textBoxEqvNum.Text = SR.ReadLine();;
            textBoxVarNum.Text = SR.ReadLine();

            int i = 0, j = 0;
            for (i = 0; i < DataGridMain.ColumnCount; i++)
            {
                for (j = 0; j < DataGridMain.RowCount - 1; j++)
                {
                    DataGridMain[i, j].Value = SR.ReadLine();
                }
            }

            DataGridMain.Refresh();
            SR.Close();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
