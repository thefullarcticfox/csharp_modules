using System;
using System.IO;

static int LevDist(string a, string b)
{
    if (a.Length == 0)
        return a.Length;
    if (b.Length == 0)
        return b.Length;

    int res0 = LevDist(a[1..], b[1..]);
    if (char.ToLower(a[0]) == char.ToLower(b[0]))
        return res0;

    int res1 = LevDist(a[1..], b);
    int res2 = LevDist(a, b[1..]);
    return 1 + Math.Min(res0, Math.Min(res1, res2));
}

const string dictPath = "us.txt";
if (!File.Exists(dictPath))
    return;

string[] dictionary = File.OpenText(dictPath).ReadToEnd().
    Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

Console.WriteLine("> Enter name:");
var name = Console.ReadLine();
if (name == null)
    return;

var minLevDist = int.MaxValue;
var closestName = "";
foreach (string word in dictionary)
{
    int levDist = LevDist(name, word);
    // Console.WriteLine($"{name} - {word} = {levDist}");
    if (minLevDist == levDist && Math.Abs(word.Length - name.Length) < Math.Abs(closestName.Length - name.Length) ||
        minLevDist > levDist)
    {
        minLevDist = levDist;
        closestName = word;

        switch (minLevDist)
        {
            case 0:
            {
                Console.WriteLine($">Hello, {name}!");
                return;
            }
            case < 3:
            {
                Console.WriteLine($">Did you mean “{closestName}”? Y/N");
				if (Console.ReadLine() == "Y")
                {
                    name = closestName;
                    Console.WriteLine($">Hello, {name}!");
                    return;
                }
                break;
            }
        }
    }
}

Console.WriteLine("Name not found");
