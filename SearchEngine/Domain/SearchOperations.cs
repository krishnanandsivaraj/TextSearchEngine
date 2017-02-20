using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    class SearchOperations : SearchBase, IFactory
    {
        SearchBase search = new SearchBase();
        public string Search(string[] s)
        {
            search.BaseSearch(s);
            return null;
        }
    }
}
