using System.Collections.Concurrent;

namespace PrivateFrequencyDll
{
    public class WordFrequency
    {
        public IReadOnlyDictionary<string, int> ParallelCalculateSorted(string text, string splitSymbols)
        {
            ReplaceSplitSymbols(text, splitSymbols);

            string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            ConcurrentDictionary<string, int> frequency = new ConcurrentDictionary<string, int>();

            ToDictionary(words, frequency);

            IReadOnlyDictionary<string, int> sortedFrequency = frequency
                                                                        .OrderByDescending(x => x.Value)
                                                                        .ThenBy(y => y.Key)
                                                                        .ToDictionary(x => x.Key, y => y.Value);

            return sortedFrequency;
        }

        private void ReplaceSplitSymbols(string text, string splitSymbols)
        {
            ParallelLoopResult result = Parallel.ForEach(splitSymbols, (symbol) => text = text.Replace(symbol, ' '));

            if (!result.IsCompleted)
                throw new Exception("An error occurred while processing the text.");
        }

        private void ToDictionary(string[] words, ConcurrentDictionary<string, int> frequency)
        {
            ParallelLoopResult result = Parallel.ForEach(
                words.Select(x => x.ToLower()),
                (word) => frequency.AddOrUpdate(word, 1, (word, count) => count + 1));

            if (!result.IsCompleted)
                throw new Exception("An error occurred while processing words into the dictionary.");
        }

        private IReadOnlyDictionary<string, int> CalculateSorted(string text, string splitSymbols)
        {
            foreach (char symbol in splitSymbols)
            {
                text = text.Replace(symbol, ' ');
            }

            text = text.Replace(Environment.NewLine, " ");

            string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

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