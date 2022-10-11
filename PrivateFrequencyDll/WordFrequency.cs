using System.Collections.Generic;
using System.Linq;

namespace PrivateFrequencyDll
{
    public class WordFrequency
    {
        private IReadOnlyDictionary<string, int> CalculateSorted(string text, string splitSymbols)
        {
            foreach (char symbol in splitSymbols)
            {
                text = text.Replace(symbol, ' ');
            }

            text = text.Replace(Environment.NewLine, " ");

            string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, int> _frequency = new Dictionary<string, int>();

            foreach (string word in words.Select(x => x.ToLower()))
            {
                if (_frequency.TryGetValue(word, out int count))
                    _frequency[word] = count + 1;
                else
                    _frequency.Add(word, 1);
            }

            IReadOnlyDictionary<string, int> sortedFrequency = _frequency
                                                                        .OrderByDescending(x => x.Value)
                                                                        .ThenBy(y => y.Key)
                                                                        .ToDictionary(x => x.Key, y => y.Value);

            return sortedFrequency;
        }
    } 
}