using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Database;
using System.Data.SQLite;
using System.IO;
using System.Data.Common;

namespace Neuron
{
    public partial class LoadSaveTasks : Form
    {
        bool save;
        Database databaseSQLite = new Database();
        public ConnectToDatabaseForm ConnectionForm = new ConnectToDatabaseForm();
        NeuronNet net;
        DataTable loadedExamples = new DataTable();
        DataTable currentParameters = new DataTable();
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
            //loadedExamples = ConnectionForm.ConnectToDatabase.SelectQuery
            string query =
                ("SELECT InputsCount, "+ 
	            "OutputsCount, " +
	            "Subject, " +
	            "NeuronNetTypeID as NetTypeID, " +
	            "ProblemTypeID AS TaskTypeID, " +
	            "ControlID FROM NeuronNet");
            //string query = "select * from NeuronNet";
            databaseSQLite.OpenConnection();
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
            SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter(myCommand);
            try
            {
                myDataAdapter.Fill(loadedExamples);
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                databaseSQLite.CloseConnection();
            }
            currentRow = loadedExamples.Rows[currentIndex = 0];
            //ShowCurrentRow();
            databaseSQLite.CloseConnection();
        }

        public void ShowCurrentRow()
        {
            NetTypeBox.Items.Clear();
            TaskBox.Items.Clear();
            InputsCount.Text = currentRow["InputsCount"].ToString();
            OutputsCount.Text = currentRow["OutputsCount"].ToString();
            StudyPairsCount.Text = net.StudyPairs.Count.ToString();
            TaskDescription.Text = currentRow["Subject"].ToString();
            NetTypeBox.Items.Add(Convert.ToInt32(currentRow["NetTypeID"]));
            TaskBox.Items.Add(Convert.ToInt32(currentRow["TaskTypeID"]));
        }

        //public bool ConnectToDatabase()
        //{
        //    if (ConnectionForm.Connected) return true;

        //    ConnectionForm.ShowDialog();

        //    if (ConnectionForm.Connected)
        //    {
        //        LoadTasks();
        //        LoadNetTypes();
        //    }

        //    return ConnectionForm.Connected;
        //}

        private void LoadSaveTasks_Load(object sender, EventArgs e)
        {
            LoadTasks();
            LoadNetTypes();
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
            string query = "select * from TaskType";
            databaseSQLite.OpenConnection();
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
            SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter(myCommand);
            TaskBox.Items.Clear();
            try
            {
                DataTable dataTable = new DataTable();
                myDataAdapter.Fill(dataTable);

                for (int i = 0; i< dataTable.Rows.Count; i++)
                {
                    TaskBox.Items.Add(dataTable.Rows[i].ItemArray[0].ToString());
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                databaseSQLite.CloseConnection();
            }
            //TaskBox.Table = ConnectionForm.ConnectToDatabase.SelectQuery("select * from TaskType");
            //TaskBox.FillControl(1, 0);
        }

        public void LoadNetTypes()
        {
            string query = "select * from TaskType";
            databaseSQLite.OpenConnection();
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
            SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter(myCommand);
            NetTypeBox.Items.Clear();
            try
            {
                DataTable dataTable = new DataTable();
                myDataAdapter.Fill(dataTable);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    NetTypeBox.Items.Add(dataTable.Rows[i].ItemArray[0].ToString());
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                databaseSQLite.CloseConnection();
            }
            //NetTypeBox.Table = ConnectionForm.ConnectToDatabase.SelectQuery("select * from NetType");
            //NetTypeBox.FillControl(1, 0);            
        }

        //Создание нового экземпляра
        public void AddNewTask()
        {
            databaseSQLite.OpenConnection();
            string query = "select max(id) from NeuronNet";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
            int newNetID = Convert.ToInt32(myCommand.ExecuteScalar());
            newNetID = newNetID + 1;
            query = "insert into NeuronNet(InputsCount, OutputsCount, NeuronNetTypeID, ProblemTypeID, Subject , ControlID, id)" +
                " VALUES (@inputsCount,@outputsCount,@neuronNetTypeID,@problemTypeID,@subject,@controlID,@id)";
            myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
            myCommand.Parameters.AddWithValue("@inputsCount", InputsCount.Text);
            myCommand.Parameters.AddWithValue("@outputsCount", OutputsCount.Text);
            myCommand.Parameters.AddWithValue("@neuronNetTypeID", NetTypeBox.SelectedIndex);
            myCommand.Parameters.AddWithValue("@problemTypeID", TaskBox.SelectedIndex);
            myCommand.Parameters.AddWithValue("@subject", TaskDescription.Text);
            myCommand.Parameters.AddWithValue("@controlID", TaskBox.SelectedIndex);
            myCommand.Parameters.AddWithValue("@id", newNetID);
            var result = myCommand.ExecuteNonQuery();    
            //myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
            //ConnectionForm.ConnectToDatabase.InsertQuery(string.Format("insert into NeuronNet(InputsCount, OutputsCount, NeuronNetTypeID, ProblemTypeID, Subject , ControlID) values({0},{1},{2},{3},'{4}',{5})", InputsCount.Text , OutputsCount.Text , NetTypeBox.DataIndex , TaskBox.DataIndex , TaskDescription.Text, TaskBox.SelectedIndex ));
            //int newNetID = (int)ConnectionForm.ConnectToDatabase.SelectQuery("select max(id) from NeuronNet").Rows[0].ItemArray[0];
            int newStudyPairID;
            try
            {
                for (int i = 0; i < net.StudyPairs.Count; i++)
                {
                    query = "select max(id) from StudyPair";
                    myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
                    newStudyPairID = Convert.ToInt32(myCommand.ExecuteScalar());
                    newStudyPairID = newStudyPairID + 1;

                    query = "insert into StudyPair(Name , NeuronNetID,id) values(@name,@neuronNetID,@id)";
                    myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
                    myCommand.Parameters.AddWithValue("@name", net.StudyPairs[i].name);
                    myCommand.Parameters.AddWithValue("@neuronNetID", newNetID);
                    myCommand.Parameters.AddWithValue("@id", newStudyPairID);
                    result = myCommand.ExecuteNonQuery();
                    //ConnectionForm.ConnectToDatabase.InsertQuery(string.Format("insert into StudyPair(c , NeuronNetID) values('{0}',{1})", net.StudyPairs[i].name, newNetID));
                    //newStudyPairID = (int)ConnectionForm.ConnectToDatabase.SelectQuery("select max(id) from StudyPair").Rows[0].ItemArray[0];

                    for (int input = 0; input < net.StudyPairs[i].inputs.Count; input++)
                    {
                        query = "insert into Inputs(InputValue , StudyPairID) values(@inputValue,@studyPairID)";
                        myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
                        myCommand.Parameters.AddWithValue("@inputValue", net.StudyPairs[i].inputs[input].ToString().Replace(',', '.'));
                        myCommand.Parameters.AddWithValue("@studyPairID", newStudyPairID);
                        result = myCommand.ExecuteNonQuery();
                        //ConnectionForm.ConnectToDatabase.InsertQuery(string.Format("insert into Inputs(InputValue , StudyPairID) values({0},{1})", net.StudyPairs[i].inputs[input].ToString().Replace(',', '.'), newStudyPairID));
                    }

                    for (int output = 0; output < net.StudyPairs[i].quits.Count; output++)
                    {
                        query = "insert into Outputs(OutputValue , StudyPairID) values(@outputValue,@studyPairID)";
                        myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
                        myCommand.Parameters.AddWithValue("@outputValue", net.StudyPairs[i].quits[output].ToString().Replace(',', '.'));
                        myCommand.Parameters.AddWithValue("@studyPairID", newStudyPairID);
                        result = myCommand.ExecuteNonQuery();
                        //ConnectionForm.ConnectToDatabase.InsertQuery(string.Format("insert into Outputs(OutputValue , StudyPairID) values({0},{1})", net.StudyPairs[i].quits[output].ToString().Replace(',', '.'), newStudyPairID));
                    }
                }

                for (int input = 0; input < net.InputsCount; input++)
                {
                    query = "insert into Parameter(Name , NeuronNetID) values(@name,@neuronNetID)";
                    myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
                    myCommand.Parameters.AddWithValue("@name", net.Inputs[input].Name);
                    myCommand.Parameters.AddWithValue("@neuronNetID", newNetID);
                    result = myCommand.ExecuteNonQuery();
                    //ConnectionForm.ConnectToDatabase.InsertQuery(string.Format("insert into Parameter(Name , NeuronNetID) values('{0}',{1})", net.Inputs[input].Name, newNetID));

                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                databaseSQLite.CloseConnection();
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
            string query = "select id, Name from StudyPair where NeuronNetID = " + NetID;
            databaseSQLite.OpenConnection();
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
            SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter(myCommand);
            try
            {
                myDataAdapter.Fill(currentParameters);
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                databaseSQLite.CloseConnection();
            }
            //currentParameters = ConnectionForm.ConnectToDatabase.SelectQuery(string.Format("SELECT Parameter.Name FROM NeuronNet INNER JOIN Parameter ON NeuronNet.id = Parameter.NeuronNetID WHERE NeuronNet.id = {0}", NetID));

            for (int i = 0; i < currentParameters.Rows.Count; i++)
            {
                net.Inputs[i].Name = currentParameters.Rows[i].ItemArray[0].ToString();
            }                
        }

        public void BuildNet()
        {
            DataTable valuesTable;
           
            switch (Convert.ToInt32(currentRow["ControlID"]))
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

            net.InputsCount = Convert.ToInt32(currentRow["InputsCount"]);
            net.OutputsCount = Convert.ToInt32(currentRow["OutputsCount"]);

            //Ошибка
            string query = "select id, Name from StudyPair where NeuronNetID = " + Convert.ToInt32(currentRow["id"]);
            databaseSQLite.OpenConnection();
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
            SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter(myCommand);
            DataTable studyPairTable = null;
            try
            {
                myDataAdapter.Fill(studyPairTable);
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                databaseSQLite.CloseConnection();
            }
            //DataTable studyPairTable = ConnectionForm.ConnectToDatabase.SelectQuery(string.Format("select id, Name from StudyPair where NeuronNetID = {0}", (int)currentRow["id"]));
            net.StudyPairs.Clear();

            databaseSQLite.OpenConnection();
            foreach (DataRow row in studyPairTable.Rows)
            {
                net.StudyPairs.Add(new StudyPair());
                net.StudyPairs.Last().name = row["Name"].ToString();
                query = "select InputValue from Inputs where StudyPairID = " + Convert.ToInt32(row["id"]);
                myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
                myDataAdapter = new SQLiteDataAdapter(myCommand);
                valuesTable = null;
                try
                {
                    myDataAdapter.Fill(valuesTable);
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                //valuesTable = ConnectionForm.ConnectToDatabase.SelectQuery(string.Format("select InputValue from Inputs where StudyPairID = {0}", (int)row["id"]));

                foreach (DataRow r in valuesTable.Rows)
                {
                    net.StudyPairs.Last().inputs.Add(float.Parse(r.ItemArray[0].ToString().Replace('.',',')));
                }

                query = "select OutputValue from Outputs where StudyPairID = " + Convert.ToInt32(row["id"]);
                myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
                myDataAdapter = new SQLiteDataAdapter(myCommand);
                //valuesTable = ConnectionForm.ConnectToDatabase.SelectQuery(string.Format("select OutputValue from Outputs where StudyPairID = {0}", (int)row["id"]));
                try
                {
                    myDataAdapter.Fill(valuesTable);
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

                foreach (DataRow r in valuesTable.Rows)
                {
                    net.StudyPairs.Last().quits.Add(float.Parse(r.ItemArray[0].ToString().Replace('.', ',')));
                }
            }
            net.StudyPairsLoaded = true;
            databaseSQLite.CloseConnection();
        }

        //Кнопка загрузить 
        private void LoadCurrentExample(object sender, EventArgs e)
        {
            BuildNet();
            LoadCurrentRowData();
        }

        private void TaskBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
