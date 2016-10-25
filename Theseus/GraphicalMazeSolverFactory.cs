using System;

namespace Theseus
{
    enum SolverType { WallFollower };

    static class GraphicalMazeSolverFactory
    {

        public static GraphicalMazeSolver GetSolver(SolverType algorithmType)
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
