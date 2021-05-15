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

        public LoadMenu()
        {
            InitializeComponent();
        }

        private void LoadMenu_Load(object sender, EventArgs e)
        {
            LoadNames();
        }

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

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "DELETE from SaveFiles where id=" + "\"" + comboBox1.SelectedIndex + "\"";
            databaseSQLite.OpenConnection();
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
            int result = myCommand.ExecuteNonQuery();
            button3.Enabled = false;
            button2.Enabled = false;
            databaseSQLite.CloseConnection();
            LoadNames();
        }

        private void LoadNames()
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

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM SaveFiles; REINDEX SaveFiles; VACUUM;";
            databaseSQLite.OpenConnection();
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseSQLite.myConnection);
            int result = myCommand.ExecuteNonQuery();
            LoadNames();
        }
    }
}
