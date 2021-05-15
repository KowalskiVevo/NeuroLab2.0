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
    public partial class ParameterListForm : Form
    {
        NeuronNet net;

        public ParameterListForm(NeuronNet net)
        {
            InitializeComponent();
            this.net = net;
            FillParameterGrid();
        }

        public void FillParameterGrid()
        {
            for (int i = 0; i < net.InputsCount; i++)
            {
                ParametrGrid.Rows.Add(new object[]{i + 1 , net.Inputs[i].Name, net.Inputs[i].value});
            }
            
            for (int i = 0; i < net.OutputsCount; i++)
            {
                OutputGridView.Rows.Add(new Object[]{i + 1, net.LastNeuronGroup.Neurons[i].OUT});
            }
        }

        private void ParametrGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (e.RowIndex >= net.Inputs.Count)
                {
                    net.InputsCount++;
                    net.GraphicsNeuron.Refresh();
                }
                if (e.ColumnIndex == 1) net.Inputs[e.RowIndex].Name = (string)ParametrGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                else net.Inputs[e.RowIndex].value = float.Parse(ParametrGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }
        }

        private void ParameterListForm_Load(object sender, EventArgs e)
        {

        }

        private StudyPair CurrentToStudyPair()
        {
            StudyPair pair = new StudyPair();

            for (int i = 0; i < net.InputsCount; i++)
            {
                pair.inputs.Add(float.Parse(ParametrGrid.Rows[i].Cells[2].Value.ToString()));
            }

            for (int i = 0; i < net.OutputsCount; i++)
            {
                pair.quits.Add(float.Parse(OutputGridView.Rows[i].Cells[1].Value.ToString()));
            }

            return pair;
        }

        private void AddStudyPair(object sender, EventArgs e)
        {
            net.StudyPairs.Add(CurrentToStudyPair());
        }

        private void посчитатьВыходСетиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            net.Calculate();

            for (int i = 0; i < net.OutputsCount; i++)
            {
                OutputGridView.Rows[i].Cells[1].Value = net.LastNeuronGroup.Neurons[i].OUT;
            }
        }

        private void OutputGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (e.RowIndex >= net.OutputsCount)
            {
                net.OutputsCount++;
                net.GraphicsNeuron.Refresh();
            }
        }
    }
}
