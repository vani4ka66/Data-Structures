using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Tree<T>
{
    public Tree(T value, params Tree<T>[] children)
    {
        Value = value;
        Children = new List<Tree<T>>(children);
    }

    public T Value { get; set; }
    public Tree<T> Parent { get; set; }
    public List<Tree<T>> Children { get; private set; }

    public void PrintTree(int indent = 0)
    {
        Console.Write(new string(' ', indent));
        Console.WriteLine(this.Value);

        foreach (var child in this.Children)
        {
            child.PrintTree(indent + 2);
        }
    }
}

   

    

