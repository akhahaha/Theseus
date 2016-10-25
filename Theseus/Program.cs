using System;
using System.Drawing;
using System.IO;

namespace Theseus
{
    class Program
    {
        private static string USAGE = "Usage: theseus.exe sourceImageFile.[bmp/png/jpg] outputImageFile.[bmp/png/jpg]";
        private static Color startColor = Color.FromName("Red");
        private static Color finishColor = Color.FromName("Blue");
        private static Color wallColor = Color.FromName("Black");
        private static Color solutionColor = Color.FromName("Green");

        static void Main(string[] args)
        {
            if (args.Length == 1 && (args[0] == "-h" || args[0] == "--help"))
            {
                Console.WriteLine(USAGE);
                exit();
            }

            // Validate arguments
            if (args.Length != 2)
            {
                Console.Error.WriteLine("Incorrect number of arguments found.");
                Console.Error.WriteLine(USAGE);
                exit();
            }

            // Process arguments
            string currentDirectory = Directory.GetCurrentDirectory();
            string sourceFile = Path.Combine(currentDirectory, args[0]);
            string outputFile = Path.Combine(currentDirectory, args[1]);

            // TODO: Validate file formats

            // Get source image
            Image sourceImage;
            try
            {
                sourceImage = Image.FromFile(sourceFile);
                // Generate solution image
                GraphicalMazeSolver graphicalMazeSolver = GraphicalMazeSolverFactory.GetSolver(SolverType.WallFollower);
                Image solutionImage = graphicalMazeSolver.GenerateSolution(sourceImage, startColor, finishColor, wallColor, solutionColor);

                // Write solution image, creating the directory if necessary
                Directory.CreateDirectory(Path.GetDirectoryName(outputFile));
                solutionImage.Save(outputFile);
            } catch (FileNotFoundException e)
            {
                Console.Error.WriteLine("Source image not found (" + sourceFile + ").");
                exit();
            }

            exit();
        }

        private static void exit()
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
