using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Neuron
{
    public partial class StudyPairModifier : Form
    {
        List<StudyPair> studyPairs;
        NeuronNet neuronNet;

        public StudyPairModifier(List<StudyPair> pairs, NeuronNet net)
        {
            InitializeComponent();
            studyPairs = pairs;
            neuronNet = net;
            
            ShowStudyPairs();
        }

        private void ShowStudyPairs()
        {
            studyPairsBox.Text = "";
            studyPairsCount.Value = studyPairs.Count;

            for (int i = 0; i < studyPairs.Count; i++)
            {
                studyPairsBox.Text += studyPairs[i].ToString() + Environment.NewLine;
            }
        }

        private void studyPairsCount_ValueChanged(object sender, EventArgs e)
        {
                       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            studyPairs.Clear();

            for (int i = 0; i < studyPairsBox.Lines.Length; i++)
            {
                if (studyPairsBox.Lines[i] == "") continue;

                studyPairs.Add(StudyPair.FromString(studyPairsBox.Lines[i]));
            }

            studyPairsCount.Value = studyPairs.Count;
            neuronNet.OutputsCount = studyPairs[0].quits.Count;
            neuronNet.InputsCount = studyPairs[0].inputs.Count;
            neuronNet.StudyPairsLoaded = true;
        }

        private void activeStudyPair_ValueChanged(object sender, EventArgs e)
        {
            //ShowStudyPairs((int)activeStudyPair.Value);
        }

        private void LoadPairs(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            string[] strings;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                strings = File.ReadAllLines(dlg.FileName);
                studyPairs.Clear();

                for (int i = 0; i < strings.Length; i++)
                    studyPairs.Add(StudyPair.FromString(strings[i]));

                studyPairsCount.Value = studyPairs.Count;
                neuronNet.OutputsCount = studyPairs[0].quits.Count;
                neuronNet.InputsCount = studyPairs[0].inputs.Count;
               // DialogResult = DialogResult.OK;
                neuronNet.StudyPairsLoaded = true;
                ShowStudyPairs();
            }
        }
    }
}
