using System;
using System.Collections.Generic;

public class BinarySearchTree<T> where T : IComparable<T>
{
    public BinarySearchTree()
    {
        
    }

    private BinarySearchTree(Node node)
    {
        this.Copy(node);
    }

    private Node root;

    private class Node
    {
        public Node(T value)
        {
            Value = value;
        }
        public T Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    public void Insert(T element)
    {
        if (this.root == null)
        {
            this.root = new Node(element);
            return;
        }

        Node parent = null;
        Node current = this.root;

        while (current != null)
        {
            parent = current;

            if (current.Value.CompareTo(element) > 0)
            {
                current = current.Left;
            }
            else if (current.Value.CompareTo(element) < 0)
            {
                current = current.Right;
            }
            else
            {
                return;
            }
        }

        current = new Node(element);
        if (element.CompareTo(parent.Value) < 0)
        {
            parent.Left = current;
        }
        else
        {
            parent.Right = current;
        }

    }

    public bool Contains(T element)
    {
        Node current = this.root;

        while (current != null)
        {

            if (current.Value.CompareTo(element) > 0)
            {
                current = current.Left;
            }
            else if (current.Value.CompareTo(element) < 0)
            {
                current = current.Right;
            }
            else
            {
                break;
            }
        }
        return current != null;
    }

    public void DeleteMin()
    {
        if (this.root == null)
        {
            return;
        }
        Node parent = null;
        Node current = this.root;

        while (current.Left != null)
        {
            parent = current;
            current = current.Left;
        }

        if (parent == null)
        {
            this.root = current.Right;
        }
        else
        {
            parent.Left = current.Right;
        }
    }

    public BinarySearchTree<T> Search(T element)
    {
        Node current = this.root;
        while (current != null)
        {

            if (current.Value.CompareTo(element) > 0)
            {
                current = current.Left;
            }
            else if (current.Value.CompareTo(element) < 0)
            {
                current = current.Right;
            }
            else
            {
                break;
            }
        }

        return new BinarySearchTree<T>(current);
    }

    private void Copy(Node node)
    {
        if (node == null)
        {
            return;
        }
        this.Insert(node.Value);
        this.Copy(node.Left);
        this.Copy(node.Right);
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        Queue<T> queue = new Queue<T>();

        this.Range(this.root, queue, startRange, endRange);

        return queue;
    }

    private void Range(Node node, Queue<T> queue, T startRange, T endRange)
    {
        if (node == null)
        {
            return;
        }

        int compareLow = startRange.CompareTo(node.Value);
        int compareHigh = endRange.CompareTo(node.Value);

        if (compareLow < 0)
        {
            this.Range(node.Left, queue, startRange, endRange);
        }

        if (compareLow <= 0 && compareHigh >= 0)
        {
            queue.Enqueue(node.Value);
        }

        if (compareHigh > 0)
        {
            this.Range(node.Right, queue, startRange, endRange);

        }
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    private void EachInOrder(Node node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);

    }
}

public class Launcher
{
    public static void Main(string[] args)
    {
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        bst.Insert(10);
        bst.Insert(37);
        bst.Insert(5);
        bst.Insert(8);
        bst.Insert(8);

        bst.EachInOrder(Console.WriteLine);

        Console.WriteLine(bst.Contains(10));

        BinarySearchTree<int> search = bst.Search(5);
        search.EachInOrder(Console.WriteLine);
        search.Insert(50);
        Console.WriteLine("--------");
        search.EachInOrder(Console.WriteLine);
        Console.WriteLine("**********");
        bst.EachInOrder(Console.WriteLine);

        bst.DeleteMin();
        Console.WriteLine("-------");
        bst.EachInOrder(Console.WriteLine);


        Console.WriteLine("+++++++++++");
        foreach (var i in bst.Range(8, 50))
        {
            Console.WriteLine(i);
        }




    }
}
