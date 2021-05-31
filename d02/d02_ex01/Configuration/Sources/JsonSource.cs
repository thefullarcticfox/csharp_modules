using System.Collections;
using System.IO;
using System.Text.Json;

namespace d02_ex01.Configuration.Sources
{
    class JsonSource : IConfigurationSource
    {
        private string _configPath;
        public Hashtable Params { get; set; }

        public JsonSource(string configPath)
        {
            _configPath = configPath;
            string jsonText = File.ReadAllText(_configPath);
            Params = JsonSerializer.Deserialize<Hashtable>(jsonText);
        }
    }
}
