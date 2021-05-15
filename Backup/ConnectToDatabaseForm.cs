using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Database
{
    public partial class ConnectToDatabaseForm : Form
    {
        ConnectToDatabase connectToDatabase;
        public bool Connected = false;

        public ConnectToDatabase ConnectToDatabase
        {
            get 
            {
                return connectToDatabase;
            }
        }

        public ConnectToDatabaseForm()
        {
            InitializeComponent();
        }

        private void Connect(object sender, EventArgs e)
        {
            connectToDatabase = new ConnectToDatabase();
            connectToDatabase.DatabaseName = DatabaseText.Text;
            connectToDatabase.Server = ServerText.Text;
            connectToDatabase.UserID = UserIDText.Text;

            if (connectToDatabase.Connect(true))
            {                
                Connected = true;
                Hide();
            }
            else MessageBox.Show("Подключение НЕ выполнено!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ConnectToDatabaseFile(object sender, EventArgs e)
        {
            connectToDatabase = new ConnectToDatabase();
            connectToDatabase.DatabaseName = DatabaseText.Text;
           
            if (connectToDatabase.Connect(false))
            {
                Connected = true;
                Hide();
            }
            else MessageBox.Show("Подключение НЕ выполнено!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}