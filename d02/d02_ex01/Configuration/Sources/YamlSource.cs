using System.Collections;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.TypeResolvers;

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
            string yamlText = File.ReadAllText(_configPath);
            Params = deserializer.Deserialize<Hashtable>(yamlText);
        }
    }
}
