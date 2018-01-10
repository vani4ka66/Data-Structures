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
        }
    }

    public class ReversedList<T>
    {
        private readonly List<T> list;

        public ReversedList()
        {
            list = new List<T>();
        }

        public int Count()
        {
            return this.list.Count();
        }

        public int Capacity()
        {
            return this.Capacity();
        }

        public void Add(T item)
        {
            this.list.Add(item);
        }

        public void RemoveAt(int index)
        {
            if (this.list.Count == 0)
            {
                throw new InvalidOperationException("List empty");
            }
            else if (index >= this.list.Count)
            {
                throw new InvalidOperationException("Index is outside of the bound of an array!");
            }
            
            else
            {
                this.list.RemoveAt(index);
            }

        }
    }
}
