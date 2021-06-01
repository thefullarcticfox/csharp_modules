﻿using System.Collections;
using System.IO;
using YamlDotNet.Serialization;

namespace d02_ex01.Configuration.Sources
{
    class YamlSource : IConfigurationSource
    {
        private readonly string _configPath;
        public Hashtable Params { get; }
        public int Priority { get; }

        public YamlSource(string configPath, int priority)
        {
            _configPath = configPath;
            Priority = priority;
            IDeserializer deserializer = new DeserializerBuilder().Build();
            string yamlText = File.ReadAllText(_configPath);
            Params = deserializer.Deserialize<Hashtable>(yamlText);
        }
    }
}
