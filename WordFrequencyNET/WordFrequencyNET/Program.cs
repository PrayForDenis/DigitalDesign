using System;
using System.Collections.Generic;
using System.Diagnostics;
using WordFrequencyNET.ServiceReference1;

namespace WordFrequencyNET
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Welcome! To start the program, enter the file name with the permission : ");

            string filename = Console.ReadLine();

            if (filename == null)
            {
                Console.WriteLine("You entered an empty string!");
                Console.ReadKey();
                return;
            }

            string content;

            ITextReader reader = new FileReader(filename);
            content = reader.Read();

            if (content == "")
            {
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine("The file has been read successfully!");
                Console.ReadKey();
            }

            string splitSymbols = ".,?!/*”“\"—[]{}«»();:… " + Environment.NewLine;

            var client = new Service1Client();

            Stopwatch timer = new Stopwatch();

            timer.Start();

            Dictionary<string, int> sortedFrequency = (Dictionary<string, int>)client.ProcessTextAsync(content, splitSymbols);

            timer.Stop();

            client.Close();

            Console.WriteLine(timer.ElapsedMilliseconds + " ms");

            IWriter writer = new FileWriter();
            writer.Write(sortedFrequency);

            Console.ReadKey();
        }
    }
}