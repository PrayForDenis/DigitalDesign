using System;
using System.Collections.Generic;
using System.IO;

namespace WordFrequencyNET
{
    public class FileWriter : IWriter
    {
        public void Write(Dictionary<string, int> frequency)
        {
            foreach (var pair in frequency)
            {
                try
                {
                    File.AppendAllText("Stats.txt", pair.Key + " " + pair.Value + Environment.NewLine);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: {0}", e.Message);
                    return;
                }
            }

            Console.WriteLine("The data has been writed to file \"Stats.txt\" successfully!");
        }
    }
}
