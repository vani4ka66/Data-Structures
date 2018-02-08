using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.Linked_Stack
{
    public class LinkedStack<T> 
    {
        private Node<T> firstNode;
        public int Count { get; private set; }

        private class Node<T>
        {
            public T Value;
            public Node<T> Next { get; set; }

            public Node(T value, Node<T> next = null)
            {
                this.Value = value;
            }
        }

        public void Push(T element)
        {
            Node<T> newItem = new Node<T>(element);
            var old = this.firstNode;
            this.firstNode = newItem;
            this.firstNode.Next = old;

            this.Count++;
        }

        public T Pop()
        {
            var oldFirst = this.firstNode;

            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            this.firstNode = this.firstNode.Next;
            this.Count--;

            return oldFirst.Value;
        }

        public T[] ToArray()
        {
            var resultArr = new T[this.Count];
            CopyAllElements(resultArr);
            return resultArr;
        }

        private void CopyAllElements(T[] resultArr)
        {
            for (int i = 0; i < this.Count; i++)
            {
                resultArr[i] = this.firstNode.Value;
                this.firstNode = firstNode.Next;
            }
        }

       
    }
}
