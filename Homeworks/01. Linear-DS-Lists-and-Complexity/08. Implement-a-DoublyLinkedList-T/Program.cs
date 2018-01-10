using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08. Implement-a-DoublyLinkedList-T
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new DoublyLinkedList<int>();

            list.ForEach(Console.WriteLine);
            Console.WriteLine("--------------------");

            list.AddLast(5);
            list.AddFirst(3);
            list.AddFirst(2);
            list.AddLast(10);
            list.AddLast(20);

            Console.WriteLine("Count = {0}", list.Count);                   //2 3 5 10 20

          list.ForEach(Console.WriteLine);
          Console.WriteLine("--------------------");
          
          list.RemoveFirst();
          list.RemoveLast();
          list.RemoveFirst();                                               //5 10
          
          list.ForEach(Console.WriteLine);
          Console.WriteLine("-------------------");
          Console.WriteLine("Count = {0}", list.Count);


          list.RemoveLast();                                                  //5
           
          list.ForEach(Console.WriteLine);
          Console.WriteLine("--------------------");
            Console.WriteLine("Count = {0}", list.Count);

            list.RemoveLast();                                               //5 10

            list.ForEach(Console.WriteLine);

        }
    }

    public class DoublyLinkedList<T> : IEnumerable<T>
    {
        private class ListNode<T>
        {
            public T Value { get; set; }

            public ListNode<T> NextNode { get; set; }

            public ListNode<T> PrevNode { get; set; }

            public ListNode(T value)
            {
                this.Value = value;
            }
        }

        private ListNode<T> head;

        private ListNode<T> tail;


        public int Count { get; private set; }

        public void AddFirst(T element)
        {
            if (this.Count == 0)
            {
                this.head = this.tail = new ListNode<T>(element);
            }
            else
            {
                var newHead = new ListNode<T>(element);
                newHead.NextNode = this.head;
                this.head.PrevNode = newHead;
                this.head = newHead;
            }
            this.Count++;
        }

        public void AddLast(T element)
        {
            if (this.Count == 0)
            {
                this.head = this.tail = new ListNode<T>(element);
            }
            else
            {
                var newTail = new ListNode<T>(element);
                newTail.PrevNode = this.tail;
                this.tail.NextNode = newTail;
                this.tail = newTail;
            }
            this.Count++;
        }

        public T RemoveFirst()
        {
            var element = this.head.Value;
            if (this.Count == 0)
            {
                throw new InvalidOperationException("List empty");
            }
            else if (this.Count == 2)
            {
                this.head = this.tail = head.NextNode;
            }
            else if (this.Count == 1)
            {
                //this.head = null;
                this.tail = null;
            }
            else if (this.Count > 2)
            {
                this.head = head.NextNode;
                this.head.PrevNode = null;
            }

            this.Count--;

            return element ;
        }

        public T RemoveLast()
        {
            var element = this.tail.Value;

            if (this.Count == 2)
            {
                this.head = this.tail = tail.PrevNode;
                this.head.NextNode = null;

            }
            else if (this.Count == 1)
            {
                this.head = null;
                this.tail = null;
            }
            else if(this.Count > 2)
            {
                this.tail = tail.PrevNode;
                this.tail.NextNode = null;
            }

            this.Count--;

            return element;
        }

        public void ForEach(Action<T> action)
        {
            var currentNode = this.head;
            while (currentNode != null)
            {
                action(currentNode.Value);
                currentNode = currentNode.NextNode;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = this.head;
            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.NextNode;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public T[] ToArray()
        {
            throw new ArgumentException();
        }
    }
}
