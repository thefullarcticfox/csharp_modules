using System.Collections;
using System.IO;
using System.Text.Json;

namespace d02_ex01.Configuration.Sources
{
    class JsonSource : IConfigurationSource
    {
        private readonly string _configPath;
        private readonly int _priority;
        private readonly Hashtable _params;
        public Hashtable Params { get => _params; }
        public int Priority { get => _priority; }

        public JsonSource(string configPath, int priority)
        {
            _configPath = configPath;
            _priority = priority;
            string jsonText = File.ReadAllText(_configPath);
            _params = JsonSerializer.Deserialize<Hashtable>(jsonText);
        }
    }
}
