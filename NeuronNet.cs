using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ColorGrid;
//using NLog.Internal.Xamarin;
using Newtonsoft.Json;

namespace Neuron
{
    [Serializable]
    public class NeuronInput
    {
        [JsonProperty("Position")]
        public PointF Position;
        [JsonProperty("positionChanged")]
        public bool positionChanged = false;
        [JsonProperty("value")]
        public float value = 1.1f;
        [JsonProperty("wasPainted")]
        public bool wasPainted = false;
        [JsonProperty("Name")]
        public string Name;


        public NeuronInput(string name)
        {
            this.Name = name;
        }

        public NeuronInput(float val)
        {
            value = val;
        }

        [JsonConstructor]
        public NeuronInput(float val,string name, PointF position)
        {
            this.Position = position;
            value = val;
            this.Name = name;
        }
    }
    [Serializable]
    public class Sinaps
    {
        public Neuron fromNeuron = null;
        public NeuronInput frominput = null;
        public float value;
        public float priv_direction = 0f;
        public static Random rand = new Random();
        public float dE_dx;
        static Point interval = new Point(-50, 50);
        static int multiplier = 1000;

        public static PointF Interval
        {
            set
            {
                interval = new Point((int)(value.X * multiplier), (int)(value.Y * multiplier));
            }
        }

        public Sinaps(Neuron neuron)
        {
            fromNeuron = neuron;
            value = ((float)rand.Next(interval.X, interval.Y)) / multiplier;
            // value = 0.2f;
        }

        public Sinaps(NeuronInput input)
        {
            frominput = input;
            value = ((float)rand.Next(interval.X, interval.Y)) / multiplier;
            //  value = 0.2f;
        }

        [JsonConstructor]
        public Sinaps (NeuronInput input, Neuron neuron)
        {
            fromNeuron = neuron;
            frominput = input;
            value = ((float)rand.Next(interval.X, interval.Y)) / multiplier;
        }

    }

    public enum ActivationFunction
    {
        LIMIT = 0,
        LOGISTIC = 1,
        TANGENS = 2,
        RADIAL = 3,
        GORBAN = 4,
        LINEAR = 5,
        RADIAL_VEC = 6,
        SOFT_MAX = 7
    }
    [Serializable]
    public class Neuron
    {
        public PointF Position;
        public bool positionChanged = false;
        public List<Sinaps> sinapses = new List<Sinaps>();
        public float NET, OUT, studyValue, Shift = 0.2f;
        public float priv_direction = 0f;
        public ActivationFunction activationFunction = ActivationFunction.LOGISTIC;
        public List<Sinaps> aksons = new List<Sinaps>();
        public float A = 1f, C, R;
        public float[] Centers;
        public bool active = false;
        public NeuronGroup NeuronGroup = null;
        public bool wasPainted = false;
        public Random rand = new Random();
        public static ActivationFunction DefaultActivationFunction = ActivationFunction.LOGISTIC;

        public Neuron()
        {
            Shift = (float)rand.Next(-500, 500) / 1000;
            activationFunction = DefaultActivationFunction;
        }

        [JsonConstructor]
        public Neuron(NeuronGroup group)
        {
            NeuronGroup = group;
            activationFunction = DefaultActivationFunction;
        }

        
        public Neuron(Neuron obj, NeuronGroup group)
        {
            if (obj != null)
            {
                positionChanged = obj.positionChanged;
                activationFunction = obj.activationFunction;
                A = obj.A;
                active = obj.active;
                wasPainted = obj.wasPainted;
            }
            else
            {
                activationFunction = DefaultActivationFunction;
            }
            Shift = (float)rand.Next(-500, 500) / 1000;
            NeuronGroup = group;
        }

        public float dOUT_dNET()
        {
            float buf;
            switch (activationFunction)
            {
                case ActivationFunction.LOGISTIC: return A * OUT * (1 - OUT); break;
                case ActivationFunction.TANGENS: buf = 2 * OUT - 1; return 0.5f * (1 - buf * buf); break;
                case ActivationFunction.RADIAL: return -NET * OUT / ((float)Math.Sqrt(2 * Math.PI * R)); break;
                case ActivationFunction.RADIAL_VEC: return -OUT; break;
                case ActivationFunction.GORBAN: buf = 1 - (2 * OUT - 1) * (2 * OUT - 1); return 0.5f * buf * buf; break;
                case ActivationFunction.SOFT_MAX: return OUT / NeuronGroup.SumForSoftMax; break;
            }

            return 0;
        }

        public void Activate()
        {
            switch (activationFunction)
            {
                case ActivationFunction.LIMIT: if (NET > 0.0f) OUT = 1; else OUT = 0; break;
                case ActivationFunction.LOGISTIC: OUT = 1.0f / (1.0f + (float)Math.Exp(-A * NET)); break;
                case ActivationFunction.TANGENS: OUT = (float)0.5 * ((Single)Math.Tanh(NET * A) + 1); break;
                case ActivationFunction.RADIAL: OUT = (float)(Math.Exp(-Math.Pow(NET - C, 2) / (2 * R * R)) / Math.Sqrt(2 * Math.PI * R)); break;
                case ActivationFunction.RADIAL_VEC: OUT = (float)(Math.Exp(-A * NET)); break;
                case ActivationFunction.GORBAN: OUT = (float)0.5 * (NET / (1.0f + Math.Abs(NET)) + 1); break;
                case ActivationFunction.LINEAR: OUT = NET; break;
                case ActivationFunction.SOFT_MAX: OUT = (float)(Math.Exp(NET)) / NeuronGroup.SumForSoftMax; break;
            }
        }

        public void SecondActivate()
        {
            OUT = (float)(Math.Pow(OUT, 2) / NeuronGroup.SumForSoftMax);
        }

        public float ActivateFunction(float x)
        {
            switch (activationFunction)
            {
                case ActivationFunction.LIMIT: if (x > 0.0f) return 1; return 0;
                case ActivationFunction.LOGISTIC: return 1.0f / (1.0f + (float)Math.Exp(-A * x));
                case ActivationFunction.TANGENS: return (float)0.5f * (Single)Math.Tanh(x * A);
                case ActivationFunction.RADIAL: return (float)(Math.Exp(-A * Math.Pow(x, 2)));
                case ActivationFunction.RADIAL_VEC: return (float)(Math.Exp(-A * Math.Pow(x, 2)));
                case ActivationFunction.GORBAN: return (float)0.5f * (x / (1.0f + Math.Abs(x) + 1));
                case ActivationFunction.LINEAR: return x;
                case ActivationFunction.SOFT_MAX: return (float)(Math.Exp(x)) / NeuronGroup.SumForSoftMax;
            }

            return 0;
        }

        public float CalculateNET()
        {
            NET = Shift;
            if (activationFunction == ActivationFunction.RADIAL_VEC)
            {
                float buffer;
                for (int i = 0; i < sinapses.Count; i++)
                {
                    buffer = (sinapses[i].frominput == null ? sinapses[i].fromNeuron.OUT : sinapses[i].frominput.value) - Centers[i];
                    NET += buffer * buffer;
                }
                //NET = (float)Math.Sqrt(NET);
            }
            else
            {
                for (int i = 0; i < sinapses.Count; i++) NET += sinapses[i].value * (sinapses[i].frominput == null ? sinapses[i].fromNeuron.OUT : sinapses[i].frominput.value);
            }
            return NET;
        }

        public float dE_dX()
        {
            float res = 0;

            for (int i = 0; i < aksons.Count; i++) res += aksons[i].dE_dx;

            return res/* / aksons.Count*/;
        }

        public override string ToString()
        {
            return string.Format("{0|0.00}", OUT);
        }
    }
    [Serializable]
    public class NeuronGroup
    {
        //[JsonProperty("Neurons")]
        public List<Neuron> Neurons = new List<Neuron>();
        public bool SecondActivate = false;
        public float SumForSoftMax = 0;
        public static int maxNeuronsCount = 6;
        public bool allNeuronsWasPainted = true;

        public int PaintedNeuronsCount
        {
            get
            {
                if (Neurons.Count > maxNeuronsCount)
                {
                    allNeuronsWasPainted = false;
                    return maxNeuronsCount;
                }
                else allNeuronsWasPainted = true;

                return Neurons.Count;
            }
            set
            {
                
            }
        }

        [JsonConstructor]
        public NeuronGroup()
        {
            Neurons.Add(new Neuron(this));
        }

        public NeuronGroup(int neuronCount)
        {
            if (neuronCount == 0)
            {
                Neurons.Add(new Neuron(this));
            }
            else
            {
                for (int i = 0; i < neuronCount; i++) Neurons.Add(new Neuron(this));
            }
            
        }

        public void SetNeuronCount(int count)
        {
            Neuron exepl;
            if (Neurons.Count() > 0)
            {
                exepl = Neurons[0];
                Neurons.Clear();
                for (int i = 0; i < count; i++) Neurons.Add(new Neuron(exepl, this));
            }
            else
            {
                Neurons.Clear();
                for (int i = 0; i < count; i++) Neurons.Add(new Neuron(this));
            }
        }


        public void CalculateSumForSoftMax()
        {
            SumForSoftMax = 0;

            for (int i = 0; i < Neurons.Count; i++) SumForSoftMax += (float)Math.Pow(Neurons[i].OUT, 2);
        }
    }
    [Serializable]
    public class StudyPair
    {
        public List<float> inputs;
        public List<float> quits;
        public List<float> realQuits;
        public string name = "";

        public StudyPair()
        {
            inputs = new List<float>();
            quits = new List<float>();
            realQuits = new List<float>();
        }

        public static StudyPair FromString(string str)
        {
            string tmp = "";
            StudyPair pair = new StudyPair();
            bool output = false;
            bool name = false;

            pair.inputs.Clear();

            for (int i = 0; i < str.Length; i++)
            {
                switch (str[i])
                {
                    case '-':
                    case ',':
                    case '.': tmp += str[i]; break;

                    case '|':

                        if (!output) pair.inputs.Add(Convert.ToSingle(tmp));
                        else
                        {
                            pair.quits.Add(Convert.ToSingle(tmp));
                            pair.realQuits.Add(0);
                        }
                        tmp = "";
                        break;

                    case ';':
                        if (output) pair.quits.Add(Convert.ToSingle(tmp));
                        else
                        {
                            pair.inputs.Add(Convert.ToSingle(tmp));
                            pair.realQuits.Add(0);
                        }
                        output = true;
                        tmp = "";
                        break;

                    case '&':
                        if (name) pair.name = tmp;
                        name = true;
                        break;

                    case '#': return pair;

                    default:
                        if (char.IsDigit(str[i])) tmp += str[i];
                        if (name) tmp += str[i];
                        break;
                }
            }

            return pair;
        }

        public override string ToString()
        {
            string str = "";

            for (int i = 0; i < inputs.Count; i++) str += inputs[i].ToString("0.00") + "|";
            str = str.Remove(str.Length - 1) + ";";

            for (int i = 0; i < quits.Count; i++) str += quits[i].ToString("0.00") + "|";
            str = str.Remove(str.Length - 1) + ";";

            str += string.Format("&{0}&#", name);

            for (int i = 0; i < realQuits.Count; i++)
                str += realQuits[i].ToString("0.00") + "|";


            return str;
        }

        public string ToString(int quitsCount, int currPair)
        {
            string str = "";

            for (int i = 0; i < inputs.Count; i++) str += inputs[i].ToString("0.00") + "|";
            str = str.Remove(str.Length - 1) + ";";
            for (int i = 0; i < quitsCount; i++) str += (i == currPair ? 1 : -1).ToString("0.00") + "|";
            str = str.Remove(str.Length - 1) + ";";

            str += string.Format("&{0}&", name);

            return str;
        }
    }
    [Serializable]
    public class RecognitionResult
    {
        public StudyPair pair;
        public int result;

        public RecognitionResult(StudyPair studyPair, int res)
        {
            pair = studyPair;
            result = res;
        }
    }

    [Serializable]
    public class NeuronNet 
    {
        [JsonProperty("inputss")]
        public List<NeuronInput> inputss = new List<NeuronInput>();
        [JsonProperty("NeuronGroups")]
        public List<NeuronGroup> NeuronGroups = new List<NeuronGroup>();
        [NonSerialized] public NeuronGraphics GraphicsNeuron;
        [JsonProperty("studyPairss")]
        public List<StudyPair> studyPairss = new List<StudyPair>();
        int currentStudyPair = 0;
        public float E = 0.9f,moment=0.3f;
        Random rand = new Random();
        public List<PointF> errors = new List<PointF>(), normalizedErrors = new List<PointF>();
        public int EraCount = 1000;
        public NeuronGroup currentSelection = new NeuronGroup(0);
        public List<RecognitionResult> recognitionResults = new List<RecognitionResult>();
        public bool StudyPairsLoaded = false;
        public float InputsSum;
        public bool allInputsWasPainted = true;
        public float minError;
        public float NormalizeOutputValue = 1;
        public float[] biasX = null;
        public float[] biasY = null;
        public float[] scaleX = null;
        public float[] scaleY = null;
        public float StudyLimit = 0.0001f;
        [NonSerialized] public Parser.Parser.Variable VariableI , VariableN , VariableD , VariableM;
        [NonSerialized] public Parser.Parser parser = new Parser.Parser();
        public bool AccessChangeNet = true;

        public NeuronNet()
        {
            InitializeParser();
        }

        public void InitializeParser()
        {
            parser = new Parser.Parser();
            parser.InputString = "(1/exp(i/(N/D)))/M";
            VariableN = parser.GetVariable("n");
            VariableI = parser.GetVariable("i");
            VariableD = parser.GetVariable("d");
            VariableM = parser.GetVariable("m");
            VariableM.value = 5;
            VariableD.value = 3;
        }

        [JsonProperty("StudyPairs")]
        public List<StudyPair> StudyPairs
        {
            get 
            {
                return studyPairss;
            }
            set 
            {
                studyPairss.Clear();
                for (int i = 0; i < value.Count; i++) studyPairss.Add(value[i]);
                StudyPairsLoaded = true;
            }
        }

        public List<NeuronInput> Inputs
        {
            get 
            {
                return inputss;
            }
            set 
            {
                if (!AccessChangeNet) return;
                inputss.Clear();
                for (int i = 0; i < value.Count; i++) inputss.Add(value[i]);
            }
        }

        public int PaintedInputsCount
        {
            get 
            {
                if (inputss.Count > NeuronGroup.maxNeuronsCount)
                {
                    allInputsWasPainted = false;
                    return NeuronGroup.maxNeuronsCount;
                }

                return inputss.Count;
            }
        }

        public virtual int InputsCount
        {
            get
            {
                return inputss.Count;
            }
            set
            {
                if (!AccessChangeNet) return;
                if (value < 1) return;
                allInputsWasPainted = true;
                inputss.Clear();
                for (int i = 0; i < value; i++) inputss.Add(new NeuronInput("Параметр № " + (i + 1).ToString()));
                SetSinapses();
                if(GraphicsNeuron != null) GraphicsNeuron.CalculateObjectsPositions();               
            }                     
        }

        public virtual int NeuronGroupsCount
        {
            get 
            {
                return NeuronGroups.Count;
            }
            set 
            {
                if (!AccessChangeNet) return;
                int neuronCount = LastNeuronGroup.Neurons.Count;
                NeuronGroups.Clear();
                for (int i = 0; i < value; i++) NeuronGroups.Add(new NeuronGroup());
                SetNeuronsCount(value - 1 , neuronCount);
                SetSinapses();
                if (GraphicsNeuron != null) GraphicsNeuron.CalculateObjectsPositions();
            }
        }

        public NeuronGroup LastNeuronGroup
        {
            get 
            {
                return NeuronGroups[NeuronGroups.Count - 1];
            }
        }

        public virtual int OutputsCount
        {
            get 
            {
                return LastNeuronGroup.Neurons.Count;
            }

            set
            {
                if (!AccessChangeNet) return;
                SetNeuronsCount(NeuronGroups.Count - 1 , value);
            }
        }

        public virtual void SetNeuronsCount(int group, int count)
        {
            if (count < 1) return;
            if (!AccessChangeNet) return;

            NeuronGroups[group].SetNeuronCount(count);
            SetSinapses();
            if (GraphicsNeuron != null) GraphicsNeuron.CalculateObjectsPositions();
        }

        public int GetNeuronIDMaxOUT()
        {
            int maxID = 0;
            float maxValue = float.MinValue;

            for (int i = 0; i < LastNeuronGroup.Neurons.Count; i++)
            {
                if (maxValue < LastNeuronGroup.Neurons[i].OUT)
                {
                    maxValue = LastNeuronGroup.Neurons[i].OUT;
                    maxID = i;
                }
            }

            return maxID;
        }
        public void NormalizeInputs()
        {
            InputsSum = 0;

            for (int i = 0; i < studyPairss.Count; i++, InputsSum = 0)
            {
                for (int j = 0; j < studyPairss[i].inputs.Count; j++) InputsSum += (float)Math.Pow(studyPairss[i].inputs[j], 2);

                InputsSum = (float)Math.Sqrt(InputsSum);

                for (int j = 0; j < studyPairss[i].inputs.Count; j++) studyPairss[i].inputs[j] /= InputsSum;
            }
        }

        public void NormalizeOutputs()
        {
            NormalizeOutputValue = 0;

            for (int i = 0; i < studyPairss.Count; i++)
            {
                for (int j = 0; j < studyPairss[i].quits.Count; j++)
                {
                    NormalizeOutputValue += (float)Math.Pow(studyPairss[i].quits[j], 2);
                }
            }

            NormalizeOutputValue = (float)Math.Sqrt(NormalizeOutputValue);

            for (int i = 0; i < studyPairss.Count; i++)
            {
                for (int j = 0; j < studyPairss[i].quits.Count; j++)
                {
                    studyPairss[i].quits[j] /= NormalizeOutputValue;
                }
            }
        }
        public void LinearNormalizeInputs()
        {
            float max, min;
            biasX = new float[InputsCount];
            scaleX = new float[InputsCount];            
            for(int i=0;i<InputsCount;i++)
            {
                min=float.MaxValue;
                max=float.MinValue;
                for(int j=0;j< studyPairss.Count;j++)
                {
                    if (max < studyPairss[j].inputs[i]) max = studyPairss[j].inputs[i];
                    if (min > studyPairss[j].inputs[i]) min = studyPairss[j].inputs[i];
                }
                biasX[i]=min;
                scaleX[i]=max-min;
                for (int j = 0; j < studyPairss.Count; j++)
                {
                    studyPairss[j].inputs[i] = (studyPairss[j].inputs[i]-biasX[i])/scaleX[i];
                }
            }  
        }

        public void LinearNormalizeOutputs()
        {
            float max, min;
            biasY = new float[OutputsCount];
            scaleY = new float[OutputsCount];
            for (int i = 0; i < OutputsCount; i++)
            {
                min = float.MaxValue;
                max = float.MinValue;
                for (int j = 0; j < studyPairss.Count; j++)
                {
                    if (max < studyPairss[j].quits[i]) max = studyPairss[j].quits[i];
                    if (min > studyPairss[j].quits[i]) min = studyPairss[j].quits[i];
                }
                biasY[i] = min;
                scaleY[i] = max - min;
                for (int j = 0; j < studyPairss.Count; j++)
                {
                    studyPairss[j].quits[i] = (studyPairss[j].quits[i] - biasY[i]) / scaleY[i];
                }
            }  

        }

        public void SaveRealQuits()
        {
            for (int i = 0; i < studyPairss.Count; i++)
            {
                LoadNextStudyPair(i);
                Calculate();

                StudyPairs[i].realQuits.Clear();
                for (int quit = 0; quit < this.LastNeuronGroup.Neurons.Count; quit++)
                    StudyPairs[i].realQuits.Add(LastNeuronGroup.Neurons[quit].OUT);
            }
        }

        public float StudyFunction(float x)
        {
            VariableI.value = x;
            VariableN.value = EraCount;

            return parser.Calculate();
        }

        public virtual void Study()
        {
            int interval = EraCount / 50;
            GraphicsNeuron.StudyStatus.Maximum = EraCount;
            GraphicsNeuron.StudyStatus.Value = 0;
            errors.Clear();
            minError = float.MaxValue;
            VariableN.value = EraCount;
            
            for (int i = 0; i < EraCount; i++)
            {
               // VariableI.value = i;
               // E = parser.Calculate();

                for (int k = 0; k < StudyPairs.Count; k++)
                {
                    LoadNextStudyPair(k);
                    //LoadNextStudyPair(rand.Next(0,StudyPairs.Count-1));
                    for (int j = NeuronGroups.Count - 1; j >= 0; j--) StudyByGroups(j);                                
                }
                
                SaveRealQuits();
                errors.Add(new PointF(i, GetNetError()));                    
                
                if (errors.Last().Y < StudyLimit) break;
                GraphicsNeuron.StudyStatus.Value++;
            }
                        
           
            ShowErrorForm errorForm = new ShowErrorForm(ToStandartSize());
            errorForm.drawer.GraphicBounds = new PointF(0, errors.Last().X + 1);
            errorForm.Show();
            errorForm.drawer.Redraw();
        }

        public List<PointF> ToStandartSize()
        { 
            int interval = ((int)errors.Last().X + 1) / 50;
            normalizedErrors.Clear();
            if (interval == 0) interval = 1;

            for (int i = 0; i < errors.Count; i++)
            {
                if (i % interval == 0) normalizedErrors.Add(errors[i]);
            }
            
            return normalizedErrors;
        }

        public virtual void StudyByParts(int eracount, int currentEra)
        {            
            VariableN.value = EraCount;

            for (int i = 0; i < eracount; i++)
            {                
//                VariableI.value = currentEra + i;
  //              E = parser.Calculate();

                for (int k = 0; k < StudyPairs.Count; k++)
                {
                    LoadNextStudyPair(k);
                    //LoadNextStudyPair(rand.Next(0,studyPairs.Count-1));
                    for (int j = NeuronGroups.Count - 1; j >= 0; j--) StudyByGroups(j);                    
                }
                SaveRealQuits();
                errors.Add(new PointF(currentEra+i, GetNetError()));

//                if (errors.Last().Y < StudyLimit) break;
  //              GraphicsNeuron.StudyStatus.Value++;
            }            
        }
        

        public float dNETdw(Sinaps sinaps)
        {
            return sinaps.frominput == null ? sinaps.fromNeuron.OUT : sinaps.frominput.value;
        }

        public virtual void StudyByGroups(int group)
        {
            NeuronGroup currGroup = NeuronGroups[group];
            float dE_dOUT = float.MinValue;
            float dOUT_dNET;
            float p;

            for (int i = 0; i < currGroup.Neurons.Count; i++/*, dE_dOUT = float.MinValue*/)
            {
                if (currGroup == LastNeuronGroup) dE_dOUT = currGroup.Neurons[i].OUT - currGroup.Neurons[i].studyValue;
                else dE_dOUT = currGroup.Neurons[i].dE_dX();

                dOUT_dNET = currGroup.Neurons[i].dOUT_dNET();
               

                for (int j = 0; j < currGroup.Neurons[i].sinapses.Count; j++)
                {
                    currGroup.Neurons[i].sinapses[j].dE_dx = dE_dOUT * dOUT_dNET * currGroup.Neurons[i].sinapses[j].value;    
                    p=-E*dE_dOUT * dNETdw(currGroup.Neurons[i].sinapses[j]) * dOUT_dNET;
                    currGroup.Neurons[i].sinapses[j].value += p+moment*currGroup.Neurons[i].sinapses[j].priv_direction;
                    currGroup.Neurons[i].sinapses[j].priv_direction = p;
                }
                p = -E*dE_dOUT * dOUT_dNET;
                currGroup.Neurons[i].Shift += p+moment*currGroup.Neurons[i].priv_direction;
                currGroup.Neurons[i].priv_direction = p;
            }
        }

        public float GetNetError()
        {
            float result = 0;

            for (int i = 0; i < LastNeuronGroup.Neurons.Count; i++)
            {
                for (int j = 0; j < StudyPairs.Count; j++)
                {
                    result += (float)Math.Pow(Math.Pow(StudyPairs[j].quits[i], 2) - Math.Pow(StudyPairs[j].realQuits[i], 2), 2);
                }
            }

            return result / 2;
        }

        public virtual void SetSinapses()
        {
            if (!AccessChangeNet) return;

            Sinaps curr;

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

                for (int j = 0; j < inputss.Count; j++)
                {
                    NeuronGroups[0].Neurons[i].sinapses.Add(new Sinaps(inputss[j]));
                }
            }

            for (int i = 1; i < NeuronGroups.Count; i++)
            {
                for (int j = 0; j < NeuronGroups[i].Neurons.Count; j++)
                {
                    NeuronGroups[i].Neurons[j].sinapses.Clear();

                    for (int k = 0; k < NeuronGroups[i - 1].Neurons.Count; k++)
                    {
                        NeuronGroups[i].Neurons[j].sinapses.Add(curr = new Sinaps(NeuronGroups[i - 1].Neurons[k]));
                        NeuronGroups[i - 1].Neurons[k].aksons.Add(curr);
                    }
                }
            }

            //Calculate();
        }

        public NeuronGroup Calculate(float [] input)
        {
            for (int i = 0; i < inputss.Count; i++) inputss[i].value = input[i];
            Calculate();

            return LastNeuronGroup;
        }        

        public virtual float Calculate()
        {
            for (int i = 0; i < NeuronGroups.Count; i++)
            {
                for (int j = 0; j < NeuronGroups[i].Neurons.Count; j++)
                {
                    NeuronGroups[i].Neurons[j].CalculateNET();                    
                    NeuronGroups[i].Neurons[j].Activate();
                }
            }

            if (LastNeuronGroup.SecondActivate)
            {
                LastNeuronGroup.CalculateSumForSoftMax();

                for (int i = 0; i < LastNeuronGroup.Neurons.Count; i++)
                    LastNeuronGroup.Neurons[i].SecondActivate();
            }

            return LastNeuronGroup.Neurons[0].OUT;
        }

        public virtual void LoadNextStudyPair(int studyPair)
        {
            if (studyPair >= studyPairss.Count) return;

            for (int i = 0; i < inputss.Count; i++) inputss[i].value = StudyPairs[studyPair].inputs[i];
            for (int i = 0; i < LastNeuronGroup.Neurons.Count; i++) LastNeuronGroup.Neurons[i].studyValue = StudyPairs[studyPair].quits[i];

            Calculate();

            currentStudyPair = studyPair;        
        }

        public string StudyPairsToString(int pairsCount)
        {
            string s = "";

            for (int i = 0; i < StudyPairs.Count; i++)
            {
                s += StudyPairs[i].ToString() + Environment.NewLine;
            }

            return s;
        }

        public NeuronGroup GetNeuronsByRectangle(Rectangle rect)
        {
            NeuronGroup group = new NeuronGroup(0);

            for (int i = 0; i < NeuronGroups.Count; i++)
            {
                for (int j = 0; j < NeuronGroups[i].Neurons.Count; j++)
                {
                    if (rect.Contains(new Point((int)NeuronGroups[i].Neurons[j].Position.X, (int)NeuronGroups[i].Neurons[j].Position.Y))) group.Neurons.Add(NeuronGroups[i].Neurons[j]);
                }
            }

            return group;
        }

        public void SetSelection(NeuronGroup group)
        {
            for (int i = 0; i < currentSelection.Neurons.Count; i++) currentSelection.Neurons[i].active = false;
            currentSelection = group;
            for (int i = 0; i < group.Neurons.Count; i++) group.Neurons[i].active = true;
        }

        public void SetSelectionGroupParameters(Neuron neuron)
        {
            for (int i = 0; i < currentSelection.Neurons.Count; i++)
            {
                currentSelection.Neurons[i].activationFunction = neuron.activationFunction;                   
            }
        }

        public void ClearSelectionGroup()
        {
            foreach (Neuron n in currentSelection.Neurons) n.active = false;
            currentSelection.Neurons.Clear();           
        }       
    }   
}
