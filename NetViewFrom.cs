using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Neuron
{
    public partial class NetViewForm : Form
    {
        public NetViewForm(Bitmap bitmap)
        {
            InitializeComponent();
            pictureBox1.Image = bitmap;
        }


    }
}
