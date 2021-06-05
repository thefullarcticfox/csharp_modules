using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace d03.Nasa.Apod.Models
{
    public class MediaOfToday
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("copyright")]
        public string Copyright { get; set; }

        [JsonPropertyName("explanation"), JsonConverter(typeof(StringConverter))]
        public string Explanation { get; set; }

        [JsonPropertyName("url")]
        public Uri Url { get; set; }

        public override string ToString() =>
            $"{Date:d}\n\'{Title}\'{(Copyright != null ? $" by {Copyright}" : "")}\n{Explanation}\n{Url}";
    }

    public class StringConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader,
            Type typeToConvert, JsonSerializerOptions options) =>
            reader.GetString().Replace("\r", "");

        public override void Write(Utf8JsonWriter writer,
            string value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value);
    }
}
