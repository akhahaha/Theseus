using System;
using System.Collections.Generic;

namespace Theseus
{
    public class Program
    {
        private const string ProgramName = "theseus";
        private const string Version = "1.0.0";

        private static readonly Dictionary<string, ISubcommand> Subcommands = new Dictionary<string, ISubcommand>()
        {
            {"help", new Help()},
            {"solve", Solve.GetInstance()},
            {"clean", Clean.GetInstance()}
        };

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(GetProgramDescription());
                Exit(0);
                return;
            }

            // Determine subcommand
            var subcommandName = args[0];
            var subcommand = GetSubcommand(subcommandName);
            if (subcommand == null)
            {
                Console.Error.WriteLine("Subcommand '" + subcommandName + "' not recognized.");
                Exit(-1);
                return;
            }

            var subcommandArgs = new string[args.Length - 1];
            Array.Copy(args, 1, subcommandArgs, 0, subcommandArgs.Length);
            subcommand.Execute(subcommandArgs);

            Exit(0);
        }

        private static string GetProgramDescription()
        {
            return ProgramName + " v" + Version;
        }

        private class Help : ISubcommand
        {
            private const string Description = "Shows help information.";
            private const string Usage = "help [subcommand]";

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
                if (args.Length == 0)
                {
                    Console.WriteLine(GetProgramDescription());
                    Console.WriteLine();
                    Console.WriteLine("Available commands:");
                    foreach (var entry in Subcommands)
                    {
                        Console.WriteLine("\t" + entry.Key + "\t\t" + entry.Value.GetDescription());
                    }

                    Exit(0);
                    return;
                }

                var subcommandName = args[0];
                var subcommand = GetSubcommand(subcommandName);
                if (subcommand == null)
                {
                    Console.Error.WriteLine("Subcommand '" + subcommandName + "' not recognized.");
                    Exit(-1);
                    return;
                }

                Console.WriteLine(subcommandName);
                Console.WriteLine(subcommand.GetDescription());
                Console.WriteLine();
                Console.WriteLine("Usage: " + subcommand.GetUsage());

                Exit(0);
            }
        }

        private static ISubcommand GetSubcommand(string name)
        {
            return Subcommands.ContainsKey(name) ? Subcommands[name] : null;
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
