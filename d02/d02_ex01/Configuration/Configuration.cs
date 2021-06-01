using d02_ex01.Configuration.Sources;
using System;
using System.Collections;
using System.Collections.Generic;

namespace d02_ex01.Configuration
{
    public class Configuration
    {
        public readonly Hashtable Params;

        public Configuration(List<IConfigurationSource> sources)
        {
            var sortedSrcs = new List<IConfigurationSource>(sources);   // so it wont mutate outside
            sortedSrcs.Sort((src1, src2) => src1.Priority - src2.Priority);

            Params = new Hashtable();
            foreach (IConfigurationSource src in sortedSrcs)
                foreach (DictionaryEntry param in src.Params)
                    Params[param.Key] = param.Value;
        }

        public void PrintConfig()
        {
            foreach (DictionaryEntry entry in Params)
                Console.WriteLine($"{entry.Key} : {entry.Value}");
        }
    }
}
