using System.Collections;

namespace d02_ex01.Configuration.Sources
{
    interface IConfigurationSource
    {
        public Hashtable Params { get; }
    }
}
