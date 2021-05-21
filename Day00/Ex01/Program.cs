using System;
using System.IO;

static int LevenshteinDistance(string a, string b)
{
    var distMatrix = new int[a.Length + 1, b.Length + 1];
    if (a.Length == 0)
        return b.Length;
    if (b.Length == 0)
        return a.Length;

    for (var i = 0; i <= a.Length; i++)
        distMatrix[i, 0] = i;
    for (var j = 0; j <= b.Length; j++)
        distMatrix[0, j] = j;

    for (var i = 1; i <= a.Length; i++)
    {
        for (var j = 1; j <= b.Length; j++)
        {
            int dist = char.ToLower(a[i - 1]) == char.ToLower(b[j - 1]) ? 0 : 1;
            distMatrix[i, j] = Math.Min(distMatrix[i - 1, j - 1] + dist,
                Math.Min(distMatrix[i - 1, j] + 1, distMatrix[i, j - 1] + 1));
        }
    }
    return distMatrix[a.Length, b.Length];
}

static bool IsInvalidName(string name)
{
    foreach (char c in name)
        if (!char.IsLetter(c) && c != '-' && c != ' ')
            return true;
    return false;
}

const string dictPath = "us.txt";
if (!File.Exists(dictPath))
{
    Console.WriteLine($"Dictionary file {dictPath} not found.");
    return;
}

// loading dictionary to memory
string[] dictionary;
try
{
    StreamReader dictionaryFile = File.OpenText(dictPath);
    string plainDictionary = dictionaryFile.ReadToEnd();
    dictionary = plainDictionary.Split(new[] {"\r\n", "\r", "\n"}, StringSplitOptions.RemoveEmptyEntries);
    if (dictionary.Length == 0)
        throw new Exception($"Dictionary file {dictPath} is empty.");
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    return;
}

// user input begin
Console.WriteLine("> Enter name:");
string name = Console.ReadLine();

// validations
if (string.IsNullOrEmpty(name))
{
    Console.WriteLine("Your name was not found.");
    return;
}

if (IsInvalidName(name))
{
    Console.WriteLine("Invalid name entered.");
    return;
}

// counting Levenshtein distances for all words in dictionary
var levDistances = new int[dictionary.Length];
for (var i = 0; i < dictionary.Length; i++)
    levDistances[i] = LevenshteinDistance(name, dictionary[i]);

var found = false;
for (var minLevDist = 0; minLevDist < 3 && !found; minLevDist++)
{
    for (var i = 0; i < dictionary.Length && !found; i++)
    {
        if (levDistances[i] != minLevDist)
            continue;

        string closestName = dictionary[i];
        if (minLevDist == 0)
        {
            name = closestName;
            found = true;
            break;
        }

        Console.WriteLine($"> Did you mean “{closestName}”? Y/N");
        while (true)
        {
            string reply = Console.ReadLine();
            if (reply != null)
            {
                if (reply.ToUpper() == "Y")
                {
                    name = closestName;
                    found = true;
                    break;
                }
                if (reply.ToUpper() == "N")
                    break;
            }
        }
    }
}

Console.WriteLine(found ? $"Hello, {name}!" : "Your name was not found.");
