using System.Collections;

namespace d02_ex01.Configuration.Sources
{
    public interface IConfigurationSource
    {
        public int Priority { get; }

        public Hashtable Deserialize();
    }
}
