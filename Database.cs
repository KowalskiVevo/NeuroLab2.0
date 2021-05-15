using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;

namespace Neuron
{
    class Database
    {
        public SQLiteConnection myConnection;

        public Database()
        {
            myConnection = new SQLiteConnection("Data Source=./neyrolab.db");
            if (!File.Exists("neyrolab.db"))
            {
                SQLiteConnection.CreateFile("neyrolab.db");
                System.Console.WriteLine("Create file neyrolab.db");
            }
            else
            {
                System.Console.WriteLine("File was created neyrolab.db");
            }
        }

        public void OpenConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Я ВКЛЮЧИЛСЯ");
                myConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Closed)
            {
                myConnection.Close();
            }
        }
    }
}
