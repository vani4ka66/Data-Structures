using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Program
{
    static void Main(string[] args)
    {
        var items = new List<Item>();
        var byId = new Dictionary<string, Item>();

        var ticks = 1;

        var line = Console.ReadLine();

        while (line != "end")
        {
            var cmArgs = line.Split(' ');

            switch (cmArgs[0])
            {
                case "add":
                    AddItem(cmArgs, items, byId);
                    break;
                case "start":
                    while (true)
                    {
                        //ticks++;
                        line = Console.ReadLine();
                        cmArgs = line.Split();

                        if (cmArgs[0].Equals("end"))
                        {
                            return;
                        }
                        if (cmArgs[0].Equals("move"))
                        {
                            var id = cmArgs[1];
                            var x = int.Parse(cmArgs[2]);
                            var y = int.Parse(cmArgs[3]);

                            byId[id].X1 = x;
                            byId[id].Y1 = y;
                        }

                        Sweep(ticks++, items);
                    }
            }

            line = Console.ReadLine();
        }
    }

    private static void Sweep(int ticks, List<Item> items)
    {
        InsertionSort(items);

        for (int i = 0; i < items.Count; i++)
        {
            var current = items[i];

            for (int j = i + 1; j < items.Count; j++)
            {
                var candiidate = items[j];

                if (candiidate.X1 > current.X2)
                {
                    break;
                }

                if (current.Intersect(candiidate))
                {
                    Console.WriteLine("({0}) {1} collides with {2}", ticks, candiidate.Id, current.Id);
                }
            }
        }
    }

    private static void InsertionSort(List<Item> items)
    {
        for (int i = 1; i < items.Count; i++)
        {
            var j = i;

            while (j > 0 && items[j - 1].X1 < items[j].X1)
            {
                Swap(j-1, j, items);
                j--;
            }
        }
    }

    private static void Swap(int i, int j, List<Item> items)
    {
        var temp = items[i];
        items[i] = items[j];
        items[j] = temp;
    }

    private static void AddItem(string[] cmArgs, List<Item> items, Dictionary<string, Item> byId)
    {
        var id = cmArgs[1];
        var x = int.Parse(cmArgs[2]);
        var y = int.Parse(cmArgs[3]);

        var item = new Item(id, x, y);

        items.Add(item);
        byId[id] = item;
    }
}

