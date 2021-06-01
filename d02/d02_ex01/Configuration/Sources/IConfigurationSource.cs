using System.Collections;

namespace d02_ex01.Configuration.Sources
{
    public interface IConfigurationSource
    {
        public Hashtable Params { get; }
        public int Priority { get; }
    }
}
