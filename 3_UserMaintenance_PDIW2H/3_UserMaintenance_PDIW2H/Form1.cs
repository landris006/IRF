using _3_UserMaintenance_PDIW2H.Entities;
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
        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();
            label1.Text = Resource.LastName;
            label2.Text = Resource.FirstName;
            button1.Text = Resource.Add;

            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "")
            {
                return;
            }

            var newUser = new User
            {
                FirstName = textBox2.Text,
                LastName = textBox3.Text,
            };

            users.Add(newUser);
        }
    }
}
