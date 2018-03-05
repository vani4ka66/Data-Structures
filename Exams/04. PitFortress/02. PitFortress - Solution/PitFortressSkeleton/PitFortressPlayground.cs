using System;

public class PitFortressPlayground
{
    public static void Main()
    {
        PitFortressCollection collection = new PitFortressCollection();


        collection.AddMinion(13);
        collection.AddMinion(27);
        collection.AddMinion(5);
        collection.AddMinion(5066);
        collection.AddMinion(5066);
        collection.AddMinion(134013);


        Console.WriteLine(collection.MinionsCount);
        foreach (var mine in collection.ReportMinions())
        {
            Console.WriteLine(mine.XCoordinate);
            Console.WriteLine(mine.Health);
            Console.WriteLine(mine.Id);
            Console.WriteLine("--------");
        }
    }
}
