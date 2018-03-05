using System.Linq;

namespace BunnyWars.Core
{
    using System;

    public class Bunny : IComparable<Bunny>
    {
        public Bunny(string name, int team, int roomId, int health = 100, int score = 0)
        {
            this.RoomId = roomId;
            this.Name = name;
            this.Team = team;
            this.Health = health;
            this.Score = score;
            
        }

        public int RoomId { get; set; }

        public string Name { get; private set; }

        public string ReversedName
        {
            get
            {
                string reversed = "";
                for (int i = Name.Length - 1; i >= 0; i--)
                {
                    reversed += Name[i];
                }

                return reversed;
            }
        }

        public int Health { get; set; }

        public int Score { get; set; }

        public int Team { get; private set; }

        public int CompareTo(Bunny other)
        {
            int compare = other.Name.CompareTo(this.Name);
            return compare;
        }
    }
}
