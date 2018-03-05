using System.Linq;
using Wintellect.PowerCollections;

namespace BunnyWars.Core
{
    using System;
    using System.Collections.Generic;

    public class BunnyWarsStructure : IBunnyWarsStructure
    {
        private Dictionary<string, Bunny> bunniesIds;
        private Dictionary<int, Bag<Bunny>> roomsWithTeams;
        private Dictionary<int, OrderedBag<Bunny>> teamIds;
        private List<int> rooms;

        public BunnyWarsStructure()
        {
            bunniesIds = new Dictionary<string, Bunny>();
            roomsWithTeams = new Dictionary<int, Bag<Bunny>>();
            teamIds = new Dictionary<int, OrderedBag<Bunny>>();
            rooms = new List<int>();
        }

        public int BunnyCount
        {
            get { return bunniesIds.Count; }
        }

        public int RoomCount
        { get { return roomsWithTeams.Count; } }

        public void AddRoom(int roomId)
        {
            if (roomsWithTeams.ContainsKey(roomId))
            {
                throw new ArgumentException();
            }

            roomsWithTeams.Add(roomId, new Bag<Bunny>());
            rooms.Add(roomId);
        }

        public void AddBunny(string name, int team, int roomId)
        {
            if (!roomsWithTeams.ContainsKey(roomId))
            {
                throw new ArgumentException();
            }

            if (team < 0 || team > 4)
            {
                throw new IndexOutOfRangeException();
            }

            if (bunniesIds.ContainsKey(name))
            {
                throw new ArgumentException();
            }
            

            Bunny bunny = new Bunny(name, team, roomId);
            bunniesIds[name] = bunny;


            if (roomsWithTeams[roomId].Count < 5)
            {
                roomsWithTeams[roomId].Add(bunny);

                if (!teamIds.ContainsKey(team))
                {
                    teamIds.Add(team, new OrderedBag<Bunny>());
                }

                teamIds[team].Add(bunny);
            }
        }

        public void Remove(int roomId)
        {
            if (!roomsWithTeams.ContainsKey(roomId))
            {
                throw new ArgumentException();
            }

            if (roomsWithTeams[roomId].Count != 0)
            {
                foreach (var kvp in roomsWithTeams[roomId])
                {
                    if (bunniesIds.ContainsKey(kvp.Name))
                    {
                        bunniesIds.Remove(kvp.Name);
                    }

                    if (teamIds[kvp.Team].Contains(kvp))
                    {
                        teamIds[kvp.Team].Remove(kvp);
                    }
                }
            }

            roomsWithTeams.Remove(roomId);
        }

        public void Next(string bunnyName)
        {
            if (!bunniesIds.ContainsKey(bunnyName))
            {
                throw new ArgumentException();
            }

            rooms.Sort();

            bool isFoundNextRoom = false;

            foreach (var kvp in roomsWithTeams)
            {
                foreach (var bunny in kvp.Value)
                {
                    if (bunny.Name.Equals(bunnyName))
                    {
                        Bunny foundBunny = bunny;
                        int currentRoom = kvp.Key;

                        for (int i = 0; i < rooms.Count; i++)
                        {
                            int nextRoom = Int32.MinValue;
                            if (rooms[i] == currentRoom)
                            {
                                if (i == rooms.Count - 1)
                                {
                                    nextRoom = rooms[0];
                                }
                                else if (i < rooms.Count - 1)
                                {
                                    nextRoom = rooms[i + 1];
                                }

                                roomsWithTeams[currentRoom].Remove(foundBunny);
                                foundBunny.RoomId = nextRoom;
                                roomsWithTeams[nextRoom].Add(foundBunny);
                                isFoundNextRoom = true;

                                break;
                            }
                        }
                        break;
                    }
                }
                if (isFoundNextRoom)
                {
                    break;
                }
            }
        }

        public void Previous(string bunnyName)
        {
            if (!bunniesIds.ContainsKey(bunnyName))
            {
                throw new ArgumentException();
            }

            rooms.Sort();

            bool isFoundNextRoom = false;

            foreach (var kvp in roomsWithTeams)
            {
                foreach (var bunny in kvp.Value)
                {
                    if (bunny.Name.Equals(bunnyName))
                    {
                        Bunny foundBunny = bunny;
                        int currentRoom = kvp.Key;

                        for (int i = 0; i < rooms.Count; i++)
                        {
                            int nextRoom = Int32.MinValue;
                            if (rooms[i] == currentRoom)
                            {
                                if (i == 0)
                                {
                                    nextRoom = rooms[rooms.Count - 1];
                                }
                                else if (i > 0)
                                {
                                    nextRoom = rooms[i - 1];
                                }

                                roomsWithTeams[currentRoom].Remove(foundBunny);
                                foundBunny.RoomId = nextRoom;
                                roomsWithTeams[nextRoom].Add(foundBunny);
                                isFoundNextRoom = true;

                                break;
                            }
                        }
                        break;
                    }
                }
                if (isFoundNextRoom)
                {
                    break;
                }
            }
        }

        public void Detonate(string bunnyName)
        {
            if (!bunniesIds.ContainsKey(bunnyName))
            {
                throw new ArgumentException();
            }

            int detonatedRoom = 0;
            int teamId = 0;

            foreach (var kvp in roomsWithTeams)
            {
                foreach (var bunny in kvp.Value)
                {
                    if (bunny.Name.Equals(bunnyName))
                    {
                        teamId = bunny.Team;
                        detonatedRoom = kvp.Key;
                    }
                }
            }

            foreach (var v in roomsWithTeams[detonatedRoom])
            {
                if (v.Team != teamId)
                {
                    v.Health -= 30;
                    if (v.Health <= 0)
                    {
                        roomsWithTeams[detonatedRoom].Remove(v);
                        bunniesIds.Remove(bunnyName);
                        teamIds[v.Team].Remove(v);

                        Bunny b = roomsWithTeams[detonatedRoom].FirstOrDefault(x => x.Name.Equals(bunnyName));
                        b.Score += 1;
                    }
                }
            }
        }

        public IEnumerable<Bunny> ListBunniesByTeam(int team)
        {
            if (team < 0 || team > 4)
            {
                throw new IndexOutOfRangeException();
            }
            var result = teamIds[team];
            return result;
        }

        public IEnumerable<Bunny> ListBunniesBySuffix(string suffix)
        {
            var result = bunniesIds.Values
                .Where(x => x.Name.EndsWith(suffix))
                .OrderBy(x => x.ReversedName, StringComparer.Ordinal).ThenBy(x => x.Name.Length);
            
            return result;
        }
    }
}
