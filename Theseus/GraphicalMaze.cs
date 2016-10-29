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

        public GraphicalMazePixel GetPixel(int x, int y)
        {
            if (0 <= x && x < MazeImage.Width && 0 <= y && y < MazeImage.Height)
            {
                return GraphicalMazePixel.Create(x, y, MazeImage.GetPixel(x, y));
            }

            return null;
        }

        public GraphicalMazePixel GetPixel(Point point)
        {
            return GetPixel(point.X, point.Y);
        }

        public GraphicalMazePixel GetPixel(Point point, Direction direction)
        {
            switch (direction)
            {
                case Direction.Top:
                    return GetPixelTop(point);
                case Direction.TopRight:
                    return GetPixelTopRight(point);
                case Direction.Right:
                    return GetPixelRight(point);
                case Direction.BottomRight:
                    return GetPixelBottomRight(point);
                case Direction.Bottom:
                    return GetPixelBottom(point);
                case Direction.BottomLeft:
                    return GetPixelBottomLeft(point);
                case Direction.Left:
                    return GetPixelLeft(point);
                case Direction.TopLeft:
                    return GetPixelTopLeft(point);
                default:
                    return null;
            }
        }

        public GraphicalMazePixel GetPixelTop(Point point)
        {
            return GetPixel(point.X, point.Y - 1);
        }

        public GraphicalMazePixel GetPixelTopRight(Point point)
        {
            return GetPixel(point.X + 1, point.Y - 1);
        }

        public GraphicalMazePixel GetPixelRight(Point point)
        {
            return GetPixel(point.X + 1, point.Y);
        }

        public GraphicalMazePixel GetPixelBottomRight(Point point)
        {
            return GetPixel(point.X + 1, point.Y + 1);
        }

        public GraphicalMazePixel GetPixelBottom(Point point)
        {
            return GetPixel(point.X, point.Y + 1);
        }

        public GraphicalMazePixel GetPixelBottomLeft(Point point)
        {
            return GetPixel(point.X - 1, point.Y + 1);
        }

        public GraphicalMazePixel GetPixelLeft(Point point)
        {
            return GetPixel(point.X - 1, point.Y);
        }

        public GraphicalMazePixel GetPixelTopLeft(Point point)
        {
            return GetPixel(point.X - 1, point.Y - 1);
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
