namespace Classes
{
    using Interfaces;

    public class Player : IPlayer
    {
        public Player(string name, int radius, int score = 0)
        {
            Name = name;
            Radius = radius;
            Score = score;
        }

        public string Name { get; set; }

        public int Radius { get; private set; }

        public int Score { get; set; }

        public int CompareTo(Player other)
        {
            int compare = other.Score.CompareTo(this.Score);
            if (compare == 0)
            {
                compare = other.Name.CompareTo(this.Name);
            }

            return compare;
        }
    }
}
