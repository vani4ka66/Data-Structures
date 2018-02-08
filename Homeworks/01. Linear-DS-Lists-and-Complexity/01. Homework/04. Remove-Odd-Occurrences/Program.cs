using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.Remove_Odd_Occurrences
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list =
                Console.ReadLine()
                    .Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)
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

            List<int> result = new List<int>();

            foreach (var kvp in map)
            {
                if (kvp.Value %2 != 0)
                {
                    list.RemoveAll(x => x ==kvp.Key);
                }
            }

            Console.WriteLine(string.Join(" ", list));
        }
    }
}
