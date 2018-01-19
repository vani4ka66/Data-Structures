using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace _03._1.Basic_Trees_Lab
{
    class Program
    {
        static readonly Dictionary<int, Tree<int>> nodeByValue = new Dictionary<int, Tree<int>>();

        static void Main(string[] args)
        {
            ReadTree();
            int path = int.Parse(Console.ReadLine());
            int sum = int.Parse(Console.ReadLine());

            var root = GetRootNode();
            //1
            Console.WriteLine("Root Node: " + root.Value);

            //2
            root.PrintTree();

            //3
            Console.WriteLine("Leaf nodes: " + string.Join(" ", Leafs()));

            //4
            PrintMiddleNodes();

            //5
            //Console.Write("Deepest node: ");
            //DeepestPath();

            //6
            //Console.Write("Longest path: ");
            //LongestPath();

            //7
            //Console.WriteLine("Paths of sum {0}: ", path);
            //AllPathsSum(path);

            //8
            Console.Write("Subtrees of sum {0}: ", sum);
            Sum(sum);

           

           
           

        }

        static void ReadTree()
        {
            int nodeCount = int.Parse(Console.ReadLine());

            for (int i = 1; i < nodeCount; i++)
            {
                int[] edge = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                AddEdge(edge[0], edge[1]);
            }
        }

        public static void AddEdge(int parent, int child)
        {
            Tree<int> parentNode = GetTreeNodeByValue(parent);
            Tree<int> childNode = GetTreeNodeByValue(child);

            parentNode.Children.Add(childNode);
            childNode.Parent = parentNode;
        }

        static Tree<int> GetTreeNodeByValue(int value)
        {
            if (!nodeByValue.ContainsKey(value))
            {
                nodeByValue[value] = new Tree<int>(value);
            }

            return nodeByValue[value];
        }
        
        static Tree<int> GetRootNode()
        {
            return nodeByValue.Values.FirstOrDefault(x => x.Parent == null);
        }

        static List<int> Leafs()
        {
            var nodes =  nodeByValue.Values
                .Where(x => x.Children.Count == 0)
                .Select(x => x.Value)
                .OrderBy(x => x)
                .ToList();

            return nodes;
        }

        static void PrintMiddleNodes()
        {
            var nodes = nodeByValue.Values
                .Where(x => x.Parent != null && x.Children.Count != 0)
                .Select(x => x.Value)
                .OrderBy(x => x)
                .ToList();

            Console.WriteLine("Middle nodes: " + string.Join(" ", nodes));
        }

        static void Sum(int sum1)
        {
            var nodes = nodeByValue.Values
                .Where(x => x.Parent != null && x.Children.Count != 0).ToList();

            foreach (var node in nodes)
            {
                int sum = node.Children.Select(x => x.Value).Sum(x => x) + node.Value;
                if (sum == sum1)
                {
                    Console.WriteLine(node.Value + " + " +  string.Join(" + ", node.Children.Select(x => x.Value)));
                }
            }
        }

        static void DeepestPath()
        {
            Dictionary<int, int> map1 = new Dictionary<int, int>();

            var nodes = nodeByValue.Values
               .Where(x => x.Children.Count == 0)
               .ToList();

            foreach (var node in nodes)
            {
                int count = 0;

                while (node.Parent != null)
                {
                    count++;
                    node.Parent = node.Parent.Parent;
                }

                if (!map1.ContainsKey(node.Value))
                {
                    map1[node.Value] = count;
                }
            }

            var map2 = map1.OrderByDescending(x => x.Value);

            foreach (var key in map2)
            {
                Console.WriteLine(key.Key);
                break;;
            }

        }

        static void LongestPath()
        {
            Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();
            List<int> list = new List<int>();
            List<int> list2 = new List<int>();



            var nodes = nodeByValue.Values
               .Where(x => x.Children.Count == 0)
               .ToList();

            foreach (var node in nodes)
            {
                list.Clear();
                list.Add(node.Value);
                int count = 1;

                while (node.Parent != null)
                {

                    list.Add(node.Parent.Value);
                    count++;
                    node.Parent = node.Parent.Parent;
                }

                if (!map.ContainsKey(count))
                {
                    map.Add(count, new List<int>(list));
                }
            }

            var newMap = map.OrderByDescending(x => x.Key);

            foreach (var key in newMap)
            {
                var reversedArr = key.Value.ToArray();

                Console.WriteLine(string.Join(" -> ", reversedArr.Reverse()) + " (length = {0})", key.Key);
                break; ;
                
            }
        }

        static void AllPathsSum(int path)
        {
            Stack<int> sumList = new Stack<int>();
            var leafs = nodeByValue.Values.Where(x => x.Children.Count == 0).ToList();

            foreach (var leaf in leafs)
            {
                int sum = leaf.Value;
                sumList.Clear();
                sumList.Push(leaf.Value);

                while (leaf.Parent != null)
                {
                    sum += leaf.Parent.Value;
                    sumList.Push(leaf.Parent.Value);
                    leaf.Parent = leaf.Parent.Parent;
                }

                if (sum == path)
                {
                    Console.WriteLine(string.Join(" -> ", sumList));
                }
            }
        }

    }
}

