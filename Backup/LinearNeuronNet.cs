using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ColorGrid;
using System.Drawing;

namespace Neuron
{
    [Serializable]
    public class LinearNeuronNet : NeuronNet
    {
        public LinearNeuronNet()
        {
            Neuron.DefaultActivationFunction = ActivationFunction.LOGISTIC;

            for (int i = 0; i < 2; i++) Inputs.Add(new NeuronInput("Параметр № " + (i + 1).ToString()));

            NeuronGroups.Add(new NeuronGroup(2));
            NeuronGroups.Add(new NeuronGroup(1));
            SetSinapses();            
        }

        public StudyPair Recognize(GridItem[,] data)
        {
            int inputCount = 0;

            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    Inputs[inputCount++].value = data[j, i].FloatValueClassification;
                }
            }

            return Recognize();
        }

        public StudyPair Recognize()
        {
            float[] error = new float[StudyPairs.Count];
            float curr_error = 0;
            int minIndex = 0;
            float minValue = float.MaxValue;

            LastNeuronGroup.SecondActivate = true;
            Calculate();
            LastNeuronGroup.SecondActivate = false;

            for (int i = 0; i < StudyPairs.Count; i++, curr_error = 0)
            {
                for (int j = 0; j < LastNeuronGroup.Neurons.Count; j++)
                {
                    curr_error += Math.Abs(LastNeuronGroup.Neurons[j].OUT - StudyPairs[i].quits[j]);
                }

                error[i] = curr_error;
            }

            recognitionResults.Clear();

            for (int i = 0; i < error.Length; i++)
            {
                if (minValue > error[i])
                {
                    minValue = error[i];
                    minIndex = i;
                }

                recognitionResults.Add(new RecognitionResult(StudyPairs[i], (int)(LastNeuronGroup.Neurons[i].OUT * 100)));
            }

            return StudyPairs[minIndex];
        }

        public Bitmap GetNetView()
        {
            //if (InputsCount != 2 || OutputsCount != 1) return;
            Bitmap bitmap = new Bitmap(200 , 200);
            Graphics grfx = Graphics.FromImage(bitmap);
            Font fnt = new Font("Times New Roman" , 10);
            float x = 0, y = 0;
            float addX = 1.0f / bitmap.Width, addY = 1.0f / bitmap.Height;
            float OUT;
            
            for (int i = 0; i < bitmap.Width; i++, y = 0, x += addX)
            {
                for (int j = 0; j < bitmap.Height; j++, y += addY)
                {
                    Inputs[0].value = x;
                    Inputs[1].value = y;
                    OUT = Calculate();
                    if (OUT < 0) OUT = 0;
                    bitmap.SetPixel(i, j, Color.FromArgb((int)(255 * OUT), (int)(255 * OUT), (int)(255 * OUT)));
                }
            }

            grfx.DrawString("{0,0}", fnt, Brushes.Green, 0, 0);
            grfx.DrawString("{1,0}", fnt, Brushes.Green, bitmap.Width - 40, 0);
            grfx.DrawString("{0,1}", fnt, Brushes.Green, 0, bitmap.Height - 20);
            grfx.DrawString("{1,1}", fnt, Brushes.Green, bitmap.Width - 40, bitmap.Height - 20);

            return bitmap;
        }
    }
}
