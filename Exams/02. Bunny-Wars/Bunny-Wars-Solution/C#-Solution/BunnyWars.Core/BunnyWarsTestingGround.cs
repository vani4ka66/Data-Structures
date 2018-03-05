using System;
using System.Linq;

namespace BunnyWars.Core
{
    class BunnyWarsTestingGround
    {
        static void Main(string[] args)
        {
            BunnyWarsStructure collection = new BunnyWarsStructure();

            collection.AddRoom(1);
            collection.AddRoom(5);
            collection.AddBunny("Nasko", 3, 1);
            collection.Next("Nasko");

            var bunnies = collection.ListBunniesByTeam(3);
            var bunny = bunnies.FirstOrDefault();

            //Assert
            //Assert.AreEqual(5, bunny.RoomId, "Room Id was incorrect!");

            //Console.WriteLine(collection.RoomCount);
            var r = collection.ListBunniesBySuffix("");

            foreach (var bunn in r)
            {
                Console.WriteLine(bunn.Name);
            }
        }
    }
}
