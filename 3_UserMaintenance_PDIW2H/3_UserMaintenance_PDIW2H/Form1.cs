using _3_UserMaintenance_PDIW2H.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3_UserMaintenance_PDIW2H
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Text = Resource.LastName;
            label2.Text = Resource.FirstName;
            button1.Text = Resource.Add;
        }
    }
}
