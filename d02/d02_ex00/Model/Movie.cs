using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace d02_ex00.Model
{
    public class Movie : ISearchable
    {
        public Media MediaType => Media.Movie;

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("mpaa_rating")]
        public string Rating { get; set; }

        [JsonPropertyName("critics_pick"), JsonConverter(typeof(BoolConverter))]
        public bool IsCriticsPick { get; set; }

        [JsonPropertyName("summary_short")]
        public string SummaryShort { get; set; }

        [JsonPropertyName("link")]
        public Link Link { get; set; }

        public string Url => Link.Url;

        public override string ToString() =>
            $"{Title} {(IsCriticsPick ? "[NYT critic’s pick]" : "")}" +
            $"{Environment.NewLine}{SummaryShort}{Environment.NewLine}{Url}";
    }

    public class Link
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class BoolConverter : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader,
            Type typeToConvert, JsonSerializerOptions options) =>
            reader.GetInt32() != 0;

        public override void Write(Utf8JsonWriter writer,
            bool value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value ? "1" : "0");
    }
}
