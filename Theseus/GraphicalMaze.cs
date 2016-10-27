using System.Drawing;

namespace Theseus
{
    public class GraphicalMaze
    {
        public static GraphicalMaze Create(Image mazeImage, Color wallColor, Color startColor, Color finishColor,
            Color solutionColor)
        {
            return new GraphicalMaze(new Bitmap(mazeImage), wallColor, startColor, finishColor, solutionColor);
        }

        protected GraphicalMaze(Bitmap mazeImage, Color wallColor, Color startColor, Color finishColor,
            Color solutionColor)
        {
            MazeImage = mazeImage;
            WallColor = wallColor;
            StartColor = startColor;
            FinishColor = finishColor;
            SolutionColor = solutionColor;
        }

        public Bitmap MazeImage { get; }

        public int Height => MazeImage.Height;

        public int Width => MazeImage.Width;

        public GraphicalMazePixel? GetPixel(int x, int y)
        {
            if (0 <= x && x < MazeImage.Width && 0 <= y && y < MazeImage.Height)
            {
                return GraphicalMazePixel.Create(x, y, MazeImage.GetPixel(x, y));
            }

            return null;
        }

        public GraphicalMazePixel? GetPixelTop(GraphicalMazePixel pixel)
        {
            return GetPixel(pixel.X, pixel.Y - 1);
        }

        public GraphicalMazePixel? GetPixelTopRight(GraphicalMazePixel pixel)
        {
            return GetPixel(pixel.X + 1, pixel.Y - 1);
        }

        public GraphicalMazePixel? GetPixelRight(GraphicalMazePixel pixel)
        {
            return GetPixel(pixel.X + 1, pixel.Y);
        }

        public GraphicalMazePixel? GetPixelBottomRight(GraphicalMazePixel pixel)
        {
            return GetPixel(pixel.X + 1, pixel.Y + 1);
        }

        public GraphicalMazePixel? GetPixelBottom(GraphicalMazePixel pixel)
        {
            return GetPixel(pixel.X, pixel.Y + 1);
        }

        public GraphicalMazePixel? GetPixelBottomLeft(GraphicalMazePixel pixel)
        {
            return GetPixel(pixel.X - 1, pixel.Y + 1);
        }

        public GraphicalMazePixel? GetPixelLeft(GraphicalMazePixel pixel)
        {
            return GetPixel(pixel.X - 1, pixel.Y);
        }

        public GraphicalMazePixel? GetPixelTopLeft(GraphicalMazePixel pixel)
        {
            return GetPixel(pixel.X - 1, pixel.Y - 1);
        }

        public void SetPixel(int x, int y, Color color)
        {
            if (0 <= x && x < MazeImage.Width && 0 <= y && y < MazeImage.Height)
            {
                MazeImage.SetPixel(x, y, color);
            }
        }

        public void SetPixel(Point point, Color color)
        {
            SetPixel(point.X, point.Y, color);
        }

        public Color WallColor { get; }

        public Color StartColor { get; }

        public Color FinishColor { get; }

        public Color SolutionColor { get; }
    }
}
