using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    public class Program
    {

        static void Main(string[] args)
        {
            
            string command = args[0].ToLower();
            string[] searchKeyWords=args.Where(s=>s!= command).ToArray();

            SearchFactory.GetInstance(command).Search(searchKeyWords);
            
            Console.WriteLine(Convert.ToString(command));
            Console.Read();
        }
    }
}
