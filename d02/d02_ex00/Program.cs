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

try
{
    const string bookJsonFilename = "book_reviews.json";
    string bookJson = File.ReadAllText(bookJsonFilename);
    var response = JsonSerializer.Deserialize<JsonResponse<Book>>(bookJson);
    if (response == null) return;
    searchables.AddRange(response.Results);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

try
{
    const string movieJsonFilename = "movie_reviews.json";
    string movieJson = File.ReadAllText(movieJsonFilename);
    var response = JsonSerializer.Deserialize<JsonResponse<Movie>>(movieJson);
    if (response == null) return;
    searchables.AddRange(response.Results);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

namespace d02_ex00
{
    public class JsonResponse<T> where T : ISearchable
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("results")]
        public List<T> Results { get; set; }
    }
}
