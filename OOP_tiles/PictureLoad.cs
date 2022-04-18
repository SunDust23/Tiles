using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_tiles
{
    public class PictureLoad
    {
        private Size size;
        private Point pos;

        private Color color;

        private byte red;
        private byte blue;
        private byte green;
        private byte alpha;

        public Size Size { get => size; set => size = value; }
        public Color Color { get => color; set => color = value; }
        public Point Pos { get => pos; set => pos = value; }
        public byte Red { get => red; set => red = value; }
        public byte Blue { get => blue; set => blue = value; }
        public byte Green { get => green; set => green = value; }
        public byte Alpha { get => alpha; set => alpha = value; }

        public void getColor(Color color)
        {
            Alpha = color.A;
            Red = color.R;
            Blue = color.B;
            Green = color.G;
        }

        public void setColor()
        {
            color = Color.FromArgb(alpha, red, green, blue);
        }
    }
}
