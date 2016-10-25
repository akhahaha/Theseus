using System.Drawing;

namespace Theseus
{
    class WallFollowerGraphicalMazeSolver : GraphicalMazeSolver
    {
        public static WallFollowerGraphicalMazeSolver Create()
        {
            return new WallFollowerGraphicalMazeSolver();
        }

        private WallFollowerGraphicalMazeSolver()
        {
            // Private constructor to force use of factory method
        }

        Image GraphicalMazeSolver.GenerateSolution(Image mazeImage, Color startColor, Color finishColor, Color wallColor, Color solutionColor)
        {
            // TODO
            return mazeImage;
        }
    }
}
