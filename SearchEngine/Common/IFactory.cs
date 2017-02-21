using System.Collections.Generic;

namespace SearchEngine
{
    public interface IFactory
    {
        Dictionary<string, List<string>> Search(string[] s);
    }
}