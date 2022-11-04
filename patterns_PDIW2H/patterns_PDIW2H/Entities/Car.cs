using patterns_PDIW2H.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_PDIW2H.Entities
{
    public class Car : Toy
    {
        protected override void DrawImage(Graphics graphics)
        {
            Image image = Image.FromFile("Images/car.png");
            graphics.DrawImage(image, new Rectangle(0, 0, Width, Height));
        }
    }
}
