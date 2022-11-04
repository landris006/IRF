using patterns_PDIW2H.Abstractions;
using patterns_PDIW2H.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace patterns_PDIW2H
{
    public partial class Form1 : Form
    {
        private List<Toy> _toys = new List<Toy>();
        private IToyFactory _factory;

        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }


        public Form1()
        {
            InitializeComponent();
            Factory = new CarFactory();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Toy newBall = Factory.CreateNew();
            newBall.Left = -newBall.Width;

            mainPanel.Controls.Add(newBall);
            _toys.Add(newBall);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
        int maxPosition = 0;

            foreach (Toy toy in _toys)
            {
                toy.MoveToy();

                if (toy.Left > maxPosition)
                {
                    maxPosition = toy.Left;
                }
            }

            if (maxPosition > 1000)
            {
                Toy toyToDelete = _toys.FirstOrDefault();

                if (toyToDelete == null)
                {
                    return;
                }

                _toys.Remove(toyToDelete);
                mainPanel.Controls.Remove(toyToDelete);
            }
        }
    }
}