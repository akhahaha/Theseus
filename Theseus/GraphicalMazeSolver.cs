using System.Drawing;

namespace Theseus
{
    public interface IGraphicalMazeSolver
    {
        Image GenerateSolution(GraphicalMaze maze);
    }
}
