using System.Collections;
using System.IO;
using System.Text.Json;

namespace d02_ex01.Configuration.Sources
{
    class JsonSource : IConfigurationSource
    {
        private readonly string _configPath;
        public Hashtable Params { get; }
        public int Priority { get; }

        public JsonSource(string configPath, int priority)
        {
            _configPath = configPath;
            Priority = priority;
        }

        public Hashtable Deserialize()
        {
            string jsonText = File.ReadAllText(_configPath);
            return JsonSerializer.Deserialize<Hashtable>(jsonText);
        }
    }
}
