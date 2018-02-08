using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayList_T
{
    public class ArrayList<T>
    {
        private const int InitialCapacity = 2;

        private T[] data;

        public int Count { get; set; }

        public ArrayList()
        {
            this.data = new T[InitialCapacity];
        }

        public T this[int index]
        {
            get
            {
                if (index >= this.Count)
                {
                    throw  new ArgumentOutOfRangeException();
                }
                return this.data[index];
            }

            set
            {
                if (index >= this.Count)
                {
                    throw new ArgumentOutOfRangeException();
                }
                this.data[index] = value;
            }
        }

        public void Add(T item)
        {
            if (this.Count == this.data.Length)
            {
                this.Resize();
            }

            this.data[this.Count++] = item;
        }

        private void Resize()
        {
            T[] copy = new T[this.data.Length * 2];

            for (int i = 0; i < this.data.Length; i++)
            {
                copy[i] = this.data[i];
            }
            this.data = copy;
        }

        public T RemoveAt(int index)
        {
            if (index >= this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            T element = this.data[index];
            this.data[index] = default(T);
            this.Shift(index);
            this.Count--;

            if (this.Count <= this.data.Length/4)
            {
                this.Shrink();
            }
            return element;
        }

        private void Shift(int index)
        {
            for (int i = index; i < this.Count; i++)
            {
                this.data[i] = this.data[i + 1];
            }
        }

        private void Shrink()
        {
            T[] copy = new T[this.data.Length / 2];
            for (int i = 0; i < this.Count; i++)
            {
                copy[i] = this.data[i];
            }
            this.data = copy;
        }
    }
}
