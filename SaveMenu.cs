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
    public partial class SaveMenu : Form
    {
        Database databaseSQLite = new Database();
        public string fileName;
        public int indexSave;

        public SaveMenu()
        {
            InitializeComponent();
        }

        //Сохранить
        private void button2_Click(object sender, EventArgs e)
        {
            fileName = comboBox1.Text;
            this.Close();
        }

        private void SaveMenu_Load(object sender, EventArgs e)
        {
            if (indexSave == 0)
            {
                SaveFiles();
            }
            else if (indexSave == 1)
            {
                SaveGraphs();
            }
            else
            {
                MessageBox.Show("Не инициализирована таблица", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void SaveFiles()
        {
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
            databaseSQLite.CloseConnection();
        }

        private void SaveGraphs()
        {
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
            databaseSQLite.CloseConnection();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                button2.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
            }
        }
    }
}
