using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06.Sequence_N_M
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> queue = new Queue<int>();

            int[] arr = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int n = arr[0];
            int m = arr[1];

            queue.Enqueue(m);

            if (m >= n)
            {
                
                while (m/2 >= n)
                {
                    m /= 2;
                    queue.Enqueue(m);
                }

                while (m - 2 >= n)
                {
                    m -= 2;
                    queue.Enqueue(m);
                }

                while (m - 1 >= n)
                {
                    m -= 1;
                    queue.Enqueue(m);
                }

                Console.WriteLine(string.Join(" -> ", queue.Reverse()));
            }
        }
    }
}
