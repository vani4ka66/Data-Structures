using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class LinkedQueue<T> : IEnumerable<T>
    {
        private class Node<T>
        {
            public T Value { get; private set; }
            public Node<T> Next { get; set; }
            public Node<T> Prev { get; set; }

            public Node(T value)
            {
                this.Value = value;
            }
        }

        //private Node<T> Head;
        //private Node<T> Tail;

        private Node<T> firstNode;

        public int Count { get; private set; }

        public void Enqueue(T element)
        {
            Node<T> newTail = new Node<T>(element);

            if (this.Count == 0)
            {
                this.firstNode = newTail;
                this.firstNode.Prev = null;
                this.firstNode.Next = null;

            }
            else
            {
                //var old = this.firstNode;
                //this.firstNode = newTail;
                //this.firstNode.Next = old;
                Node<T> last = GetLastNode();
                last.Next = newTail;

            }
            
            this.Count++;
        }

        private Node<T> GetLastNode()
        {
            Node<T> current = this.firstNode;

            while (current.Next != null)
            {
                current = current.Next;
            }

            return current;

        }

        public T Dequeue()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            Node<T> oldHead = this.firstNode;
            this.firstNode = this.firstNode.Next;
            this.Count--;

            return oldHead.Value;
        }

        public T[] ToArray()
        {
            T[] resultArr = new T[this.Count];
            CopyAllElements(resultArr);

            return resultArr;
        }

        private void CopyAllElements(T[] resultArr)
        {
            for (int i = 0; i < this.Count; i++)
            {
                resultArr[i] = this.firstNode.Value;
                this.firstNode = this.firstNode.Next;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = this.firstNode;
            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.Next;
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    
}
