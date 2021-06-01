using d02_ex01.Configuration.Sources;
using System;
using System.Collections;
using System.Collections.Generic;

namespace d02_ex01.Configuration
{
    public class Configuration
    {
        public Hashtable Params;

        public Configuration(List<IConfigurationSource> sources)
        {
            Params = new Hashtable(sources[0].Params);
            foreach (DictionaryEntry param in sources[1].Params)
                Params[param.Key] = param.Value;
        }

        public void PrintConfig()
        {
            foreach (DictionaryEntry entry in Params)
                Console.WriteLine($"{entry.Key} : {entry.Value}");
        }
    }
}
