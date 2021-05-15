using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neuron
{
    public class LinearSystemTask: NeuronNet
    {
        public int epochNum = 1;
        float maxValue = float.MinValue;

        public LinearSystemTask()
        {
            Neuron.DefaultActivationFunction = ActivationFunction.LINEAR;

            NeuronGroups.Add(new NeuronGroup(2));
            NeuronGroups.Add(new NeuronGroup(2));
            InputsCount = 2;
        }

        public override int InputsCount
        {
            get
            {
                return Inputs.Count;
            }
            set
            {
                if (value < 1) return;
                allInputsWasPainted = true;

                Inputs.Clear();
                for (int i = 0; i < value; i++)
                    Inputs.Add(new NeuronInput("Параметр № " + (i + 1).ToString()));

                LastNeuronGroup.SetNeuronCount(value);
                if (GraphicsNeuron != null) GraphicsNeuron.CalculateObjectsPositions();
            }
        }
        public override int NeuronGroupsCount
        {
            get
            {
                return NeuronGroups.Count;
            }
        }
        public override int OutputsCount
        {
            get
            {
                return LastNeuronGroup.Neurons.Count;
            }

            set
            {
                if (value > 0)
                {
                    InputsCount = value;
                }
            }
        }

        public int FindMaximum(Matrix A)
        {
            int i = 0, j = 0;
            for (i = 0; i < A.Width; i++)
            {
                for (j = 0; j < A.Height; j++)
                    if (maxValue < A[i, j]) maxValue = A[i, j]; 
            }

            return 1;
        }

        public int InitInputs(Matrix X)
        {
            if (X.Height != InputsCount) return 0;

            for (int i = 0; i < X.Height; i++)
            {
                Inputs[i].value = X[0, i];
            }

            return 1;
        }

        public int InitTask(Matrix A, Matrix B)
        {
            NeuronGroups.Clear();
            NeuronGroups.Add(new NeuronGroup(A.Width));
            NeuronGroups.Add(new NeuronGroup(A.Height));

            InputsCount = A.Height;
            OutputsCount = A.Height;

            InitNet(A, B);
            return 1;
        }

        public int InitNet(Matrix A, Matrix B)
        {
            if (NeuronGroupsCount != 2) return 0;

            SetSinapses();
            int l = 0, i = 0, j = 0;

            maxValue = float.MinValue;
            FindMaximum(A);
            FindMaximum(B);

            for (l = 0; l < NeuronGroupsCount; l++)
                for (i = 0; i < NeuronGroups[l].Neurons.Count; i++)
                {
                    NeuronGroups[l].Neurons[i].Shift = (l == 0) ? -B[0, i]/maxValue : 0;
                    for (j = 0; j < NeuronGroups[l].Neurons[i].sinapses.Count; j++)
                        NeuronGroups[l].Neurons[i].sinapses[j].value = (l == 0) ? (A[i, j]/maxValue) : A.Transponation()[i, j]/maxValue;
                }
                
            return 1;
        }

        public virtual void SetSinapses()
        {
            if (!AccessChangeNet) return;

            for (int i = 0; i < NeuronGroups.Count; i++)
            {
                for (int j = 0; j < NeuronGroups[i].Neurons.Count; j++)
                {
                    NeuronGroups[i].Neurons[j].aksons.Clear();
                }
            }

            for (int i = 0; i < NeuronGroups[0].Neurons.Count; i++)
            {
                NeuronGroups[0].Neurons[i].sinapses.Clear();

                for (int j = 0; j < inputs.Count; j++)
                {
                    NeuronGroups[0].Neurons[i].sinapses.Add(new Sinaps(inputs[j]));
                }
            }

            Sinaps CurSin;
            for (int i = 1; i < NeuronGroups.Count; i++)
            {
                for (int j = 0; j < NeuronGroups[i].Neurons.Count; j++)
                {
                    NeuronGroups[i].Neurons[j].sinapses.Clear();

                    for (int k = 0; k < NeuronGroups[i - 1].Neurons.Count; k++)
                    {
                        NeuronGroups[i].Neurons[j].sinapses.Add(CurSin = new Sinaps(NeuronGroups[i - 1].Neurons[k]));
                        NeuronGroups[i - 1].Neurons[k].aksons.Add(CurSin);
                    }
                }
            }
        }

        public override float Calculate()
        {
            int i = 0, j = 0;
            for (i = 0; i < NeuronGroups.Count; i++)
            {
                for (j = 0; j < NeuronGroups[i].Neurons.Count; j++)
                {
                    NeuronGroups[i].Neurons[j].CalculateNET();

                    if (i == 0) NeuronGroups[i].Neurons[j].Activate();
                    else NeuronGroups[i].Neurons[j].Activate();
                }
            }

            float ERR = 0.0f;
            for (i = 0; i < NeuronGroups[0].Neurons.Count; i++)
                ERR += (float)Math.Pow(NeuronGroups[0].Neurons[i].OUT, 2.0);

            float H = maxValue/50;
            for (i = 0; i < LastNeuronGroup.Neurons.Count; i++)
                Inputs[i].value = Inputs[i].value - (2 * H * LastNeuronGroup.Neurons[i].OUT);

            epochNum++;
            return ERR / 2 / NeuronGroups[0].Neurons.Count;
        }

        public Matrix GetOutput()
        {
            Matrix X = new Matrix(1, Inputs.Count);

            for (int i = 0; i < Inputs.Count; i++)
                X[0, i] = Inputs[i].value;

            return X;
        }

        public int GetEpoches()
        {
            return epochNum;
        }
    }
}
