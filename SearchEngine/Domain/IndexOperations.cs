using System;

namespace SearchEngine
{
    public class IndexOperations : SearchBase, IFactory
    {
            SearchBase search = new SearchBase();
        public string Search(string[] s)
        {
             search.BaseSearch(s);
            return null;
        }

    }
}
