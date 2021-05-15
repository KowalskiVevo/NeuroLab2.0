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

namespace Neuron
{
    public partial class AssociationMemoryForm : ColorGridForm
    {
        HopfieldNeuronNet hopfieldNet;

        public AssociationMemoryForm(HopfieldNeuronNet net)
        {
            InitializeComponent();
            hopfieldNet = net;
            IsClassification = false;
        }

        private void AssociationMemoryForm_Load(object sender, EventArgs e)
        {

        }

        protected override void ChangeNetSettings()
        {
            if (currentStudyPair >= studyPairs.Count) return;

            hopfieldNet.InputsCount = studyPairs[currentStudyPair].inputs.Count;
            if(!hopfieldNet.StudyPairsLoaded) hopfieldNet.StudyPairs = studyPairs;
            pairsLoaded = true;
            hopfieldNet.GraphicsNeuron.netOptions.InputsCount = hopfieldNet.InputsCount;
        }

        protected override void BuildExistingStudyPairs()
        {
            if (!hopfieldNet.StudyPairsLoaded) return;

            colorGrid.GridSize = new Size((int)Math.Sqrt(hopfieldNet.InputsCount), (int)Math.Sqrt(hopfieldNet.InputsCount));
            studyPairs = hopfieldNet.StudyPairs;
            pairsLoaded = true;
            CreatePreview();
        }

        protected override void Study(object sender, EventArgs e)
        {
            if (pairsLoaded)
            {
                hopfieldNet.Study();
                hopfieldNet.GraphicsNeuron.Refresh();
            }
        }

        protected override void RecognizePicture(object sender, EventArgs e)
        {
            if (pairsLoaded)
            {
                hopfieldNet.Relax(colorGrid);
            }
        }

        protected override void Save(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            //string[] s;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //s = File.ReadAllLines(dlg.FileName);
                File.WriteAllText(dlg.FileName, hopfieldNet.StudyPairsToString(studyPairs.Count));
            }

        }
    }
}
