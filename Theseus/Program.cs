using System;
using System.Drawing;
using System.IO;

namespace Theseus
{
    public class Program
    {
        private const string Usage = "Usage: theseus.exe sourceImageFile.[bmp/png/jpg] outputImageFile.[bmp/png/jpg]";
        private static readonly Color StartColor = Color.FromName("Red");
        private static readonly Color FinishColor = Color.FromName("Blue");
        private static readonly Color WallColor = Color.FromName("Black");
        private static readonly Color SolutionColor = Color.FromName("Green");

        public static void Main(string[] args)
        {
            if (args.Length == 1 && (args[0] == "-h" || args[0] == "--help"))
            {
                Console.WriteLine(Usage);
                Exit();
            }

            // Validate arguments
            if (args.Length != 2)
            {
                Console.Error.WriteLine("Incorrect number of arguments found.");
                Console.Error.WriteLine(Usage);
                Exit();
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
                var graphicalMaze = GraphicalMaze.Create(sourceImage, WallColor, StartColor, FinishColor,
                    SolutionColor);
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
                    Exit();
                }
            }
            catch (FileNotFoundException)
            {
                Console.Error.WriteLine("Source image not found (" + sourceFile + ").");
                Exit();
            }

            // Solution generated successfully
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("Solution generated and saved (" + outputFile + ".");
            }
            Exit();
        }

        private static void Exit()
        {
            // Freeze console output
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("Press any key to continue...");
                Console.Read();
            }

            Environment.Exit(0); // TODO: Define exit codes
        }
    }
}
