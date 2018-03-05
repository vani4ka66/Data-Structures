namespace Classes
{
    using Interfaces;

    public class Mine : IMine
    {
        public Mine(int delay, int damage, int xCoordinate, Player player)
        {
            Delay = delay;
            Damage = damage;
            XCoordinate = xCoordinate;
            Player = player;
        }

        public int Id { get; set; }

        public int Delay { get; set; }

        public int Damage { get; private set; }

        public int XCoordinate { get; private set; }

        public Player Player { get;  set; }

        public int CompareTo(Mine other)
        {
            int compare = this.Delay.CompareTo(other.Delay);
            if (compare == 0)
            {
                compare = this.Id.CompareTo(other.Id);
            }

            return compare;
        }
    }
}
