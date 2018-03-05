using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.PowerCollections;

public class Program
{
    static void Main(string[] args)
    {

       Computer computer = new Computer(100);

        Random random = new Random();

        List<Invader> expected = new List<Invader>();

        for (int i = 1; i <= 100; i++)
        {
            var invader = new Invader(random.Next(50), random.Next(50));
            computer.AddInvader(invader);
            expected.Add(invader);
        }

        computer.DestroyHighestPriorityTargets(1);

        var toRemove = expected.OrderBy(x => x.Distance).ThenBy(x => -x.Damage).Take(50).ToList();
        expected.RemoveAll(x => toRemove.Contains(x));

        foreach (var invader in expected.Skip(0).Take(5))
        {
            Console.WriteLine(invader.Damage);
            Console.WriteLine(invader.Distance);
            Console.WriteLine();
        }
        Console.WriteLine("-------------");
        foreach (var source in computer.Invaders().Skip(0).Take(5))
        {
            Console.WriteLine(source.Damage);
            Console.WriteLine(source.Distance);
            Console.WriteLine();
        }

        bool areEqual = expected.Equals(computer.Invaders());
        //Console.WriteLine(areEqual);

        //Console.WriteLine(computer.Energy);
        //Console.WriteLine(computer.Count());



        /*foreach (var item in collection)
        {
            Console.WriteLine(item.Damage);
            Console.WriteLine(collection.Energy);
        }
        Console.WriteLine("----------");
        foreach (var item in collection.Invaders())
        {
            Console.WriteLine(item.Damage);
        }*/

    }
}


