using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

public class LinkedList<T> : IEnumerable<T>
{
    public class Node
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }
        public Node Next { get; set; }
        public Node Previous { get; set; }

    }

    public Node Head { get; private set; }
    public Node Tail { get; private set; }
    public int Count { get; private set; }

    public void AddFirst(T item)
    {
        Node old = Head;

        this.Head = new Node(item);
        this.Head.Next = old;

        if (isEmpty())
        {
            Tail = Head;
        }
        Count++;
    }

    public bool isEmpty()
    {
        if (this.Count == 0)
        {
            return true;
        }
        return false;
    }

    public void AddLast(T item)
    {
        Node old = this.Tail;
        this.Tail = new Node(item);

        if (isEmpty())
        {
            this.Head = this.Tail;
        }
        else
        {
            old.Next = this.Tail;
        }

        this.Count++;
    }

    public T RemoveFirst()
    {
        if (isEmpty())
        {
            throw new InvalidOperationException();
        }

        T item = this.Head.Value;

        this.Head = this.Head.Next;

        this.Count--;

        if (isEmpty())
        {
            this.Tail = null;
        }

        return item;
    }

    public T RemoveLast()
    {
        if (isEmpty())
        {
            throw new InvalidOperationException();
        }

        Node oldTail = this.Tail;

        if (this.Count == 1)
        {
            this.Head = this.Tail = null;
        }
        else if (this.Count == 2)
        {
            this.Head = this.Tail = GetSecondToLast();
            this.Head.Next = null;
        }
        else if(this.Count > 2)
        {
            Node newTail = GetSecondToLast();
            this.Tail = newTail;
            newTail.Next = null;
        }

        this.Count--;

        return oldTail.Value;
    }

    private Node GetSecondToLast()
    {
        Node current = this.Head;

        while (current.Next != this.Tail)
        {
            current = current.Next;
        }

        return current;
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node current = this.Head;

        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
