using System;
using System.Collections.Generic;
using System.Linq;
using Classes;
using Interfaces;
using Wintellect.PowerCollections;

public class PitFortressCollection : IPitFortress
{
    private Dictionary<string, Player> players;
    private SortedSet<Player> playersSet;
    private OrderedDictionary<int, SortedSet<Minion>> minions;
    private SortedSet<Mine> mines;


    public PitFortressCollection()
    {
        players = new Dictionary<string, Player>(); 
        playersSet = new SortedSet<Player>();
        minions = new OrderedDictionary<int, SortedSet<Minion>>();
        mines = new SortedSet<Mine>();
    }

    public int PlayersCount { get { return players.Count; } }

    public int MinionsCount { get { return this.minions.Sum(x => x.Value.Count); } }

    public int MinesCount { get { return this.mines.Count; } }

    public void AddPlayer(string name, int mineRadius)
    {
        if (players.ContainsKey(name))
        {
            throw new ArgumentException();
        }

        if (mineRadius < 0)
        {
            throw new ArgumentException();
        }

        Player player = new Player(name, mineRadius);
        players[name] = player;
        playersSet.Add(player);
    }

    public void AddMinion(int xCoordinate)
    {
        if (xCoordinate < 0 || xCoordinate > 1000000)
        {
            throw new ArgumentException();
        }

        var minion = new Minion(xCoordinate);
        minion.Id = this.MinionsCount + 1;

        if (!minions.ContainsKey(xCoordinate))
        {
            minions.Add(xCoordinate, new SortedSet<Minion>());
        }

        minions[xCoordinate].Add(minion);
    }

    public void SetMine(string playerName, int xCoordinate, int delay, int damage)
    {
        if (!players.ContainsKey(playerName))
        {
            throw new ArgumentException();
        }

        if (xCoordinate < 0 || xCoordinate > 1000000)
        {
            throw new ArgumentException();
        }

        if (delay < 1 || delay > 10000)
        {
            throw new ArgumentException();
        }

        if (damage < 0 || damage > 100)
        {
            throw new ArgumentException();
        }

        var player = this.players[playerName];
        Mine mine = new Mine(delay, damage, xCoordinate, player);
        mine.Id = mines.Count + 1;

        this.mines.Add(mine);
    }

    public IEnumerable<Minion> ReportMinions()
    {
        foreach (var set in minions.Values)
        {
            foreach (var minion in set)
            {
                yield return minion;
            }
        }
    }

    public IEnumerable<Player> Top3PlayersByScore()
    {
        if (this.players.Count < 3)
        {
            throw new ArgumentException();
        }

        return this.playersSet.Take(3);
    }

    public IEnumerable<Player> Min3PlayersByScore()
    {
        if (this.players.Count < 3)
        {
            throw new ArgumentException();
        }

        return this.playersSet.Reverse().Take(3);
    }

    public IEnumerable<Mine> GetMines()
    {
        return this.mines;
    }

    public void PlayTurn()
    {
        var minesToDetonate = new List<Mine>();

        foreach (var mine in mines)
        {
            mine.Delay--;

            if (mine.Delay <= 0)
            {
                minesToDetonate.Add(mine);
            }
        }

        foreach (var mine in minesToDetonate)
        {
            var start = mine.XCoordinate - mine.Player.Radius;
            var end = mine.XCoordinate + mine.Player.Radius;

            var player = mine.Player;
            var minionsToUpdate = this.minions.Range(start, true, end, true).SelectMany(x => x.Value).ToList();

            foreach (var minion in minionsToUpdate)
            {
                minion.Health -= mine.Damage;

                if (minion.Health <= 0)
                {
                    this.playersSet.Remove(player);
                    mine.Player.Score++;
                    this.playersSet.Add(player);
                    this.minions[minion.XCoordinate].Remove(minion);
                }
            }
        }

        foreach (var mine in minesToDetonate)
        {
            this.mines.Remove(mine);
        }
    }
}
