using System.Text.Json.Serialization;

namespace d02_ex00.Model
{
    public class Book : ISearchable
    {
        public Media MediaType => Media.Book;

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("description")]
        public string SummaryShort { get; set; }

        [JsonPropertyName("rank")]
        public int Rank { get; set; }

        [JsonPropertyName("list_name")]
        public string ListName { get; set; }

        [JsonPropertyName("amazon_product_url")]
        public string Url { get; set; }
    }
}
