using _3_UserMaintenance_PDIW2H.Entities;
using _3_UserMaintenance_PDIW2H.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace _3_UserMaintenance_PDIW2H
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();
            label1.Text = Resource.FullName;
            button1.Text = Resource.Add;
            button2.Text = Resource.WriteToFile;
            button3.Text = Resource.DeleteSelected;

            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                return;
            }

            var newUser = new User
            {
                FullName = textBox2.Text,
            };

            users.Add(newUser);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                DefaultExt = "csv",
                Filter = "Comma separated values (*.csv) | *.csv",
            };

            if (sfd.ShowDialog() == DialogResult.Cancel || sfd.FileName == "")
            {
                return;
            }

            StreamWriter sw = new StreamWriter(sfd.FileName);

            foreach (User user in users)
            {
                sw.WriteLine($"{user.ID};{user.FullName}");
                sw.WriteLine();
            }

            sw.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedValue == null)
            {
                return;
            }

            Guid selectedUserId = ((User)listBox1.SelectedItem).ID;
            Console.WriteLine(listBox1.SelectedValue);
            Console.WriteLine(((User)listBox1.SelectedItem).ID);

            users = new BindingList<User>((from user in users
                                           where !user.ID.Equals(selectedUserId)
                                           select user).ToList());
        }
    }
}
