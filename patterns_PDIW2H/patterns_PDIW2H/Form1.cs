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
        private List<Ball> _balls = new List<Ball>();
        private BallFactory _factory;

        public BallFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }


        public Form1()
        {
            InitializeComponent();
            Factory = new BallFactory();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Ball newBall = Factory.CreateNew();
            newBall.Left = -newBall.Width;

            mainPanel.Controls.Add(newBall);
            _balls.Add(newBall);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
        int maxPosition = 0;

            foreach (Ball ball in _balls)
            {
                ball.MoveBall();

                if (ball.Left > maxPosition)
                {
                    maxPosition = ball.Left;
                }
            }

            if (maxPosition > 1000)
            {
                Ball ballToDelete = _balls.FirstOrDefault();

                if (ballToDelete == null)
                {
                    return;
                }

                _balls.Remove(ballToDelete);
                mainPanel.Controls.Remove(ballToDelete);
            }
        }
    }
}