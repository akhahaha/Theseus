using System;
using System.Drawing;
using System.IO;

namespace Theseus
{
    public class Solve : ISubcommand
    {
        private static readonly Solve Instance = new Solve();

        public static Solve GetInstance()
        {
            return Instance;
        }

        private const string Description = "Solves a maze image.";
        private const string Usage = "solve <sourceImageFile.(bmp|png|jpg)> <outputImageFile.(bmp|png|jpg)>";

        private static readonly Color DefaultStartColor = Color.FromName("Red");
        private static readonly Color DefaultFinishColor = Color.FromName("Blue");
        private static readonly Color DefaultWallColor = Color.FromName("Black");
        private static readonly Color DefaultSolutionColor = Color.FromName("Green");

        protected Solve()
        {
        }

        public string GetDescription()
        {
            return Description;
        }

        public string GetUsage()
        {
            return Usage;
        }

        public void Execute(string[] args)
        {
            // Validate arguments
            if (args.Length != 2)
            {
                Console.Error.WriteLine("Incorrect number of arguments found.");
                Exit(-1);
            }

            // Process arguments
            var currentDirectory = Directory.GetCurrentDirectory();
            var sourceFile = Path.Combine(currentDirectory, args[0]);
            var outputFile = Path.Combine(currentDirectory, args[1]);

            // TODO: Validate file formats

            // Get source image
            try
            {
                var sourceImage = Image.FromFile(sourceFile);
                var graphicalMaze = GraphicalMaze.Create(sourceImage, DefaultWallColor, DefaultStartColor,
                    DefaultFinishColor,
                    DefaultSolutionColor);
                // Generate solution image
                var graphicalMazeSolver = GraphicalMazeSolverFactory.GetSolver(SolverType.ShortestPath);
                var solutionImage = graphicalMazeSolver.GenerateSolution(graphicalMaze);

                // Write solution image, creating the directory if necessary
                var ouputDirectory = Path.GetDirectoryName(outputFile);
                if (ouputDirectory != null)
                {
                    Directory.CreateDirectory(ouputDirectory);
                    solutionImage.Save(outputFile);
                }
                else
                {
                    Console.Error.WriteLine("Cannot retrieve directoy information for output file path ("
                                            + outputFile + ".");
                    Exit(-1);
                }
            }
            catch (FileNotFoundException)
            {
                Console.Error.WriteLine("Source image not found (" + sourceFile + ").");
                Exit(-1);
            }

            // Solution generated successfully
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("Solution generated and saved (" + outputFile + ".");
            }

            Exit(0);
        }

        private static void Exit(int exitCode)
        {
            // Freeze console output
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("Press any key to continue...");
                Console.Read();
            }

            Environment.Exit(exitCode);
        }
    }
}
