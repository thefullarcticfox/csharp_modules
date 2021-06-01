using System.Collections;
using System.IO;
using YamlDotNet.Serialization;

namespace d02_ex01.Configuration.Sources
{
    class YamlSource : IConfigurationSource
    {
        private readonly string _configPath;
        private readonly int _priority;
        private readonly Hashtable _params;
        public Hashtable Params { get => _params; }
        public int Priority { get => _priority; }

        public YamlSource(string configPath, int priority)
        {
            _configPath = configPath;
            _priority = priority;
            IDeserializer deserializer = new DeserializerBuilder().Build();
            string yamlText = File.ReadAllText(_configPath);
            _params = deserializer.Deserialize<Hashtable>(yamlText);
        }
    }
}
