using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neuron
{
    [Serializable]
    public class KohonenNeuronNet : NeuronNet
    {
        Neuron winner = new Neuron();

        public KohonenNeuronNet()
        {
            Neuron.DefaultActivationFunction = ActivationFunction.RADIAL;

            for (int i = 0; i < 2; i++) Inputs.Add(new NeuronInput("Параметр № " + (i + 1).ToString()));

            NeuronGroups.Add(new NeuronGroup(3));
            SetSinapses();            
        }

        public override int OutputsCount
        {
            get
            {
                return LastNeuronGroup.Neurons.Count;
            }

            set
            {
                if (!AccessChangeNet) return;

                if (value > 0)
                    SetNeuronsCount(NeuronGroups.Count - 1, value);
            }
        }

        private float NormalizeFunction(float x)
        {
            return (float)(Math.Pow(x , 2) / Math.Pow(EraCount, 2));
        }

        public void LoadNextStudyPair(int studyPair , int time)
        {
            if (studyPair >= studyPairs.Count) return;
            for (int i = 0; i < Inputs.Count; i++) Inputs[i].value = StudyPairs[studyPair].inputs[i] * NormalizeFunction(time) + (1.0f - NormalizeFunction(time)) / ((float)Math.Sqrt(InputsCount));            
            Calculate();            
        }

        public override void LoadNextStudyPair(int studyPair)
        {
            if (studyPair >= studyPairs.Count) return;

            for (int i = 0; i < Inputs.Count; i++) Inputs[i].value = StudyPairs[studyPair].inputs[i];

            Calculate();            
        }

        public override void SetSinapses()
        {
            Sinaps s;

            for (int i = 0; i < NeuronGroups[0].Neurons.Count; i++)
            {
                NeuronGroups[0].Neurons[i].sinapses.Clear();

                for (int j = 0; j < InputsCount; j++)
                {
                    s = new Sinaps(Inputs[j]);
                    s.value = (float)(1.0 / Math.Sqrt(InputsCount));
                    NeuronGroups[0].Neurons[i].sinapses.Add(s);
                }
            }            
        }

        public override float Calculate()
        {
            float outValue = float.MinValue;
            
            foreach (Neuron n in LastNeuronGroup.Neurons)
            {
                n.NET = 0;

                for (int i = 0; i < InputsCount; i++) n.NET += (float)Math.Pow(Inputs[i].value - n.sinapses[i].value, 2);

                n.OUT = (float)Math.Exp(-n.NET * 5);               
            }

            foreach (Neuron n in LastNeuronGroup.Neurons)
            {
                if (outValue < n.OUT)
                {
                    outValue = n.OUT;
                    winner = n;
                }
            }

            winner.OUT = 1;

            return 0;
        }

        public override void Study()
        {
            for (int i = 0; i < EraCount; i++)
            {
                for (int j = 0; j < studyPairs.Count; j++)
                {
                    LoadNextStudyPair(j , i);                   
                    RebuildWeights();
                }
            }
        }

        public override void StudyByParts(int eracount , int time)
        {
            for (int i = 0; i < eracount; i++)
            {
              //  E = (float)(1.0 / Math.Exp((time + i) / EraCount / 3)) / 7.5f;

                for (int k = 0; k < StudyPairs.Count; k++)
                {
                    LoadNextStudyPair(k , time + i);
                    RebuildWeights();
                }
            }
        }

        private void RebuildWeights()
        {
            foreach (Sinaps s in winner.sinapses)
            {
                s.value += E * (s.frominput.value - s.value); 
            }
        }

        public override int NeuronGroupsCount
        {
            get
            {
                return NeuronGroups.Count;
            }
            set { }
        }
        
    }
}
