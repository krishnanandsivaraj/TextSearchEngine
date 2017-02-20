using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    public static class SearchFactory
    {
        
        public static IFactory GetInstance(string command) {
            IFactory instance = null;
            switch (command)
            {
                case "index":
                    instance = new IndexOperations();
                    break;
                case "search":
                    instance = new SearchOperations();
                    break;
                default:
                    instance = null;
                    break;
            }
            return instance;
        }
    }
}
