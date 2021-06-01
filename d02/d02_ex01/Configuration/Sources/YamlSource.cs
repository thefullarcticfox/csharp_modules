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
            string yamlText = File.ReadAllText(_configPath);
            Params = deserializer.Deserialize<Hashtable>(yamlText);

            TempConverterAfterInit();
        }

        private void TempConverterAfterInit()
        {
            var keys = new ArrayList(Params.Keys);
            foreach (string key in keys)
            {
                string str = Params[key].ToString();
                if (str == "true")
                    Params[key] = true;
                else if (str == "false")
                    Params[key] = false;
                else if (int.TryParse(Params[key].ToString(), out int tmpInt))
                    Params[key] = tmpInt;
                else if (double.TryParse(Params[key].ToString(), out double tmpDouble))
                    Params[key] = tmpDouble;
            }
        }
    }
}
