using System.Collections;
using System.IO;
using YamlDotNet.Serialization;

namespace d02_ex01.Configuration.Sources
{
    class YamlSource : IConfigurationSource
    {
        private string _configPath;
        public Hashtable Params { get; set; }

        public YamlSource(string configPath)
        {
            _configPath = configPath;

            IDeserializer deserializer = new DeserializerBuilder().Build();
            TextReader yamlText = new StreamReader(_configPath);
            Params = deserializer.Deserialize<Hashtable>(yamlText);
        }
    }
}
