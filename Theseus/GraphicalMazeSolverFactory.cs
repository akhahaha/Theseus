using System;

namespace Theseus
{
    public enum SolverType
    {
        WallFollower
    }

    public static class GraphicalMazeSolverFactory
    {
        public static IGraphicalMazeSolver GetSolver(SolverType algorithmType)
        {
            switch (algorithmType)
            {
                case SolverType.WallFollower:
                    return WallFollowerGraphicalMazeSolver.Create();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
