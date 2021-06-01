using System;
using System.Collections;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            var options = new JsonSerializerOptions { Converters = { new ObjectConverter() } };
            Params = JsonSerializer.Deserialize<Hashtable>(jsonText, options);
        }
    }

    public class ObjectConverter : JsonConverter<object>
    {
        public override object Read(ref Utf8JsonReader reader,
            Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                var tmpInt = reader.GetInt32();
                return tmpInt;
            }
            catch { }
            try
            {
                var tmpBool = reader.GetBoolean();
                return tmpBool;
            }
            catch { }
            try
            {
                var tmpStr = reader.GetString();
                return tmpStr;
            }
            catch { }
            try
            {
                var tmpDouble = reader.GetDouble();
                return tmpDouble;
            }
            catch { }
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer,
            object value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToString());
    }
}
