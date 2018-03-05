using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections;

public class Hierarchy<T> : IHierarchy<T>
{
    private Dictionary<T, Node> nodesByValue;

    public Hierarchy(T root)
    {
        this.Root = new Node(root);
        this.nodesByValue = new Dictionary<T, Node>();
        this.nodesByValue.Add(root, this.Root);
        this.Children = new HashSet<T>();
    }

    private Node Root;

    public HashSet<T> Children;

    public class Node
    {
        public Node(T value, Node parent = null)
        {
            this.Value = value;
            this.Children = new List<Node>();
            this.Parent = parent;
        }

        public T Value { get; set; }
        public Node Parent { get; set; }
        public List<Node> Children { get; set; }

        public override string ToString()
        {
            return this.Value + " ";
        }
    }

    public int Count
    {
        get { return this.nodesByValue.Count; }
    }

    public void Add(T element, T child)
    {
        if (!this.nodesByValue.ContainsKey(element))
        {
            throw new ArgumentException();
        }

        if (this.nodesByValue.ContainsKey(child))
        {
            throw new ArgumentException();
        }

        Node parentNode = this.nodesByValue[element];
        Node childNode = new Node(child, parentNode);

        parentNode.Children.Add(childNode);
        this.nodesByValue.Add(child, childNode);
    }

    public void Remove(T element)
    {
        if (!this.nodesByValue.ContainsKey(element))
        {
            throw new ArgumentException();
        }

        if (element.Equals(this.Root.Value))
        {
            throw new InvalidOperationException();
        }

        var current = this.nodesByValue[element];
        if (current.Parent == null)
        {
            throw new ArgumentException();
        }

        foreach (var child in current.Children)
        {
            child.Parent = current.Parent;
            current.Parent.Children.Add(child);
        }

        current.Parent.Children.Remove(current);
        nodesByValue.Remove(element);
    }

    public IEnumerable<T> GetChildren(T element)
    {
        if (!this.nodesByValue.ContainsKey(element))
        {
            throw new ArgumentException();
        }

        Node current = this.nodesByValue[element];

        return current.Children.Select(x=> x.Value);
    }

    public T GetParent(T element)
    {
        if (!this.nodesByValue.ContainsKey(element))
        {
            throw new ArgumentException();
        }

        Node current = this.nodesByValue[element];

        if (current.Parent == null)
        {
            return default(T);
        }
        
        return current.Parent.Value;
    }

    public bool Contains(T element)
    {
        return this.nodesByValue.ContainsKey(element);
    }

    public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
    {
        /*var result = new List<T>();
        foreach (var kvp in nodesByValue)
        {
            if (this.nodesByValue.ContainsKey(kvp.Key))
            {
                result.Add(kvp.Key);
            }
        }
        return result;*/

        HashSet<T> set =  new HashSet<T>(this.nodesByValue.Keys);
        set.IntersectWith(new HashSet<T>(other.nodesByValue.Keys));
        //var result = set.Intersect(other);
        return set;
    }

    public void ForEach(Action<T> action)
    {
        var result = new List<T>();
        result.Add(this.Root.Value);
        result.AddRange(this.Root.Children.Select(x=> x.Value));

        foreach (var i in result)
        {
            action(i);
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        Queue<Node> queue = new Queue<Node>();

        Node current = this.Root;
        queue.Enqueue(current);

        while (queue.Count > 0)
        {
            current = queue.Dequeue();
            yield return current.Value;

            foreach (var child in current.Children)
            {
               queue.Enqueue(child); 
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
