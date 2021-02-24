using System;
using System.IO;
using System.Linq;

namespace DeleteEmptyDirectories
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string dir = Directory.GetCurrentDirectory();

            Console.WriteLine($"You are about to permanently remove empty directories from {dir}");
            Console.WriteLine("Are you sure you want to continue? (y/n)");
            var key = Console.ReadKey();
            Console.WriteLine();

            if (key.Key == ConsoleKey.N)
            {
                Console.WriteLine("Application aborted.");
            }
            else
            {
                string command = args.Any() ? args[0] : string.Empty;
                ProcessDirectory(dir, command);
                Console.WriteLine("Processing complete.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void ProcessDirectory(string startLocation, string command)
        {
            foreach (var directory in Directory.GetDirectories(startLocation))
            {
                ProcessDirectory(directory, command);

                if (!Directory.EnumerateFileSystemEntries(directory).Any())
                {
                    switch (command)
                    {
                        case "del":
                            Console.WriteLine($"Deleting directory {directory}");
                            Directory.Delete(directory, false);
                            break;
                        default:
                            // Note: the Console.WriteLine will not capture the root folder, because it is not empty.  The del will however, because it will be empty when the check occurs.
                            Console.WriteLine(directory);
                            break;
                    }
                }
            }
        }
    }
}
