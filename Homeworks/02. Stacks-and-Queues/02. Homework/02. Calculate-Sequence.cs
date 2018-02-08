using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculate_Sequence
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> queue = new Queue<int>();
            List<int> list = new List<int>();

            int n = int.Parse(Console.ReadLine());

            int s1 = n;
            queue.Enqueue(s1);
            list.Add(s1);

            while (list.Count <= 50)
            {
                int s2 = s1 + 1;
                queue.Enqueue(s2);

                int s3 = 2*s1 + 1;
                queue.Enqueue(s3);

                int s4 = s1 + 2;
                queue.Enqueue(s4);

                list.Add(s2);
                list.Add(s3);
                list.Add(s4);

                queue.Dequeue();
                s1 = queue.Peek();

            }

            int count = 1;
            foreach (var i in list)
            {
                if (count < 50)
                {
                    Console.Write(i + ", ");
                    count++;
                }
                else if (count == 50)
                {
                    Console.WriteLine(i);
                    count++;
                }
                else
                {
                    break;
                }
               
            }
        }
    }

   /* private class Queue<T> : IEnumerable<T>
    {
       
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    } */
}
