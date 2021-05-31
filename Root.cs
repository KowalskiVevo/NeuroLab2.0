using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuron
{
    class Root
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Position
        {
            public bool IsEmpty { get; set; }
            public double X { get; set; }
            public double Y { get; set; }
        }

        public class Inputss
        {
            public Position Position { get; set; }
            public bool positionChanged { get; set; }
            public double value { get; set; }
            public bool wasPainted { get; set; }
            public string Name { get; set; }
        }

        //public class Frominput
        //{
        //    public Position Position { get; set; }
        //    public bool positionChanged { get; set; }
        //    public double value { get; set; }
        //    public bool wasPainted { get; set; }
        //    public string Name { get; set; }
        //}

        //public class Sinaps2
        //{
        //    public object fromNeuron { get; set; }
        //    public Frominput frominput { get; set; }
        //    public double value { get; set; }
        //    public double priv_direction { get; set; }
        //    public double dE_dx { get; set; }
        //}

        public class Akson
        {
            public object frominput { get; set; }
            public double value { get; set; }
            public double priv_direction { get; set; }
            public double dE_dx { get; set; }
        }

        public class Rand
        {
        }

        //public class Neuron2
        //{
        //    public Position Position { get; set; }
        //    public bool positionChanged { get; set; }
        //    public List<Sinaps> sinapses { get; set; }
        //    public double NET { get; set; }
        //    public double OUT { get; set; }
        //    public double studyValue { get; set; }
        //    public double Shift { get; set; }
        //    public double priv_direction { get; set; }
        //    public int activationFunction { get; set; }
        //    public List<Akson> aksons { get; set; }
        //    public double A { get; set; }
        //    public double C { get; set; }
        //    public double R { get; set; }
        //    public object Centers { get; set; }
        //    public bool active { get; set; }
        //    public bool wasPainted { get; set; }
        //    public Rand rand { get; set; }
        //}

        //public class NeuronGroup2
        //{
        //    public List<Neuron> Neurons { get; set; }
        //    public bool SecondActivate { get; set; }
        //    public double SumForSoftMax { get; set; }
        //    public bool allNeuronsWasPainted { get; set; }
        //    public int PaintedNeuronsCount { get; set; }
        //}

        //public class FromNeuron
        //{
        //    public Position Position { get; set; }
        //    public bool positionChanged { get; set; }
        //    public List<Sinaps> sinapses { get; set; }
        //    public double NET { get; set; }
        //    public double OUT { get; set; }
        //    public double studyValue { get; set; }
        //    public double Shift { get; set; }
        //    public double priv_direction { get; set; }
        //    public int activationFunction { get; set; }
        //    public List<object> aksons { get; set; }
        //    public double A { get; set; }
        //    public double C { get; set; }
        //    public double R { get; set; }
        //    public object Centers { get; set; }
        //    public bool active { get; set; }
        //    public NeuronGroup NeuronGroup { get; set; }
        //    public bool wasPainted { get; set; }
        //    public Rand rand { get; set; }
        //}

        public class NeuronGroup
        {
            public List<Neuron> Neurons { get; set; }
            public bool SecondActivate { get; set; }
            public double SumForSoftMax { get; set; }
            public bool allNeuronsWasPainted { get; set; }
            public int PaintedNeuronsCount { get; set; }
        }

        public class StudyPairss
        {
            public List<double> inputs { get; set; }
            public List<double> quits { get; set; }
            public List<double> realQuits { get; set; }
            public string name { get; set; }
        }

        public class CurrentSelection
        {
            public List<object> Neurons { get; set; }
            public bool SecondActivate { get; set; }
            public double SumForSoftMax { get; set; }
            public bool allNeuronsWasPainted { get; set; }
            public int PaintedNeuronsCount { get; set; }
        }

        public class StudyPair
        {
            public List<double> inputs { get; set; }
            public List<double> quits { get; set; }
            public List<object> realQuits { get; set; }
            public string name { get; set; }
        }

        public class Input
        {
            public Position Position { get; set; }
            public bool positionChanged { get; set; }
            public double value { get; set; }
            public bool wasPainted { get; set; }
            public string Name { get; set; }
        }

        public class LastNeuronGroup
        {
            public List<Neuron> Neurons { get; set; }
            public bool SecondActivate { get; set; }
            public double SumForSoftMax { get; set; }
            public bool allNeuronsWasPainted { get; set; }
            public int PaintedNeuronsCount { get; set; }
        }

        public class Root2
        {
            public List<Inputss> inputss { get; set; }
            public List<NeuronGroup> NeuronGroups { get; set; }
            public List<StudyPairss> studyPairss { get; set; }
            public double E { get; set; }
            public double moment { get; set; }
            public List<Position> errors { get; set; }
            public List<Position> normalizedErrors { get; set; }
            public int EraCount { get; set; }
            public CurrentSelection currentSelection { get; set; }
            public List<object> recognitionResults { get; set; }
            public bool StudyPairsLoaded { get; set; }
            public double InputsSum { get; set; }
            public bool allInputsWasPainted { get; set; }
            public double minError { get; set; }
            public double NormalizeOutputValue { get; set; }
            public object biasX { get; set; }
            public object biasY { get; set; }
            public object scaleX { get; set; }
            public object scaleY { get; set; }
            public double StudyLimit { get; set; }
            public bool AccessChangeNet { get; set; }
            public List<StudyPair> StudyPairs { get; set; }
            public List<Input> Inputs { get; set; }
            public int PaintedInputsCount { get; set; }
            public int InputsCount { get; set; }
            public int NeuronGroupsCount { get; set; }
            public LastNeuronGroup LastNeuronGroup { get; set; }
            public int OutputsCount { get; set; }
        }

    }
}
