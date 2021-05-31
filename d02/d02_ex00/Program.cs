using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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

Console.WriteLine("Input search text:");
string search = Console.ReadLine();

IEnumerable<ISearchable> found = searchables.Where(
    s => s.Title.Contains(search, StringComparison.OrdinalIgnoreCase));

if (found.Count() == 0)
{
    Console.WriteLine($"There are no \"{search}\" in media today");
    return;
}

Console.WriteLine($"Items found: {found.Count()}");
FilterByMediaAndPrint(found, Media.Book);
FilterByMediaAndPrint(found, Media.Movie);

static void FilterByMediaAndPrint(IEnumerable<ISearchable> found, Media media)
{
    var foundMedia = found.Where(s => s.MediaType == media);
    if (!foundMedia.Any()) return;

    Console.WriteLine(Environment.NewLine +
        $"{media} search result [{foundMedia.Count()}]");
    Console.WriteLine(string.Join(Environment.NewLine,
        foundMedia.Select(q => q.ToString())));
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
