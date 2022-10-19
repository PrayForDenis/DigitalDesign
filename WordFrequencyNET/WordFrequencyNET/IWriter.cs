using System.Collections.Generic;

namespace WordFrequencyNET
{
    public interface IWriter
    {
        void Write(Dictionary<string, int> frequency);
    }
}
