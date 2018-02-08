using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _01.Linear_DS_Lists_and_Complexity
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            List<int> list =
                input.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x)).ToList();

            int sum = 0;

            foreach (var i in list)
            {
                sum += i;
            }
            double avg = sum;

            if (list.Count != 0)
            {  
                avg/= list.Count;
            }
            else
            {
                sum = 0;
                avg = 0;
            }

            Console.WriteLine($"Sum={sum}; Average={avg:f2}");
        }
    }
}
