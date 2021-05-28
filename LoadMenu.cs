using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using Database;

namespace Neuron
{
    public partial class LoadMenu : Form
    {
        Database databaseSQLite = new Database();
        public string fileName;
        public int indexSave;

        public LoadMenu()
        {
            InitializeComponent();
        }

        private void LoadMenu_Load(object sender, EventArgs e)
        {
            if (indexSave == 0)
                LoadFiles();
            else if (indexSave == 1)
                LoadGraphs();
            else if (indexSave == 2)
                LoadFilesClassification();
            else if (indexSave == 3)
                LoadLinearSystemTask();
            else
            {
                MessageBox.Show("Не инициализирована таблица", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        private void LoadLinearSystemTask()
        {
            comboBox1.Items.Clear();
            string query = "select * from SaveLinearSystemTask";
            databaseSQLite.OpenConnection();
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
            SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter(myCommand);
            DataTable dataTable = new DataTable();
            myDataAdapter.Fill(dataTable);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                comboBox1.Items.Add(dataRow["Name"].ToString());
            }
        }

        private void LoadFilesClassification()
        {
            comboBox1.Items.Clear();
            string query = "select * from SaveFilesClassification";
            databaseSQLite.OpenConnection();
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
            SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter(myCommand);
            DataTable dataTable = new DataTable();
            myDataAdapter.Fill(dataTable);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                comboBox1.Items.Add(dataRow["Name"].ToString());
            }
        }

        private void LoadFiles()
        {
            comboBox1.Items.Clear();
            string query = "select * from SaveFiles";
            databaseSQLite.OpenConnection();
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
            SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter(myCommand);
            DataTable dataTable = new DataTable();
            myDataAdapter.Fill(dataTable);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                comboBox1.Items.Add(dataRow["Name"].ToString());
            }
        }
        private void LoadGraphs()
        {
            comboBox1.Items.Clear();
            string query = "select * from SaveGraphs";
            databaseSQLite.OpenConnection();
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
            SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter(myCommand);
            DataTable dataTable = new DataTable();
            myDataAdapter.Fill(dataTable);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                comboBox1.Items.Add(dataRow["Name"].ToString());
            }
        }

        //Очистка всех файлов
        private void button1_Click(object sender, EventArgs e)
        {
            if (indexSave == 0)
            {
                string query = "DELETE FROM SaveFiles; REINDEX SaveFiles; VACUUM;";
                databaseSQLite.OpenConnection();
                SQLiteCommand myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
                int result = myCommand.ExecuteNonQuery();
                LoadFiles();
            }
            else if (indexSave == 1)
            {
                string query = "DELETE FROM SaveGraphs; REINDEX savegraphs; VACUUM;";
                databaseSQLite.OpenConnection();
                SQLiteCommand myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
                int result = myCommand.ExecuteNonQuery();
                LoadGraphs();
            }
            else if (indexSave == 2)
            {
                string query = "DELETE FROM SaveFilesClassification; REINDEX savegraphs; VACUUM;";
                databaseSQLite.OpenConnection();
                SQLiteCommand myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
                int result = myCommand.ExecuteNonQuery();
                LoadFilesClassification();
            }
            else if (indexSave == 3)
            {
                string query = "DELETE FROM SaveLinearSystemTask; REINDEX savegraphs; VACUUM;";
                databaseSQLite.OpenConnection();
                SQLiteCommand myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
                int result = myCommand.ExecuteNonQuery();
                LoadLinearSystemTask();
            }
        }

        //Очистка определенного файла
        private void button3_Click(object sender, EventArgs e)
        {
            if (indexSave == 0)
            {
                string query = "DELETE from SaveFiles where id=" + "\"" + comboBox1.SelectedIndex + "\"";
                databaseSQLite.OpenConnection();
                SQLiteCommand myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
                int result = myCommand.ExecuteNonQuery();
                button3.Enabled = false;
                button2.Enabled = false;
                databaseSQLite.CloseConnection();
                LoadFiles();
            }
            else if (indexSave == 1)
            {
                string query = "DELETE from SaveGraphs where id=" + "\"" + comboBox1.SelectedIndex + "\"";
                databaseSQLite.OpenConnection();
                SQLiteCommand myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
                int result = myCommand.ExecuteNonQuery();
                button3.Enabled = false;
                button2.Enabled = false;
                databaseSQLite.CloseConnection();
                LoadGraphs();
            }
            else if (indexSave == 2)
            {
                string query = "DELETE from SaveFilesClassification where id=" + "\"" + comboBox1.SelectedIndex + "\"";
                databaseSQLite.OpenConnection();
                SQLiteCommand myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
                int result = myCommand.ExecuteNonQuery();
                button3.Enabled = false;
                button2.Enabled = false;
                databaseSQLite.CloseConnection();
                LoadFilesClassification();
            }
            else if (indexSave == 3)
            {
                string query = "DELETE from SaveLinearSystemTask where id=" + "\"" + comboBox1.SelectedIndex + "\"";
                databaseSQLite.OpenConnection();
                SQLiteCommand myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
                int result = myCommand.ExecuteNonQuery();
                button3.Enabled = false;
                button2.Enabled = false;
                databaseSQLite.CloseConnection();
                LoadLinearSystemTask();
            }

        }

        //Загрузить
        private void button2_Click(object sender, EventArgs e)
        {
            fileName = comboBox1.Text;
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button3.Enabled = true;
        }
    }
}
