using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using d02_ex00;
using d02_ex00.Model;

CultureInfo.CurrentCulture = new CultureInfo("en-GB", false);

var searchables = new List<ISearchable>();

const string booksJsonFilename = "book_reviews.json";
const string moviesJsonFilename = "movie_reviews.json";

try
{
    searchables.AddRange(JsonResponse<Book>.DeserializeFile(booksJsonFilename));
    searchables.AddRange(JsonResponse<Movie>.DeserializeFile(moviesJsonFilename));
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

for (var i = 0; i < searchables.Count; i++)
{
    Console.WriteLine($"{i} {searchables[i].MediaType}");
    Console.WriteLine(searchables[i].ToString());
    Console.WriteLine();
}

namespace d02_ex00
{
    public class JsonResponse<T> where T : ISearchable
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("results")]
        public List<T> Results { get; set; }

        public static IEnumerable<T> DeserializeFile(string jsonFilename)
        {
            string bookJson = File.ReadAllText(jsonFilename);
            var response = JsonSerializer.Deserialize<JsonResponse<T>>(bookJson);
            return response.Results;
        }
    }
}
