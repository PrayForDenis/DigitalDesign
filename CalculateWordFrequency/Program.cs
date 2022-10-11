using System;
using System.Collections.Generic;
using System.Reflection;

namespace CalculateWordFrequency
{
    public class Program
    {
        private const string AssemblyDll = "PrivateFrequencyDll";
        private const string TypeName = "WordFrequency";
        private const string MethodName = "CalculateSorted";

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

            string splitSymbols = ".,?!/*”“\"—[]{}«»();:…";
            Object[] parameters = {content, splitSymbols};

            Assembly dll = Assembly.LoadFrom("D:\\Games\\DD\\1\\CalculateWordFrequency\\" + AssemblyDll + ".dll");

            Type type = dll.GetType(AssemblyDll + "." + TypeName);
            Object frequency = Activator.CreateInstance(type);
            MethodInfo mi = type.GetMethod(MethodName, BindingFlags.Instance | BindingFlags.NonPublic);

            IReadOnlyDictionary<string, int> sortedFrequency = (IReadOnlyDictionary<string, int>) mi.Invoke(frequency, parameters);

            IWriter writer = new FileWriter();
            writer.Write(sortedFrequency);

            Console.ReadKey();
        }
    }
}