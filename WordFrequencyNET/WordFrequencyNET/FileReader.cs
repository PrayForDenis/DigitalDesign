using System;
using System.IO;

namespace WordFrequencyNET
{
    public class FileReader : ITextReader
    {
        private readonly string _name;

        public FileReader(string name)
        {
            _name = name;
        }

        public string Read()
        {
            string content;

            try
            {
                content = File.ReadAllText(_name, System.Text.Encoding.UTF8);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return "";
            }

            return content;
        }
    }
}
