using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public class QuadTree<T> where T : IBoundable
{
    public const int DefaultMaxDepth = 5;

    public readonly int MaxDepth;

    private Node<T> root;

    public QuadTree(int width, int height, int maxDepth = DefaultMaxDepth)
    {
        this.root = new Node<T>(0, 0, width, height);
        this.Bounds = this.root.Bounds;
        this.MaxDepth = maxDepth;
    }

    public int Count { get; private set; }

    public Rectangle Bounds { get; private set; }

    public void ForEachDfs(Action<List<T>, int, int> action)
    {
        this.ForEachDfs(this.root, action);
    }

    public bool Insert(T item)
    {
        //if item is outside quadtree bounds => cannot add
        if (!item.Bounds.IsInside(this.Bounds))
        {
            return false;
        }

        int dept = 1;

        var node = this.root;

        while (node.Children != null)
        {
            int quadrant = GetQuadrant(node, item.Bounds);

            if (quadrant == -1)
            {
                break;
            }

            node = node.Children[quadrant];
            dept++;
        }

        node.Items.Add(item);
        TrySplit(node, dept);
        this.Count++;

        return true;
    }

    private  static int GetQuadrant(Node<T> node, Rectangle bounds)
    {
        /*int result = -1;
        var verticalMidpoint = node.Bounds.MidX;
        var horizontalMidpoint = node.Bounds.MidY;

        var inTopQuadrant = node.Bounds.Y1 <= bounds.Y1 && bounds.Y2 <= horizontalMidpoint;
        var inBottomQuadrant = horizontalMidpoint <= bounds.Y1 && bounds.Y2 <= node.Bounds.Y2;

        //???
        var inLeftQuadrant = node.Bounds.X1 <= bounds.X1 && bounds.X2 <= verticalMidpoint;
        var inRightQuadrant = verticalMidpoint <= bounds.X1 && bounds.X2 <= node.Bounds.X2;

        if (inLeftQuadrant)
        {
            if (inTopQuadrant)
            {
                result =  1;
            }
            else if (inBottomQuadrant)
            {
                result = 2;
            }
        }
        else if (inRightQuadrant)
        {
            if (inTopQuadrant)
            {
                result = 0;
            }
            else if (inBottomQuadrant)
            {
                result = 3;
            }
        }*/


        if (node.Children != null)
        {
            if (bounds.IsInside(node.Children[0].Bounds)) return 0;
            if (bounds.IsInside(node.Children[1].Bounds)) return 1;
            if (bounds.IsInside(node.Children[2].Bounds)) return 2;
            if (bounds.IsInside(node.Children[3].Bounds)) return 3;
        }
        return -1;
    }

    private void TrySplit(Node<T> node, int depth)
    {
        if (!(node.ShouldSplit && depth < MaxDepth))
        {
            return;
        }

        var leftWidth = node.Bounds.Width/2;
        var rightWidth = node.Bounds.Width - leftWidth;
        var topHeight = node.Bounds.Height/2;
        var bottomHeight = node.Bounds.Height - topHeight;

        node.Children = new Node<T>[4];
        node.Children[0] = new Node<T>(node.Bounds.MidX, node.Bounds.Y1, rightWidth, topHeight);
        node.Children[1] = new Node<T>(node.Bounds.X1, node.Bounds.Y1, leftWidth, topHeight);
        node.Children[2] = new Node<T>(node.Bounds.X1, node.Bounds.MidX, leftWidth, bottomHeight);
        node.Children[3] = new Node<T>(node.Bounds.MidX, node.Bounds.MidY, rightWidth, bottomHeight);

        var toRemove = new HashSet<T>();

        //Transfer items from parent to new nodes
        for (int i = 0; i < node.Items.Count; i++)
        {
            var item = node.Items[i];
            var quadrant = GetQuadrant(node, item.Bounds);

            if (quadrant != -1)
            {
                node.Children[quadrant].Items.Add(item);
                toRemove.Add(item);
            }
        }

        node.Items.RemoveAll(x => toRemove.Contains(x));

        //in case all items from parent go to the same node => attemp to split recursively;
        foreach (var child in node.Children)
        {
            TrySplit(child, depth + 1);
        }
    }

    public List<T> Report(Rectangle bounds)
    {
        var result = new List<T>();

        GetCollisionCandidates(this.root, bounds, result);

        return result;
    }

    private void GetCollisionCandidates(Node<T> node, Rectangle bounds, List<T> result)
    {
        var quadrant = GetQuadrant(node, bounds);

        if (quadrant == -1)
        {
            //bounds does not fit in any sub-quadrant => return all items in current subtree;
            GetSubtreeContent(node, bounds, result);
        }
        else
        {
            if (node.Children != null)
            {
                result.AddRange(node.Items);
                GetCollisionCandidates(node.Children[quadrant], bounds, result);
            }
        }

    }

    private void GetSubtreeContent(Node<T> node, Rectangle bounds, List<T> result)
    {

        if (node.Children != null)
        {
            foreach (var child in node.Children)
            {
                if (child.Bounds.Intersects(bounds))
                {
                    GetSubtreeContent(child, bounds, result);
                }
            }
        }

        result.AddRange(node.Items);
    }

    private void ForEachDfs(Node<T> node, Action<List<T>, int, int> action, int depth = 1, int quadrant = 0)
    {
        if (node == null)
        {
            return;
        }

        if (node.Items.Any())
        {
            action(node.Items, depth, quadrant);
        }

        if (node.Children != null)
        {
            for (int i = 0; i < node.Children.Length; i++)
            {
                ForEachDfs(node.Children[i], action, depth + 1, i);
            }
        }
    }
}
