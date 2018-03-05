using System;

public class ScoreboardEntry : IComparable<ScoreboardEntry>
{

    public ScoreboardEntry(string username, int score)
    {
        this.Username = username;
        this.Score = score;
    }

    public int CompareTo(ScoreboardEntry other)
    {
        int compare = other.Score.CompareTo(this.Score);
        if (compare == 0)
        {
            compare = this.Username.CompareTo(other.Username);
        }

        return compare;
    }

    public int Score { get; set; }

    public string Username { get; set; }
}