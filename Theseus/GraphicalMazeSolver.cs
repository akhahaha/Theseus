using System.Drawing;

namespace Theseus
{
    public interface GraphicalMazeSolver
    {
        Image GenerateSolution(Image mazeImage, Color startColor, Color finishColor, Color wallColor, Color solutionColor);
    }
}
