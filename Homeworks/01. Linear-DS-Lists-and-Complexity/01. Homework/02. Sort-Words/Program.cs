using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02.Sort_Words
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            List<string> list =
                input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            list = list.OrderBy(x => x).ToList();

            Console.WriteLine(string.Join(" ", list));
        }
    }
}
