using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neuron
{
    [Serializable]
    public class RBFNeuralNet: NeuronNet
    {
        public RBFNeuralNet()
        {
            Inputs.Add(new NeuronInput("Параметр № " + (1).ToString()));
            Neuron.DefaultActivationFunction = ActivationFunction.RADIAL_VEC;
            NeuronGroups.Add(new NeuronGroup(2));
            Neuron.DefaultActivationFunction = ActivationFunction.LINEAR;
            NeuronGroups.Add(new NeuronGroup(1));
            SetSinapses();
            initCenters();
        }
        public void initCenters()
        {
           // float dc,offset;
            Random rand=new Random();
           // dc = 0.8f / (NeuronGroups[0].Neurons.Count-1);
            //offset = 0.1f;
            for(int i=0;i<NeuronGroups[0].Neurons.Count;i++)
            {
                NeuronGroups[0].Neurons[i].Centers=new float[InputsCount];
                NeuronGroups[0].Neurons[i].A = (float)rand.NextDouble()*10;
                for (int j = 0; j < NeuronGroups[0].Neurons[i].sinapses.Count; j++)
                {
                    NeuronGroups[0].Neurons[i].Centers[j] = (float)rand.NextDouble();
       //             NeuronGroups[0].Neurons[i].sinapses[j].value = (float)rand.NextDouble();                   
                }
                NeuronGroups[0].Neurons[i].Shift = 0;
       //         offset += dc;
            }
            for (int i = 0; i < LastNeuronGroup.Neurons.Count; i++)
            {
                LastNeuronGroup.Neurons[i].Shift = 0;
            }
            
        }
        public override void SetSinapses()
        {
            base.SetSinapses();
            initCenters();
        }
        public override void StudyByGroups(int group)
        {
            float Ew, Ec, Er;
            Ew = 0.8f;
            Ec =  0.0001f;
            Er = E;
            
            float dE_dOut;
            NeuronGroup currGroup = NeuronGroups[group];
            if (currGroup == LastNeuronGroup)
            {
                for (int i = 0; i < currGroup.Neurons.Count; i++)
                {
                    dE_dOut = currGroup.Neurons[i].OUT - currGroup.Neurons[i].studyValue;
                    for (int j = 0; j < currGroup.Neurons[i].sinapses.Count; j++)
                    {
                        currGroup.Neurons[i].sinapses[j].dE_dx = dE_dOut;
                    }
                }
            }
            else
            {                
                float[] dc=new float[InputsCount];
                float da,dw;
                Neuron n;
                for(int i=0;i<currGroup.Neurons.Count;i++)
                {
                    n = currGroup.Neurons[i];
                    dE_dOut = -n.dE_dX();
                    dw = n.OUT;
                    da = -n.aksons[0].value * n.OUT * n.NET;
                    for (int j = 0; j < InputsCount; j++)
                    {
                        dc[j] = 2*n.aksons[0].value * n.OUT*n.A*(Inputs[j].value-n.Centers[j]);
                    }
                    
                    n.A += Er*da*dE_dOut;
                    n.A=Math.Abs(n.A);
                    for (int j = 0; j < InputsCount; j++)
                    {
                        n.Centers[j] +=  Ec*dc[j] * dE_dOut;
                        if (n.Centers[j] > 1)
                        {
                            n.Centers[j] += 1.2f * (1-n.Centers[j]);
                        }
                        if (n.Centers[j] < 0)
                        {
                            n.Centers[j] += -1.2f * n.Centers[j];
                        }
                    }
                    n.aksons[0].value += Ew*dw*dE_dOut;
                }
            }
        }
    }
     
}
