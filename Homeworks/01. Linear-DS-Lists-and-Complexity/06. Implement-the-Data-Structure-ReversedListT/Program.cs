using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06.Implement_the_Data_Structure_ReversedListT
{
    class Program
    {
        static void Main(string[] args)
        {
            ReversedList<int> reversed = new ReversedList<int>();

            reversed.Add(1);
            reversed.Add(2);
            reversed.Add(3);
            reversed.Add(4);
            reversed.Add(5);

            reversed.RemoveAt(0);

            foreach (var i in reversed)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }
    }

    public class ReversedList<T> : IEnumerable<T>
    {
        private const int InitialCapacity = 2;

        private T[] arr;

        public ReversedList()
        {
            arr = new T[InitialCapacity];
        }

        public int Count { get; set; }

        public int Capacity { get; set; }

        public T this[int index]
        {
            get
            {
                if (index >= this.Count)
                {
                    throw  new ArgumentOutOfRangeException();
                }
                return this.arr[index];
            }
            set
            {
                if (index >= this.Count)
                {
                    throw new ArgumentOutOfRangeException();
                }
                this.arr[index] = value;
            }
        }

        public void Add(T item)
        {
            if (this.Count == this.arr.Length)
            {
                this.Resize();
            }

            arr[this.Count++] = item;
        }

        private void Resize()
        {
            T[] copy = new T[this.arr.Length * 2];

            for (int i = 0; i < this.arr.Length; i++)
            {
                copy[i] = this.arr[i];
            }
            this.arr = copy;
        }

        public void RemoveAt(int index)
        {
            if (index >= this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            //T element = this.arr[index];
            this.arr[index] = default(T);
            this.Shift(index);
            this.Count--;

            if (this.Count <= this.arr.Length / 4)
            {
                this.Shrink();
            }
        }

        private void Shrink()
        {
            T[] copy = new T[this.arr.Length / 2];
            for (int i = 0; i < this.Count; i++)
            {
                copy[i] = this.arr[i];
            }

            this.arr = copy;
        }

        private void Shift(int index)
        {
            for (int i = index; i < this.Count; i++)
            {
                this.arr[i] = this.arr[i+1];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Count-1; i >=0; i--)
            {
                yield return arr[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
