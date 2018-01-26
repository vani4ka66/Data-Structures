using System;

public class Playground
{
    private static char[,] map =
    {
                    { '-', '-', '-', '-', '-' },
                    { '-', '*', '-', '-', '-' },
                    { 'W', 'W', 'W', 'W', 'W' },
                    { '-', '-', '-', 'P', '-' },
                    { '-', '-', '-', '-', '-' },
         };

    /*private static char[,] map =
    {
                    { '-', '-', '-', '-', '-', '-', '-', '-' },
                    { '-', '-', '*', '-', '-', '-', '-', '-' },
                    { 'W', 'W', 'W', 'W', 'W', '-', '-', '-' },
                    { '-', '-', '-', '-', 'W', 'W', '-', '-' },
                    { '-', '-', '-', 'P', 'W', '-', '-', '-' },
                    { '-', '-', '-', '-', '-', '-', '-', '-' }
         };*/

    static void Main()
    {
        //FCost = GCost + HCost;

        //FCost - distance from current  to the start;
        //HCost - distance from current to the destination;
        //Traverses the nodes with lowest f-cost first
        //Updates the f-costs of their neighbors

        //map = ReadMap();

        var start = FindGoal('P');
        var goal = FindGoal('*');

        var aStar = new AStar(map);
        var path = aStar.GetPath(start, goal);

        foreach (var node in path)
        {
            var row = node.Row;
            var col = node.Col;
            map[row, col] = '@';
        }

        PrintMap();

    }

    private static char[,] ReadMap()
    {
        var n = int.Parse(Console.ReadLine());
        var map = new char[n, n];
        for (int i = 0; i < n; i++)
        {
            char[] line = Console.ReadLine().ToCharArray();

            for (int j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = line[j];
            }
        }

        return map;
    }

    static Node FindGoal(char goal)
    {
        for (int row = 0; row < map.GetLength(0); row++)
        {
            for (int col = 0; col < map.GetLength(1); col++)
            {
                if (map[row, col] == goal)
                {
                    return new Node(row, col);
                }
            }
        }

        throw new ArgumentException("Object not present on map");
    }

    static void PrintMap()
    {
        for (int row = 0; row < map.GetLength(0); row++)
        {
            for (int col = 0; col < map.GetLength(1); col++)
            {
                if (map[row, col] == '@')
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }

                Console.Write(map[row, col]);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" ");
            }

            Console.WriteLine();
        }
    }
}
