using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Longest_Subsequence
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(x=> int.Parse(x)).ToList();

            List<int> result = new List<int>();

            int count = 1;
            int maxCount = 0;
            int number = 0;

            for (int i = 0; i < list.Count - 1; i++)
            {
                int current = list[i];
                int next = list[i + 1];

                if (current == next)
                {
                    count++;

                    for (int j = i+1; j < list.Count - 1; j++)
                    {
                        if (list[j] == list[j+1])
                        {
                            count++;
                            i = j + 1;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                if (count > maxCount)
                {
                    maxCount = count;
                    count = 1;
                    number = current;

                }
            }

            for (int i = 0; i < maxCount; i++)
            {
                result.Add(number);
            }

            if (result.Count == 0)
            {
                Console.WriteLine(list[0]);
            }
            else
            {
            Console.WriteLine(string.Join(" ", result));

            }
        }
    }
}
