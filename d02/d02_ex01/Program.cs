using d02_ex01.Configuration;
using d02_ex01.Configuration.Sources;
using System;
using System.Collections.Generic;
using System.Globalization;

CultureInfo.CurrentCulture = new CultureInfo("en-GB", false);

Configuration configuration;
try
{
    var configs = new List<IConfigurationSource>(2);
    if (!int.TryParse(args[1], out int jsonConfigPriority) ||
        !int.TryParse(args[3], out int yamlConfigPriority))
        throw new ArgumentException("Invalid arguments");
    string jsonConfigFile = args[0];
    string yamlConfigFile = args[2];

    configs.Add(new JsonSource(jsonConfigFile, jsonConfigPriority));
    configs.Add(new YamlSource(yamlConfigFile, yamlConfigPriority));
    configuration = new Configuration(configs);
    configuration.PrintConfig();
}
catch
{
    Console.WriteLine("Invalid data. Check your input and try again.");
    return;
}

