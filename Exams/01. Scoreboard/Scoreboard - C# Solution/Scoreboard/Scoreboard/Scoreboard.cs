using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Scoreboard : IScoreboard
{
    private Dictionary<string, OrderedBag<ScoreboardEntry>> byGameName;
    private Dictionary<string, string> byUser;
    private SortedDictionary<string, string> byGame;

    public Scoreboard(int maxEntriesToKeep = 10)
    {
        byGameName = new Dictionary<string, OrderedBag<ScoreboardEntry>>();
        byUser = new Dictionary<string, string>();
        byGame = new SortedDictionary<string, string>();
    }

    public bool RegisterUser(string username, string password)
    {
        if (!byUser.ContainsKey(username))
        {
            byUser[username] = password;

            return true;
        }
        return false;
    }

    public bool RegisterGame(string game, string password)
    {
        if(!byGame.ContainsKey(game))
        {
            byGame[game] = password;

            if (!byGameName.ContainsKey(game))
            {
                byGameName.Add(game, new OrderedBag<ScoreboardEntry>());
            }
            else
            {
                return false;
            }

            return true;
        }

        return false;
    }

    public bool AddScore(string username, string userPassword, string game, string gamePassword, int score)
    {
        bool userExists = byUser.ContainsKey(username) && byUser[username] == userPassword;
        bool gameExists = byGame.ContainsKey(game) && (byGame[game] == gamePassword);

        if (userExists && gameExists)
        {
            if (byGameName.ContainsKey(game))
            {
                ScoreboardEntry entry = new ScoreboardEntry(username, score);
                byGameName[game].Add(entry);

                return true;
            }
            return true;
        }

        return false;
    }

    public IEnumerable<ScoreboardEntry> ShowScoreboard(string game)
    {
        var result = new List<ScoreboardEntry>();
        if (!byGame.ContainsKey(game))
        {
            return null;
        }

        OrderedBag<ScoreboardEntry> v = byGameName[game];
        int n = 0;
        foreach (var key in v)
        {
            if (n < 10)
            {
                result.Add(key);
                n++;
            }
            else
            {
                break;
            }
        }

        return result;
    }

    public bool DeleteGame(string game, string gamePassword)
    {
        if (byGame.ContainsKey(game) && byGame[game] == gamePassword)
        {
            byGame.Remove(game);
            byGameName.Remove(game);

            return true;
        }

        return false;
    }

    public IEnumerable<string> ListGamesByPrefix(string gameNamePrefix)
    {
        var result = byGame.Keys.Where(x => x.StartsWith(gameNamePrefix)).Take(10);

        return result;
    }
}