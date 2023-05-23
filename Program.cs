using System;
using System.IO;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Write("Enter the path to a text file: ");
            string filePath = Console.ReadLine();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                Console.WriteLine("File contents:");
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
            }
            else
            {
                Console.WriteLine("File does not exist. Please try again.");
                continue;
            }

            Console.WriteLine("Press 'Q' to quit or any other key to continue...");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.KeyChar == 'q' || keyInfo.KeyChar == 'Q')
                break;

            Console.WriteLine();
        }
    }
}
