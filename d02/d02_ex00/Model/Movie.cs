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

        [JsonPropertyName("critics_pick")]
        public int IsCriticsPick { get; set; }

        [JsonPropertyName("summary_short")]
        public string SummaryShort { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
