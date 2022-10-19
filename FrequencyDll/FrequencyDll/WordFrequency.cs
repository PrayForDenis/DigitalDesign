using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FrequencyDll
{
    public class WordFrequency
    {
        public Dictionary<string, int> ParallelCalculateSorted(string text, string splitSymbols)
        {
            foreach(var symbol in splitSymbols) 
            {
                text = text.Replace(symbol, ' ');
            }

            string[] words = Regex.Split(text, @"\W+")
                .Where(word => word != "").ToArray();

            ConcurrentDictionary<string, int> frequency = new ConcurrentDictionary<string, int>();

            ParallelLoopResult result = Parallel.ForEach(
                words.Select(x => x.ToLower()),
                (word) => frequency.AddOrUpdate(word, 1, (key, count) => count + 1));

            if (!result.IsCompleted)
                throw new Exception("An error occurred while processing words into the dictionary.");

            Dictionary<string, int> sortedFrequency = frequency
                                                            .OrderByDescending(x => x.Value)
                                                            .ThenBy(y => y.Key)
                                                            .ToDictionary(x => x.Key, y => y.Value);

            return sortedFrequency;
        }

        private Dictionary<string, int> CalculateSorted(string text, string splitSymbols)
        {
            foreach (var symbol in splitSymbols)
            {
                text = text.Replace(symbol, ' ');
            }

            string[] words = Regex.Split(text, @"\W+")
                .Where(word => word != "").ToArray();

            Dictionary<string, int> frequency = new Dictionary<string, int>();

            foreach (string word in words.Select(x => x.ToLower()))
            {
                if (frequency.TryGetValue(word, out int count))
                    frequency[word] = count + 1;
                else
                    frequency.Add(word, 1);
            }

            Dictionary<string, int> sortedFrequency = frequency
                                                            .OrderByDescending(x => x.Value)
                                                            .ThenBy(y => y.Key)
                                                            .ToDictionary(x => x.Key, y => y.Value);

            return sortedFrequency;
        }
    }
}