namespace Theseus
{
    public class Point
    {
        public static Point Create(int x, int y)
        {
            return new Point(x, y);
        }

        protected Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }

        public int Y { get; }
    }
}
