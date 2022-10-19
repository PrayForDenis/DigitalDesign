using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using FrequencyDll;

namespace WebService
{
    public class Service1 : IService1
    {
        public Dictionary<string, int> ProcessTextAsync(string content, string splitSymbols)
        {
            var counter = new WordFrequency();

            return counter.ParallelCalculateSorted(content, splitSymbols);
        }
    }
}
