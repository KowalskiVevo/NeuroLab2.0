using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace Neuron
{
    public class DataComboBox : ComboBox
    {
        DataTable table;
        int currentIndex, shownColumnIndex, indexColumnIndex;
        string shownColumnName, indexColumnName;
        
        public DataComboBox()
        { 
        
        }

        public int DataIndex
        {
            get { return currentIndex; }
            set 
            {
                Text = (string)table.Select(string.Format("{0} = {1}", indexColumnName, value ))[0].ItemArray[shownColumnIndex]; 
            }
        }

        public DataTable Table
        {
            get 
            {
                return table;
            }

            set
            {
                table = value;
            }
        }

        public void FillControl(int indexColumnNumber, int shownColumnNumber)
        {
            Items.Clear();
            foreach (DataRow row in table.Rows) Items.Add(row[shownColumnNumber]);

            shownColumnName = table.Columns[shownColumnNumber].ColumnName;
            indexColumnName = table.Columns[indexColumnNumber].ColumnName;
            shownColumnIndex = shownColumnNumber;
            indexColumnIndex = indexColumnNumber;
            SelectedIndex = 0;
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            currentIndex = (int)table.Select(string.Format("{0} like '{1}'", shownColumnName , Text))[0].ItemArray[indexColumnIndex];            
            base.OnSelectedIndexChanged(e);
        }

    }
}
