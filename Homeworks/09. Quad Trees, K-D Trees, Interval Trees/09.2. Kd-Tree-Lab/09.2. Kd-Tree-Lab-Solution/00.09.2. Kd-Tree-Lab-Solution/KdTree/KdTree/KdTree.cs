using System;

public class KdTree
{
    private Node root;

    public class Node
    {
        public Node(Point2D point)
        {
            this.Point = point;
        }

        public Point2D Point { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    public Node Root
    {
        get
        {
            return this.root;
        }
    }

    public bool Contains(Point2D point)
    {
        var node = Search(this.root, point, 0);

        return node != null;
    }

    private Node Search(Node node, Point2D point, int depth)
    {
        if (node == null)
        {
            return null;
        }

        int compare = Compare(point, node.Point, depth);

        if (compare < 0)
        {
            return Search(node.Left, point, depth + 1);
        }
        else if (compare > 0)
        {
            return Search(node.Right, point, depth + 1);
        }

        return node;
    }

    public void Insert(Point2D point)
    {
        this.root = Insert(this.root, point, 0);
    }

    private Node Insert(Node node, Point2D point, int depth)
    {
        if (node == null)
        {
            return new Node(point);
        }
        int compare = Compare(point, node.Point, depth);

        if (compare < 0)
        {
            node.Left = Insert(node.Left, point, depth + 1);
        }
        else if (compare > 0)
        {
            node.Right = Insert(node.Right, point, depth + 1);
        }

        return node;
    }

    public void EachInOrder(Action<Point2D> action)
    {
        this.EachInOrder(this.root, action);
    }

    private void EachInOrder(Node node, Action<Point2D> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Point);
        this.EachInOrder(node.Right, action);
    }

    private int Compare(Point2D a, Point2D b, int depth)
    {
        int compare = 0;

        if (depth % 2 == 0)
        {
            compare = a.X.CompareTo(b.X);

            if (compare == 0)
            {
                compare = a.Y.CompareTo(b.Y);
            }
        }
        else
        {
            compare = a.Y.CompareTo(b.Y);

            if (compare == 0)
            {
                compare = a.X.CompareTo(b.X);
            }
        }

        return compare;
    }
}
