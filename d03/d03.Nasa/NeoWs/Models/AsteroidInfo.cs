using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace d03.Nasa.NeoWs.Models
{
    public class AsteroidInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        public double Kilometers => CloseApproachData[0].MissDistance.Kilometers;

        [JsonPropertyName("close_approach_data")]
        public List<CloseApproachData> CloseApproachData { get; set; }
    }

    public class CloseApproachData
    {
        [JsonPropertyName("miss_distance")]
        public MissDistance MissDistance { get; set; }
    }

    public class MissDistance
    {
        [JsonPropertyName("kilometers"), JsonConverter(typeof(StringToDoubleConverter))]
        public double Kilometers { get; set; }
    }

    public class StringToDoubleConverter : JsonConverter<object>
    {
        public override bool CanConvert(Type typeToConvert) =>
            typeof(double) == typeToConvert;

        public override object Read(ref Utf8JsonReader reader,
            Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
                return reader.GetDouble();
            return double.Parse(reader.GetString() ?? string.Empty);
        }

        public override void Write(Utf8JsonWriter writer,
            object value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToString());
    }
}
