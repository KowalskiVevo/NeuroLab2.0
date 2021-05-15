using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Database;

namespace Neuron
{
    public partial class LoadSaveTasks : Form
    {
        bool save;
        public ConnectToDatabaseForm ConnectionForm = new ConnectToDatabaseForm();
        NeuronNet net;
        DataTable loadedExamples, currentParameters;
        DataRow currentRow;
        int currentIndex = 0;

        public bool Save
        {
            get 
            {
                return save;
            }
            set 
            {
                if (save = value)
                {
                    button2.Enabled = button4.Enabled = button5.Enabled = false;
                    button3.Enabled = true;
                }
                else
                {
                    button2.Enabled = button4.Enabled = button5.Enabled = true;
                    button3.Enabled = false;
                }
            }
        }

        public NeuronNet Net
        {
            get { return net; }
            set 
            {
                net = value;
                ShowCurrentNetSettings();
            }
        }

        public LoadSaveTasks()
        {
            InitializeComponent();
        }

        public void LoadExamples()
        {
            loadedExamples = ConnectionForm.ConnectToDatabase.SelectQuery("SELECT NeuronNet.id, NeuronNet.InputsCount, NeuronNet.OutputsCount, NeuronNet.Subject, NetType.id as NetTypeID, TaskType.id AS TaskTypeID, NeuronNet.ControlID FROM  (NeuronNet INNER JOIN  NetType ON NeuronNet.NeuronNetTypeID = NetType.id) INNER JOIN TaskType ON NeuronNet.ProblemTypeID = TaskType.id ORDER BY NeuronNet.id;");
            currentRow = loadedExamples.Rows[currentIndex = 0];
            ShowCurrentRow();
        }

        public void ShowCurrentRow()
        {
            InputsCount.Text = currentRow["InputsCount"].ToString();
            OutputsCount.Text = currentRow["OutputsCount"].ToString();
           // StudyPairsCount.Text = net.StudyPairs.Count.ToString();
            TaskDescription.Text = currentRow["Subject"].ToString();
            NetTypeBox.DataIndex = (int)currentRow["NetTypeID"];
            TaskBox.DataIndex = (int)currentRow["TaskTypeID"];
        }

        public bool ConnectToDatabase()
        {
            if (ConnectionForm.Connected) return true;

            ConnectionForm.ShowDialog();

            if (ConnectionForm.Connected)
            {
                LoadTasks();
                LoadNetTypes();
            }

            return ConnectionForm.Connected;
        }

        private void LoadSaveTasks_Load(object sender, EventArgs e)
        {
                        
        }

        private void LoadSaveTasks_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        public void ShowCurrentNetSettings()
        {
            InputsCount.Text = net.InputsCount.ToString();
            OutputsCount.Text = net.OutputsCount.ToString();
            StudyPairsCount.Text = net.StudyPairs.Count.ToString();
        }

        public void LoadTasks()
        {
            TaskBox.Table = ConnectionForm.ConnectToDatabase.SelectQuery("select * from TaskType");
            TaskBox.FillControl(1, 0);
        }

        public void LoadNetTypes()
        {
            NetTypeBox.Table = ConnectionForm.ConnectToDatabase.SelectQuery("select * from NetType");
            NetTypeBox.FillControl(1, 0);            
        }

        public void AddNewTask()
        {
            ConnectionForm.ConnectToDatabase.InsertQuery(string.Format("insert into NeuronNet(InputsCount, OutputsCount, NeuronNetTypeID, ProblemTypeID, Subject , ControlID) values({0},{1},{2},{3},'{4}',{5})", InputsCount.Text , OutputsCount.Text , NetTypeBox.DataIndex , TaskBox.DataIndex , TaskDescription.Text, TaskBox.SelectedIndex ));
            int newNetID = (int)ConnectionForm.ConnectToDatabase.SelectQuery("select max(id) from NeuronNet").Rows[0].ItemArray[0];
            int newStudyPairID;

            for (int i = 0; i < net.StudyPairs.Count; i++)
            {
                ConnectionForm.ConnectToDatabase.InsertQuery(string.Format("insert into StudyPair(Name , NeuronNetID) values('{0}',{1})", net.StudyPairs[i].name , newNetID));
                newStudyPairID = (int)ConnectionForm.ConnectToDatabase.SelectQuery("select max(id) from StudyPair").Rows[0].ItemArray[0];

                for (int input = 0; input < net.StudyPairs[i].inputs.Count; input++)
                {
                    ConnectionForm.ConnectToDatabase.InsertQuery(string.Format("insert into Inputs(InputValue , StudyPairID) values({0},{1})", net.StudyPairs[i].inputs[input].ToString().Replace(',', '.'), newStudyPairID));
                }

                for (int output = 0; output < net.StudyPairs[i].quits.Count; output++)
                {
                    ConnectionForm.ConnectToDatabase.InsertQuery(string.Format("insert into Outputs(OutputValue , StudyPairID) values({0},{1})", net.StudyPairs[i].quits[output].ToString().Replace(',', '.'), newStudyPairID));
                }
            }

            for (int input = 0; input < net.InputsCount; input++)
            {
                ConnectionForm.ConnectToDatabase.InsertQuery(string.Format("insert into Parameter(Name , NeuronNetID) values('{0}',{1})", net.Inputs[input].Name, newNetID));
                
            }
        }

        private void SaveTask(object sender, EventArgs e)
        {
            if (save) AddNewTask();
        }

        private void ViewParameters(object sender, EventArgs e)
        {
            ParameterListForm form = new ParameterListForm(net);
            form.ShowDialog();
        }

        private void PrevTask(object sender, EventArgs e)
        {
            currentRow = loadedExamples.Rows[--currentIndex >= 0 ? currentIndex % loadedExamples.Rows.Count : currentIndex = 0];
            ShowCurrentRow();            
        }

        private void NextTask(object sender, EventArgs e)
        {
            currentRow = loadedExamples.Rows[++currentIndex % loadedExamples.Rows.Count];
            ShowCurrentRow();
        }

        public void LoadCurrentRowData()
        {
            int NetID = (int)currentRow["id"];
            currentParameters = ConnectionForm.ConnectToDatabase.SelectQuery(string.Format("SELECT Parameter.Name FROM NeuronNet INNER JOIN Parameter ON NeuronNet.id = Parameter.NeuronNetID WHERE NeuronNet.id = {0}", NetID));

            for (int i = 0; i < currentParameters.Rows.Count; i++)
            {
                net.Inputs[i].Name = currentParameters.Rows[i].ItemArray[0].ToString();
            }                
        }

        public void BuildNet()
        {
            DataTable valuesTable;
           
            switch ((int)currentRow["ControlID"])
            {
                case 0: net.GraphicsNeuron.ClassificationObjects(null, EventArgs.Empty); break;
                case 1: net.GraphicsNeuron.Clasterization(null, EventArgs.Empty); break;
                case 2: net.GraphicsNeuron.RecognizeImages(null, EventArgs.Empty); break;
                case 3: net.GraphicsNeuron.AssociationMemory(null, EventArgs.Empty); break;
                  
                default:
                    switch (NetTypeBox.SelectedIndex)
                    {
                        case 0: net.GraphicsNeuron.LinearNetType(null, EventArgs.Empty); break;
                        case 1: net.GraphicsNeuron.KohonenType(null, EventArgs.Empty); break;
                        case 2: net.GraphicsNeuron.HopfieldType(null, EventArgs.Empty); break;
                    }
                    break;
            }

            net.InputsCount = (int)currentRow["InputsCount"];
            net.OutputsCount = (int)currentRow["OutputsCount"];

            DataTable studyPairTable = ConnectionForm.ConnectToDatabase.SelectQuery(string.Format("select id, Name from StudyPair where NeuronNetID = {0}", (int)currentRow["id"]));
            net.StudyPairs.Clear();

            foreach (DataRow row in studyPairTable.Rows)
            {
                net.StudyPairs.Add(new StudyPair());
                net.StudyPairs.Last().name = row["Name"].ToString();
                valuesTable = ConnectionForm.ConnectToDatabase.SelectQuery(string.Format("select InputValue from Inputs where StudyPairID = {0}", (int)row["id"]));

                foreach (DataRow r in valuesTable.Rows)
                {
                    net.StudyPairs.Last().inputs.Add(float.Parse(r.ItemArray[0].ToString().Replace('.',',')));
                }

                valuesTable = ConnectionForm.ConnectToDatabase.SelectQuery(string.Format("select OutputValue from Outputs where StudyPairID = {0}", (int)row["id"]));

                foreach (DataRow r in valuesTable.Rows)
                {
                    net.StudyPairs.Last().quits.Add(float.Parse(r.ItemArray[0].ToString().Replace('.', ',')));
                }
            }
            net.StudyPairsLoaded = true;            
        }

        private void LoadCurrentExample(object sender, EventArgs e)
        {
            BuildNet();
            LoadCurrentRowData();
        }
    }
}
