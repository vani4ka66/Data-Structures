using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class Program
    {
        static void Main(string[] args)
        {
            LinkedQueue<int> link = new LinkedQueue<int>();

            link.Enqueue(1);
            link.Enqueue(2);
            link.Enqueue(3);
            link.Enqueue(4);
            link.Enqueue(5);


            link.Dequeue();

            foreach (var i in link.ToArray())
            {
                Console.WriteLine(i);
            }


        }
    }

