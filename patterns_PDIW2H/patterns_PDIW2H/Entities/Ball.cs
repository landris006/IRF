using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace patterns_PDIW2H.Entities
{
    class Ball : Label
    {
        public Ball()
        {
            AutoSize = false;
            Height = 50;
            Width = 50;
            Paint += HandlePaint;
        }

        private void HandlePaint(object sender, PaintEventArgs e)
        {
            DrawImage(e.Graphics);
        }

        protected void DrawImage(Graphics graphics)
        {
            graphics.FillEllipse(new SolidBrush(Color.Blue), 0, 0, Width, Height);
        }

        public void MoveBall()
        {
            Left++;
        }
    }
}
