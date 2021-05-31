using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace d02_ex00.Model
{
    public class Book : ISearchable
    {
        public Media MediaType => Media.Book;

        [JsonPropertyName("book_details")]
        public List<BookDetail> BookDetails { get; set; }

        public string Title => BookDetails[0].Title;

        public string Author => BookDetails[0].Author;

        public string SummaryShort => BookDetails[0].Description;

        [JsonPropertyName("rank")]
        public int Rank { get; set; }

        [JsonPropertyName("list_name")]
        public string ListName { get; set; }

        [JsonPropertyName("amazon_product_url")]
        public string Url { get; set; }

        public override string ToString() =>
            $"- {Title} by {Author} [{Rank} on NYT’s {ListName}]\n" +
            $"{SummaryShort}\n{Url}";
    }

    public class BookDetail
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }
    }
}
