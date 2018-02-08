using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayList_T
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList<int> list = new ArrayList<int>();
            list.Add(5);
            list.Add(5);
            list.Add(5);

            list[0] = list[0] + 1;
            int element = list.RemoveAt(0);

            Console.WriteLine(element);
        }
    }
}
