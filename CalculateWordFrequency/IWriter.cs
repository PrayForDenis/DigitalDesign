using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateWordFrequency
{
    public interface IWriter
    {
        void Write(IReadOnlyDictionary<string, int> frequency);
    }
}
