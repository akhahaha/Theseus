using System;
using System.Drawing;

namespace Theseus
{
    public class GraphicalMazePixel : Point
    {
        public static GraphicalMazePixel Create(int x, int y, Color color)
        {
            return new GraphicalMazePixel(x, y, color);
        }

        protected GraphicalMazePixel(int x, int y, Color color) : base(x, y)
        {
            Color = color;
        }

        public Color Color { get; }

        public bool IsColor(Color color)
        {
            return Color.ToArgb() == color.ToArgb();
        }
    }
}
