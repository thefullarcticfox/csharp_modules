using d02_ex01.Configuration.Sources;
using System;
using System.Collections;
using System.Globalization;

CultureInfo.CurrentCulture = new CultureInfo("en-GB", false);

const string jsonConfigFile = "config.json";
const string yamlConfigFile = "config.yml";

IConfigurationSource jsonConfig = new JsonSource(jsonConfigFile);
IConfigurationSource yamlConfig = new YamlSource(yamlConfigFile);

foreach (DictionaryEntry entry in jsonConfig.Params)
{
    Console.WriteLine($"[{entry.Key}] {entry.Value}");
}
Console.WriteLine("--------------------------------------------");
foreach (DictionaryEntry entry in yamlConfig.Params)
{
    Console.WriteLine($"[{entry.Key}] {entry.Value}");
}
