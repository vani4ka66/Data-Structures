namespace Classes
{
    using Interfaces;

    public class Minion : IMinion
    {
        public Minion(int xCoordinate, int health = 100)
        {
            this.XCoordinate = xCoordinate;
            this.Health = health;
        }

        public int Id { get; set; }

        public int XCoordinate { get; private set; }

        public int Health { get; set; }

        public int CompareTo(Minion other)
        {
            int compare = this.XCoordinate.CompareTo(other.XCoordinate);
            if (compare == 0)
            {
                compare = this.Id.CompareTo(other.Id);
            }

            return compare;
        }
    }
}
