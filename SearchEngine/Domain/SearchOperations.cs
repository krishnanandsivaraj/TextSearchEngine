using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    class SearchOperations : IFactory
    {
        SearchBase search = new SearchBase();
        public Dictionary<string, List<string>> Search(string[] s)
        {

            Dictionary<string, List<string>> output = search.BaseSearch(s);

            var items = output.SelectMany(d => d.Value).ToList();
            List<string> keys = new List<string>(output.Keys);
            string line, outputLine = null;
            string[] outputLineSubString = null;

            using (var fs = new FileStream(Directory.GetCurrentDirectory() + @"\index.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader file = new StreamReader(fs))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        outputLine += line + ";";
                    }
                }
            }
            outputLineSubString = outputLine.Split(';');
            
                
                for (int i = 0; i < outputLineSubString.Length; i++)
                {
                    
                    foreach (var t in s)
                    {

                        if (outputLineSubString[i].Contains(t))
                        {
                            Console.WriteLine("searching for " + t + "...");
                            System.Console.WriteLine(outputLineSubString[i]);
                        }
                    }
                }
            return output;

        }
    }
}
