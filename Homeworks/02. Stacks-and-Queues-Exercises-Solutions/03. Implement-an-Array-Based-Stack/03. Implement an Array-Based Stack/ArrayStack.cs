using System;


    public class ArrayStack<T>
    {
            private T[] elements;

            public int Count { get; private set; }
            private const int InitialCapacity = 16;

            public ArrayStack(int capacity = InitialCapacity)
            {
                elements = new T[InitialCapacity];
            }

            public void Push(T element)
            {
                if (this.Count == this.elements.Length)
                {
                    this.Grow();
                }
                elements[this.Count] = element;
                this.Count++;
            }

            public T Pop()
            {
                if (this.Count == 0)
                {
                    throw new InvalidOperationException();
                }
                this.Count--;
                return this.elements[this.Count];
            }

            public T[] ToArray()
            {
                var resultArr = new T[this.Count];
                CopyAllElements(resultArr);
                return resultArr;
            }

            private void Grow()
            {
                T[] newElements = new T[2 * this.elements.Length];
                this.CopyAllElements(newElements);
                this.elements = newElements;
            }

            private void CopyAllElements(T[] newElements)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    newElements[i] = this.elements[i];
                }
            }
        
    }

