using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WorkExam
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> arr = new Stack<int>();

            string[] input = Console.ReadLine().Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < input.Length; i++)
            {
                var current = int.Parse(input[i]);
                arr.Push(current);
            }

            arr.Reverse();
            Console.WriteLine(string.Join(" ", arr.ToArray()));
        }
    }
}
