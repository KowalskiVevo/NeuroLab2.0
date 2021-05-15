using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neuron
{
    public class HopfieldNeuronNet : NeuronNet
    {
        Matrix stydyPairsMatrix, weightsMatrix , inputMatrix;

        public HopfieldNeuronNet()
        {
            Neuron.DefaultActivationFunction = ActivationFunction.LIMIT;

            for (int i = 0; i < 3; i++) Inputs.Add(new NeuronInput("Параметр № " + (i + 1).ToString()));

            NeuronGroups.Add(new NeuronGroup(3));
            SetSinapses(); 
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
                for (int i = 0; i < value; i++) Inputs.Add(new NeuronInput("Параметр № " + (i + 1).ToString()));
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
            set { }
        }

        public override void SetNeuronsCount(int group, int count)
        {
            InputsCount = count;
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

        private void CreateStudyPairsMatrix()
        { 
            stydyPairsMatrix = new Matrix(InputsCount , studyPairs.Count);

            for (int i = 0; i < InputsCount; i++)
            {
                for (int j = 0; j < studyPairs.Count; j++)
                {
                    stydyPairsMatrix[i, j] = studyPairs[j].inputs[i];
                }
            }
            
            weightsMatrix = stydyPairsMatrix.Transponation() * stydyPairsMatrix - Matrix.Identity;
            bool simmetric = weightsMatrix.IsSimmetric();
            weightsMatrix.ToZeroMainLine();
        }

        private bool CreateStudyPairsMatrix1()
        {
            if (studyPairs.Count == 0) return false;

            stydyPairsMatrix = new Matrix(InputsCount, 1);
            weightsMatrix = new Matrix(InputsCount, InputsCount);

            for (int j = 0; j < studyPairs.Count; j++)
            {
                for (int i = 0; i < InputsCount; i++)
                {
                    stydyPairsMatrix[i, 0] = studyPairs[j].inputs[i];
                }

                weightsMatrix += stydyPairsMatrix.Transponation() * stydyPairsMatrix - Matrix.Identity;
            }

            weightsMatrix /= InputsCount;
            //bool simmetric = weightsMatrix.IsSimmetric();
            //weightsMatrix.ToZeroMainLine();
            return true;
        }

        public override void SetSinapses()
        {
            //if(!CreateStudyPairsMatrix1()) return;
            CreateStudyPairsMatrix();

            for (int i = 0; i < LastNeuronGroup.Neurons.Count; i++)
            {
                LastNeuronGroup.Neurons[i].sinapses.Clear();

                for (int j = 0; j < LastNeuronGroup.Neurons.Count; j++)
                {
                    LastNeuronGroup.Neurons[i].sinapses.Add(new Sinaps(LastNeuronGroup.Neurons[j]));
                    LastNeuronGroup.Neurons[i].sinapses.Last().value = weightsMatrix[j , i];
                   
                    /* if(i != j)
                    {
                        for (int k = 0; k < studyPairs.Count; k++)
                        {
                            LastNeuronGroup.Neurons[i].sinapses.Last().value += studyPairs[k].inputs[i] * studyPairs[k].inputs[j];
                        }
                    }*/
                }
            }            
        }

        public override void LoadNextStudyPair(int studyPair)
        {
            for (int i = 0; i < InputsCount; i++)
            {
                LastNeuronGroup.Neurons[i].OUT = studyPairs[studyPair].inputs[i];
            }

            SetSinapses();
        }

        public override void  Study()
        {
            SetSinapses();
        }

        private float NeuronFunction(float x)
        {
            if (x >= 0) return 1;           
            return -1;
            //return (float)Math.Tanh(x);
        }

        public override float Calculate()
        {
            inputMatrix = (inputMatrix * weightsMatrix).SetFunction(NeuronFunction);
            
            return 0;
        }

        public void Relax(ColorGrid.ColorGrid grid)
        {
            inputMatrix = new Matrix(InputsCount , 1);

            for (int i = 0; i < grid.Data.GetLength(0); i++)
            {
                for (int j = 0; j < grid.Data.GetLength(1); j++)
                {
                    inputMatrix[i * grid.Data.GetLength(0) + j, 0] = grid.Data[j, i].FloatValueAssociationMemory;
                }
            }

           // for (int i = 0; i < 10; i++) 
            Calculate();

            for (int i = 0; i < grid.Data.GetLength(0); i++)
            {
                for (int j = 0; j < grid.Data.GetLength(1); j++)
                {
                    grid.Data[j, i].FloatValueAssociationMemory = inputMatrix[i * grid.Data.GetLength(0) + j, 0];
                }
            }

            grid.DrawGrid();
            grid.RedrawData();
        }        

    }
}
