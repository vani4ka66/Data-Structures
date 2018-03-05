using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

    using System;

    class Program
    {
        static void Main()
        {

             Hierarchy<string> hierarchy = new Hierarchy<string>("Leonidas");
            hierarchy.Add("Leonidas", "Xena The Princess Warrior");
            hierarchy.Add("Leonidas", "General Protos");
            hierarchy.Add("Xena The Princess Warrior", "Gorok");
            hierarchy.Add("Xena The Princess Warrior", "Bozot");
            hierarchy.Add("General Protos", "Subotli");
            hierarchy.Add("General Protos", "Kira");
            hierarchy.Add("General Protos", "Zaler");
            
            /*Hierarchy<string> other = new Hierarchy<string>("Leonidas");
            other.Add("Leonidas", "Xena The Princess Warrior");
            other.Add("Leonidas", "General Protos");
            other.Add("Xena The Princess Warrior", "Gorok");
            other.Add("Xena The Princess Warrior", "Bozot");
            /*hierarchy.Add("General Protos", "Subotli");
            hierarchy.Add("General Protos", "Kira");
            hierarchy.Add("General Protos", "Zaler");*/

            /*var children = hierarchy.GetChildren("Zaler");
            //Console.WriteLine(string.Join(", ", children));

            //var parent = hierarchy.GetParent("Subotli");
            //Console.WriteLine(parent);
            Console.WriteLine("******");

            hierarchy.Remove("Zaler");
            children = hierarchy.GetChildren("General Protos");
            //Console.WriteLine("Children: " + string.Join(", ", children));

            Console.WriteLine("----------");
            foreach (var item in hierarchy)
            {
                Console.WriteLine(item);
            }
            
           // hierarchy.Remove("Leonidas");
            //Console.WriteLine(hierarchy.Contains("Leonidas"));
            Console.WriteLine("Count: " + hierarchy.Count);
            Console.WriteLine("****************");*/
            //hierarchy.ForEach(Console.WriteLine);
            /* var result = hierarchy.GetCommonElements(other);
            Console.WriteLine("--------");

            foreach (var i in result)
            {
                Console.WriteLine(i);
            }*/

        }

    }
