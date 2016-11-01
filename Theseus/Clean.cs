using System;
using System.Drawing;
using System.IO;

namespace Theseus
{
    public class Clean : ISubcommand
    {
        private static readonly Clean Instance = new Clean();

        public static Clean GetInstance()
        {
            return Instance;
        }

        private const string Description = "Cleans a maze image.";
        private const string Usage = "clean <sourceImageFile.(bmp|png|jpg)> <outputImageFile.(bmp|png|jpg)>";

        private static readonly Color DefaultStartColor = Color.FromName("Red");
        private static readonly Color DefaultFinishColor = Color.FromName("Blue");
        private static readonly Color DefaultWallColor = Color.FromName("Black");
        private static readonly Color DefaultSolutionColor = Color.FromName("Green");

        protected Clean()
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
                // Generate cleaned image
                var cleanedImage = CleanMaze(graphicalMaze);

                // Write solution image, creating the directory if necessary
                var ouputDirectory = Path.GetDirectoryName(outputFile);
                if (ouputDirectory != null)
                {
                    Directory.CreateDirectory(ouputDirectory);
                    cleanedImage.Save(outputFile);
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
                Console.WriteLine("Image cleaned and saved (" + outputFile + ".");
            }

            Exit(0);
        }

        private const int Threshold = 128;

        public static Image CleanMaze(GraphicalMaze maze)
        {
            // TODO Consider use of ColorMask for more efficient operation
            var palette = new[] {maze.WallColor, maze.StartColor, maze.FinishColor, Color.White};
            var cleanedImage = new Bitmap(maze.MazeImage);
            for (var y = 0; y < maze.Height; y++)
            {
                for (var x = 0; x < maze.Width; x++)
                {
                    cleanedImage.SetPixel(x, y, GetNearestColor(palette, maze.MazeImage.GetPixel(x, y)));
                }
            }

            return cleanedImage;
        }

        public static Color GetNearestColor(Color[] palette, Color color)
        {
            var nearestColor = Color.Empty;
            var nearestDistance = double.MaxValue;
            foreach(var paletteColor in palette)
            {
                if (paletteColor.ToArgb() == color.ToArgb()) return paletteColor;

                // Compute
                var distance = CalculateColorDistance(color, paletteColor);

                if (!(distance < nearestDistance)) continue;

                nearestColor = paletteColor;
                nearestDistance = distance;
            }

            return nearestColor;
        }

        public static double CalculateColorDistance(Color color1, Color color2)
        {
            var dR = color1.R - color2.R;
            var dG = color1.G - color2.G;
            var dB = color1.B - color2.B;

            return Math.Sqrt(dR * dR + dG * dG + dB * dB);
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
