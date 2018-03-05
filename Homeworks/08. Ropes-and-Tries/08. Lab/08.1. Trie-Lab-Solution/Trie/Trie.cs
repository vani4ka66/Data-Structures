using System;
using System.Collections.Generic;

public class Trie<Value>
{
    private Node root;

    private class Node
    {
        public Value value;
        public bool isTerminal;
        public Dictionary<char, Node> next = new Dictionary<char, Node>();
    }

    public Value GetValue(string key)
    {
        var x = GetNode(root, key, 0);
        if (x == null || !x.isTerminal)
        {
            throw new InvalidOperationException();
        }

        return x.value;
    }

    public bool Contains(string key)
    {
        var node = GetNode(this.root, key, 0);
        return node != null && node.isTerminal;
    }

    public void Insert(string key, Value val)
    {
        root = Insert(root, key, val, 0);
    }

    private Node Insert(Node newNode, string key, Value val, int d)
    {
        if (newNode == null)
        {
            newNode = new Node();
        }
        if (d == key.Length)
        {
            newNode.value = val;
            newNode.isTerminal = true;

            return newNode;
        }

        Node node = null;
        char c = key[d];

        if (newNode.next.ContainsKey(c))
        {
            node = newNode.next[c];
        }

        newNode.next[c] = this.Insert(node, key, val, d + 1);

        return newNode;
    }

    public IEnumerable<string> GetByPrefix(string prefix)
    {
        var results = new Queue<string>();
        var x = GetNode(root, prefix, 0);

        this.Collect(x, prefix, results);
        
        return results;
    }

    private Node GetNode(Node x, string key, int d)
    {
        if (x == null)
        {
            return null;
        }

        if (d == key.Length)
        {
            return x;
        }

        Node node = null;
        char c = key[d];

        if (x.next.ContainsKey(c))
        {
            node = x.next[c];
        }

        return GetNode(node, key, d + 1);
    }

    private void Collect(Node x, string prefix, Queue<string> results)
    {
        if (x == null)
        {
            return;
        }

        if (x.value != null && x.isTerminal)
        {
            results.Enqueue(prefix);
        }

        foreach (var c in x.next.Keys)
        {
            Collect(x.next[c], prefix + c, results);
        }
    }
}