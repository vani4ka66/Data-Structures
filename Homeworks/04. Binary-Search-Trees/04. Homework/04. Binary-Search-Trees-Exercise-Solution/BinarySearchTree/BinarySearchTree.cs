using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

public class BinarySearchTree<T> : IBinarySearchTree<T> where T:IComparable
{
    private class Node
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public int Count { get; set; }
    }

    private Node root;

    private Node FindMin(Node node)
    {
        if (node.Left == null)
        {
            return node;
        }

        return this.FindMin(node.Left);
    }

    private Node FindMinNode(Node node)
    {
        Node current = node;

            while (current.Left!= null)
            {
                current = current.Left;
            }

        return current;
    }

    private Node FindElement(T element)
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

        return current;
    }

    private void PreOrderCopy(Node node)
    {
        if (node == null)
        {
            return;
        }

        this.Insert(node.Value);
        this.PreOrderCopy(node.Left);
        this.PreOrderCopy(node.Right);
    }

    public void Insert(T element)
    {
        this.root = this.Insert(element, this.root);
    }

    private Node Insert(T element, Node node)
    {
        if (node == null)
        {
            node = new Node(element);
            node.Count++;
        }
        else if (element.CompareTo(node.Value) < 0 )
        {
            node.Left = this.Insert(element, node.Left);
        }
        else if (element.CompareTo(node.Value) > 0)
        {
            node.Right = this.Insert(element, node.Right);
        }

        node.Count = 1 + this.Count(node.Right) + this.Count(node.Left);
        return node;
    }

    private void Range(Node node, Queue<T> queue, T startRange, T endRange)
    {
        if (node == null)
        {
            return;
        }

        int nodeInLowerRange = startRange.CompareTo(node.Value);
        int nodeInHigherRange = endRange.CompareTo(node.Value);

        if (nodeInLowerRange < 0)
        {
            this.Range(node.Left, queue, startRange, endRange);
        }
        if (nodeInLowerRange <= 0 && nodeInHigherRange >= 0)
        {
            queue.Enqueue(node.Value);
        }
        if (nodeInHigherRange > 0)
        {
            this.Range(node.Right, queue, startRange, endRange);
        }
    }
    
    private BinarySearchTree(Node node)
    {
        this.PreOrderCopy(node);
    }

    public BinarySearchTree()
    {
    }
    
    public bool Contains(T element)
    {
        Node current = this.FindElement(element);

        return current != null;
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

    public BinarySearchTree<T> Search(T element)
    {
        Node current = this.FindElement(element);

        return new BinarySearchTree<T>(current);
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        Queue<T> queue = new Queue<T>();

        this.Range(this.root, queue, startRange, endRange);

        return queue;
    }

    public void DeleteMin()
    {
  	if (this.root == null)
        {
            throw new InvalidOperationException();
        }

        this.root = this.DeleteMin(this.root);
    }

    private Node DeleteMin(Node node)
    {
        if (node.Left == null)
        {
            return node.Right;
        }

        node.Left = this.DeleteMin(node.Left);
        node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

        return node;

        /*if (this.root == null)
        {
            return;
        }

        Node current = this.root;
        Node parent = null;
        while (current.Left != null)
        {
            parent = current;
            current = current.Left;
        }

        if (parent == null)
        {
            this.root = this.root.Right;
        }
        else
        {
            parent.Left = current.Right;
        }*/
    }

    public void Delete(T element)
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }

        this.root = this.Delete(element, this.root);
    }

    private Node Delete(T element, Node node)
    {
        if (node == null)
        {
            return null;
        }

        int compare = element.CompareTo(node.Value);

        if (compare < 0)
        {
            node.Left = this.Delete(element, node.Left);
        }
        else if (compare > 0)
        {
            node.Right = this.Delete(element, node.Right);
        }
        else
        {
            if (node.Right == null)
            {
                return node.Left;
            }
            if (node.Left == null)
            {
                return node.Right;
            }

            Node temp = node;
            node = this.FindMin(temp.Right);
            node.Right = this.DeleteMin(temp.Right);
            node.Left = temp.Left;

        }
        node.Count = this.Count(node.Left) + this.Count(node.Right) + 1;

        return node;
    }

/*private Node Delete(T element, Node node)
    {
        if (node == null)
        {
            return null;
        }

        int compare = element.CompareTo(node.Value);

        if (compare < 0)
        {
            node.Left = this.Delete(element, node.Left);
        }
        if (compare > 0)
        {
            node.Right = this.Delete(element, node.Right);
        }
        if (compare == 0)
        {
            //No child nodes
            if (node.Left == null && node.Right == null)
            {
                node = null;
                return node;
            }

            //No left child
            if (node.Left == null)
            {
                Node temp = node;
                node = node.Right;
                temp = null;
            }
            //No right child
            else if (node.Right == null)
            {
                Node temp = node;
                node = node.Left;
                temp = null;
            }
            //Has both child nodes
            else if(node.Right != null && node.Left != null)
            {
                Node min = FindMinNode(node.Right);   //4
                var left = node.Left;
                node = min;
                node.Left = left;
                node.Right = this.Delete(min.Value, node.Right);
            }
        }
        return node;
    }*/

    public void DeleteMax()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }

        this.root = this.DeleteMax(this.root);
    }

    private Node DeleteMax(Node node)
    {
        if (node.Right == null)
        {
            return node.Left;
        }

        node.Right = this.DeleteMax(node.Right);
        node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

        return node;

        /*if (this.root == null)
        {
            return;
        }

        Node current = this.root;
        Node parent = null;
        while (current.Right != null)
        {
            parent = current;
            current = current.Right;
        }

        if (parent == null)
        {
            this.root = this.root.Left;
        }
        else
        {
            parent.Right = current.Left;
        }*/
    }

    public int Count()
    {
        return this.Count(this.root.Left) + this.Count(this.root.Right) + 1;
    }

    private int Count(Node node)
    {
        if (node == null)
        {
            return 0;
        }

        return node.Count;
    }

    public int Rank(T element)
    {
        var count = this.Rank(element, this.root);

        return count;
    }

    private int Rank(T element, Node node)
    {
        if (node == null)
        {
            return 0;
        }

        int compare = element.CompareTo(node.Value);

        if (compare < 0)
        {
            return this.Rank(element, node.Left);
        }

        if (compare > 0)
        {
            return 1 + this.Count(node.Left) + this.Rank(element, node.Right);

        }

        return this.Count(node.Left);

    }

    public T Select(int rank)
    {
        Node node = this.Select(rank, this.root);
        if (node == null)
        {
            throw new InvalidOperationException();
        }

        return node.Value;
    }

    private Node Select(int rank, Node node)
    {
        if (node == null)
        {
            return null;
        }

        int leftCount = this.Count(node.Left);
        if (leftCount == rank)
        {
            return node;
        }

        if (leftCount > rank)
        {
            return this.Select(rank, node.Left);
        }
        else
        {
            return this.Select(rank - (leftCount + 1), node.Right);
        }
    }

    /*public T Select(int rank) //също е вярно!!!
    {
        var searchedElement = this.Select(rank, this.root);

        return searchedElement;
    }

    private T Select(int rank, Node node)  //също е вярно!!!
    {
        Queue<T> queue = new Queue<T>();

        BinarySearchTree<T> tree = new BinarySearchTree<T>(node);
        tree.EachInOrder(queue.Enqueue);

        for (int i = 0; i < rank; i++)
        {
            queue.Dequeue();
        }


        return queue.Peek();
    }*/

    public T Floor(T element)
    {
        return this.Select(this.Rank(element) - 1);
    }

    /*public T Floor(T element)
    {
        var result = this.Floor(element, this.root, null);

        return result;
    }

    private T Floor(T element, Node node, Node parent)
    {
        int compare = element.CompareTo(node.Value);

        if (compare < 0)
        {
            return this.Floor(element, node.Left, node);
        }
        if (compare > 0)
        {
            return this.Floor(element, node.Right, node);
        }
        if (compare == 0)
        {
            if (node.Left != null)
            {
                node = node.Left;
                if (node.Right != null)
                {
                    while (node.Right != null)
                    {
                        parent = node.Right;
                        node = node.Right;
                    }
                }
                else
                {
                    parent = node;
                }
            }
        }
        return parent.Value;
    }*/

    public T Ceiling(T element)
    {
        return this.Select(this.Rank(element) + 1);
    }

    /*public T Ceiling(T element)
    {
        var result = this.Ceiling(element, this.root, null);
        return result;
    }

    private T Ceiling(T element, Node node, Node parent)
    {
        int compare = element.CompareTo(node.Value);

        if (compare < 0)
        {
            parent = node;
            return this.Ceiling(element, node.Left, parent);
        }
        if (compare > 0)
        {
            return this.Ceiling(element, node.Right, parent);
        }
        if (compare == 0)
        {
            if (node.Right != null)
            {
                parent = node;
                node = node.Right;
            }
            else
            {
                node = parent;
            }
        }

        return node.Value;
    }*/

   
}

public class Launcher
{
    public static void Main(string[] args)
    {
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        bst.DeleteMin();

        //1
        bst.DeleteMax();

        //2
        int nodeCount = bst.Count();
        Console.WriteLine("Count: " + nodeCount);

        //Console.WriteLine("---------------");
        bst.EachInOrder(Console.WriteLine);

        //3
        int rank = bst.Rank(8);
        Console.WriteLine("Rank: " + rank);

        //4
        int ranks = bst.Select(4);
        Console.WriteLine("Select: " + ranks);

        //5
        int floor = bst.Floor(10);
        Console.WriteLine("Floor: " + floor);

        //6
        int ceiling = bst.Ceiling(10);
        Console.WriteLine("Ceiling: " + ceiling);

        //7
        bst.Delete(10);
        //Console.WriteLine("**************");
        //Console.WriteLine("After Delete");
        //bst.EachInOrder(Console.WriteLine);

        int count2 = bst.Count();
        Console.WriteLine("Count: " + count2);
    }
}