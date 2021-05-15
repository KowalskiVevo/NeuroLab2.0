using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;

namespace Database
{
    public class ConnectToDatabase
    {
        public string UserID;
        public string Server;
        public string DatabaseName;

        SqlConnection connection;
        SqlCommand command = new SqlCommand();
        SqlDataAdapter adapter;
        List<string> tables = new List<string>();
        DataSet dataSet;

        OleDbConnection oleConnection;
        OleDbCommand oleCommand;
        OleDbDataAdapter oleAdapter;

        bool IsSqlServerConnected = true;

        public ConnectToDatabase()
        { 
        
        }

        public void CloseConnection()
        {
            if (IsSqlServerConnected) connection.Close();
            else oleConnection.Close();
        }

        public bool Connect(bool isSqlServer)
        {
            try
            {
                if (IsSqlServerConnected = isSqlServer)
                {
                    connection = new SqlConnection(string.Format("user ID={0};server={1};database={2};Trusted_Connection=yes;connection timeout=3", UserID, Server, DatabaseName));
                    connection.Open();               
                }
                else
                {
                    oleConnection = new OleDbConnection(String.Format("provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", DatabaseName));
                    oleConnection.Open();
                }
            }
            catch (Exception)
            {
                return false;
            }

            InitializeDataAdapter();

            return true;
        }

        public void InitializeDataAdapter()
        {
            adapter = new SqlDataAdapter();
            oleAdapter = new OleDbDataAdapter();
        }

        public void FillTables()
        {
            dataSet = new DataSet();

            for (int i = 0; i < tables.Count; i++)
            {
                DataTable table = new DataTable();
                adapter.SelectCommand.CommandText = string.Format("select * from [{0}];", tables[i]);
                adapter.Fill(table);
                dataSet.Tables.Add(table);
            }
        }

        public DataTable SelectQuery(string text)
        {
            DataTable table = new DataTable();

            if (IsSqlServerConnected)
            {
                adapter.SelectCommand = new SqlCommand(text, connection);
                adapter.Fill(table);
            }
            else
            {
                oleCommand = oleConnection.CreateCommand();
                oleCommand.CommandText = text;
                oleAdapter.SelectCommand = oleCommand;
                oleAdapter.Fill(table);
            }
            return table;
        }

        public int InsertQuery(string queryText)
        {
            int RowsAffected = 0;
            if (IsSqlServerConnected)
            {
                command = connection.CreateCommand();
                command.CommandText = queryText;
                adapter.InsertCommand = command;

                RowsAffected = command.ExecuteNonQuery();
            }
            else
            {
                oleCommand = oleConnection.CreateCommand();
                oleCommand.CommandText = queryText;
                oleAdapter.InsertCommand = oleCommand;

                RowsAffected = oleCommand.ExecuteNonQuery();
            }
            return RowsAffected;
        }
    }
}
