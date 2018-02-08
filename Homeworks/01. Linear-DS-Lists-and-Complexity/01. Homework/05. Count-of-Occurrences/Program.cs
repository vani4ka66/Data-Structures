using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05.Count_of_Occurrences
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list =
               Console.ReadLine()
                   .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                   .Select(x => int.Parse(x))
                   .ToList();

            Dictionary<int, int> map = new Dictionary<int, int>();

            foreach (var ch in list)
            {
                if (!map.ContainsKey(ch))
                {
                    map.Add(ch, 0);
                }
                map[ch]++;
            }

            foreach (var k in map.OrderBy(x=> x.Key))
            {
                Console.WriteLine($"{k.Key} -> {k.Value} times");
            }
        }
    }
}
