using System;

    class Program
    {
        static void Main(string[] args)
        {
            ArrayStack<int> arr = new ArrayStack<int>();
            arr.Push(1);
            arr.Push(5);
            arr.Push(6);

            Console.WriteLine(arr.Count);
            //arr.Pop();

            foreach (var i in arr.ToArray())
            {
                Console.WriteLine(i);
            }

        }
    }

