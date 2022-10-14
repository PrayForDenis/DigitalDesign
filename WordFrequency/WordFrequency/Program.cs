using System.Diagnostics;
using System.Reflection;

namespace WordFrequency
{
    public class Program
    {
        private const string AssemblyDll = "PrivateFrequencyDll";
        private const string TypeName = "WordFrequency";
        private const string PrivateMethodName = "CalculateSorted";
        private const string PublicMethodName = "ParallelCalculateSorted";

        public static void Main(string[] args)
        {
            Console.Write("Welcome! To start the program, enter the file name with the permission : ");

            string? filename = Console.ReadLine();

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
            Object[] parameters = {content, splitSymbols};

            Assembly dll = Assembly.LoadFrom("D:\\Games\\C#\\PrivateFrequencyDll\\PrivateFrequencyDll\\bin\\Debug\\net6.0\\" + AssemblyDll + ".dll");

            Type? type = dll.GetType(AssemblyDll + "." + TypeName);
            Object? frequency = Activator.CreateInstance(type);
            MethodInfo? mi = type.GetMethod(PrivateMethodName, BindingFlags.Instance | BindingFlags.NonPublic);
            //MethodInfo? mi = type.GetMethod(PublicMethodName, BindingFlags.Instance | BindingFlags.Public);

            Stopwatch timer = new Stopwatch();

            timer.Start();

            IReadOnlyDictionary<string, int> sortedFrequency = (IReadOnlyDictionary<string, int>) mi.Invoke(frequency, parameters);

            timer.Stop();

            Console.WriteLine(timer.ElapsedMilliseconds + " ms");

            IWriter writer = new FileWriter();
            writer.Write(sortedFrequency);

            Console.ReadKey();
        }
    }
}