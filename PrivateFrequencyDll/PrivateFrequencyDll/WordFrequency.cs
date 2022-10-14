using System.Collections.Concurrent;

namespace PrivateFrequencyDll
{
    public class WordFrequency
    {
        public IReadOnlyDictionary<string, int> ParallelCalculateSorted(string text, string splitSymbols)
        {
            string[] words = text.Split(splitSymbols.ToCharArray(),
                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            ConcurrentDictionary<string, int> frequency = new ConcurrentDictionary<string, int>();

            ParallelLoopResult result = Parallel.ForEach(
                words.Select(x => x.ToLower()),
                (word) => frequency.AddOrUpdate(word, 1, (word, count) => count + 1));

            if (!result.IsCompleted)
                throw new Exception("An error occurred while processing words into the dictionary.");

            IReadOnlyDictionary<string, int> sortedFrequency = frequency
                                                                        .OrderByDescending(x => x.Value)
                                                                        .ThenBy(y => y.Key)
                                                                        .ToDictionary(x => x.Key, y => y.Value);

            return sortedFrequency;
        }

        private IReadOnlyDictionary<string, int> CalculateSorted(string text, string splitSymbols)
        {
            string[] words = text.Split(splitSymbols.ToCharArray(), 
                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, int> frequency = new Dictionary<string, int>();

            foreach (string word in words.Select(x => x.ToLower()))
            {
                if (frequency.TryGetValue(word, out int count))
                    frequency[word] = count + 1;
                else
                    frequency.Add(word, 1);
            }

            IReadOnlyDictionary<string, int> sortedFrequency = frequency
                                                                        .OrderByDescending(x => x.Value)
                                                                        .ThenBy(y => y.Key)
                                                                        .ToDictionary(x => x.Key, y => y.Value);

            return sortedFrequency;
        }
    } 
}