using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Computer : IComputer, IEnumerable<Invader>
{
    private OrderedBag<Invader> orderedList;
    private List<Invader> byAppearance;
    private int energy;

    public Computer(int energy)
    {
        orderedList = new OrderedBag<Invader>();
        byAppearance = new List<Invader>();
        this.energy = energy;
        if (energy < 0)
        {
            throw new ArgumentException();
        }
    }

    public int Energy
    {
        get
        {
            return this.energy;
        }
        set
        {
            this.energy = value;
        }
    }

    public void Skip(int turns)
    {
        foreach (var invader in orderedList)
        {
            invader.Distance -= turns;
            if (invader.Distance <= 0)
            {
                this.Energy -= invader.Damage;
                if (this.Energy < 0)
                {
                    this.Energy = 0;
                }
                byAppearance.Remove(invader);
            }
        }

        orderedList.RemoveAll(x => x.Distance <= 0);
    }

    public void AddInvader(Invader invader)
    {
        orderedList.Add(invader);
        byAppearance.Add(invader);

        //this.energy -= invader.Damage;
        if (this.energy <= 0)
        {
            this.energy = 0;
        }
    }

    public void DestroyHighestPriorityTargets(int count)
    {
        if (count < orderedList.Count)
        {
            OrderedBag<Invader> arr = new OrderedBag<Invader>();

            var toRemove = orderedList.Take(count);

            foreach (var invader in toRemove)
            {
                byAppearance.Remove(invader);
            }

            for (int i = 0; i <= count; i++)
            {
                orderedList.RemoveFirst();
            }

        }
        else
        {
            orderedList = new OrderedBag<Invader>();
            byAppearance = new List<Invader>();
        }


        /*if (count < orderedList.Count)
        {
            for (int i = count; i < orderedList.Count; i++)
            {
                arr.Add(orderedList[i]);
            }


            orderedList = arr;
        }*/


    }

    public void DestroyTargetsInRadius(int radius)
    {
        OrderedBag<Invader> arr = new OrderedBag<Invader>();

        foreach (var invader in orderedList)
        {
            if (invader.Distance <= radius)
            {
                arr.Add(invader);
                byAppearance.Remove(invader);
            }
        }
        orderedList = arr;
    }

    public IEnumerable<Invader> Invaders()
    {
        foreach (var invader in byAppearance)
        {
            yield return invader;
        }
    }

    public IEnumerator<Invader> GetEnumerator()
    {
        foreach (var invader in orderedList)
        {
            yield return invader;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
