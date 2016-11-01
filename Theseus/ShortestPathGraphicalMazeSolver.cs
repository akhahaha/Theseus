using System.Collections.Generic;
using System.Drawing;

namespace Theseus
{
    /// <summary>
    /// Graphical maze solver implementing a breadth-first search to solve the maze.
    /// Guaranteed to return the shortest solution if it exists.
    /// </summary>
    public class ShortestPathGraphicalMazeSolver : IGraphicalMazeSolver
    {
        public static ShortestPathGraphicalMazeSolver Create()
        {
            return new ShortestPathGraphicalMazeSolver();
        }

        protected ShortestPathGraphicalMazeSolver()
        {
        }

        public Image GenerateSolution(GraphicalMaze maze)
        {
            // Find start and end pixels
            Point startPoint = null;
            Point endPoint = null;
            for (var y = 0; y < maze.Height; y++)
            {
                for (var x = 0; x < maze.Width; x++)
                {
                    var pixel = maze.GetPixel(x, y);
                    if (pixel.IsColor(maze.StartColor))
                    {
                        startPoint = pixel;
                    }
                    else if (pixel.IsColor(maze.FinishColor))
                    {
                        endPoint = pixel;
                    }
                }
            }

            if (startPoint == null || endPoint == null)
            {
                return null; // No start or end points found
            }

            // Run best first search
            var path = BestFirstSearch(maze, startPoint, endPoint);

            if (path == null) return null; // No solution found

            var solutionImage = new Bitmap(maze.MazeImage);
            foreach (var point in path) solutionImage.SetPixel(point.X, point.Y, maze.SolutionColor);

            solutionImage.SetPixel(startPoint.X, startPoint.Y, maze.StartColor); // Reapply start pixel color
            return solutionImage;
        }

        private static List<Point> BestFirstSearch(GraphicalMaze maze, Point startPoint, Point endPoint)
        {
            var visited = new HashSet<Point>();

            // Set of pending nodes to be evaluated, sorted by estimated cost from start to end through the point
            var discovered = new Queue<Point>();
            discovered.Enqueue(startPoint);
            var costMap = new Dictionary<Point, int> {{startPoint, 0}};
            var accessMap = new Dictionary<Point, Point>();

            while (discovered.Count > 0)
            {
                var currentPoint = discovered.Dequeue();
                if (currentPoint.Equals(endPoint))
                {
                    return ReconstructPath(accessMap, currentPoint);
                }

                visited.Add(currentPoint);
                var cost = costMap[Point.Create(currentPoint.X, currentPoint.Y)] + 1;
                foreach (var neighbor in maze.GetNeighbors(currentPoint))
                {
                    if (neighbor.IsColor(maze.WallColor) || visited.Contains(neighbor)) continue;

                    if (costMap.ContainsKey(neighbor) && cost >= costMap[neighbor]) continue; // Not a better path

                    // Record better path
                    accessMap[neighbor] = currentPoint;
                    costMap[neighbor] = cost;
                    discovered.Enqueue(neighbor);
                }
            }

            // No solution found
            return null;
        }

        private static List<Point> ReconstructPath(IReadOnlyDictionary<Point, Point> accessMap, Point endPoint)
        {
            var path = new List<Point>();
            var currentPoint = endPoint;
            while (accessMap.ContainsKey(currentPoint))
            {
                currentPoint = accessMap[currentPoint];
                path.Add(currentPoint);
            }

            return path;
        }
    }
}
