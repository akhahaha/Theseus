using System.Drawing;

namespace Theseus
{
    public class WallFollowerGraphicalMazeSolver : IGraphicalMazeSolver
    {
        public static WallFollowerGraphicalMazeSolver Create()
        {
            return new WallFollowerGraphicalMazeSolver();
        }

        private WallFollowerGraphicalMazeSolver()
        {
        }

        Image IGraphicalMazeSolver.GenerateSolution(GraphicalMaze maze)
        {
            // TODO
            return maze.MazeImage;
        }
    }
}
