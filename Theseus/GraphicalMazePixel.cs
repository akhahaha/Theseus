using System.Drawing;

namespace Theseus
{
    public struct GraphicalMazePixel
    {
        public static GraphicalMazePixel Create(int x, int y, Color color)
        {
            return new GraphicalMazePixel(x, y, color);
        }

        private GraphicalMazePixel(int x, int y, Color color)
        {
            X = x;
            Y = y;
            Color = color;
        }

        public int X { get; }

        public int Y { get; }

        public Color Color { get; }
    }
}
