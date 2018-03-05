using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

class Example
{
    public const double Epsilon = 0.01;

    static void Main()
    {
        //exercise 2
        HashTable<string, string> phoneBook = new HashTable<string, string>();

        string input = Console.ReadLine();

        while (input != "search")
        {
            string[] splitted = input.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var name = splitted[0];
            var phone = splitted[1];

            if (!phoneBook.ContainsKey(name))
            {
                phoneBook.Add(name, phone);
            }

            input = Console.ReadLine();
            if (input == "search")
            {
                string input2 = Console.ReadLine();
                while (input2 != "")
                {
                    if (!phoneBook.ContainsKey(input2))
                    {
                        Console.WriteLine("Contact {0} does not exist.", input2);
                    }
                    else
                    {
                        Console.Write(input2);
                        Console.WriteLine(" -> " + phoneBook[input2]);
                    }

                    input2 = Console.ReadLine();
                }
                
            }


        //exercise 1;
        /*HashTable<char, int> grades = new HashTable<char, int>();

    string input = Console.ReadLine();
    for (int i = 0; i < input.Length; i++)
    {
        var current = input[i];
        if (!grades.ContainsKey(current))
        {
            grades.Add(current, 0);
        }

        grades[current]++;

    }

    foreach (var keyValue in grades.OrderBy(x=> x.Key))
    {
        Console.WriteLine(keyValue.Key + ": " + keyValue.Value + " time/s");
    }*/



        //Lab
        /*HashTable<string, int> grades = new HashTable<string, int>();

    Console.WriteLine("Grades:" + string.Join(",", grades));
    Console.WriteLine("---------------");

    grades.Add("Peter", 3);
        var n = grades.Find("Peter");
        Console.WriteLine("************" + n);

    grades.Add("Maria", 6);
    grades["George"] = 5;
    Console.WriteLine("Grades:" + string.Join(",", grades));
    Console.WriteLine("--------------------");

    grades.AddOrReplace("Peter", 33);
    grades.AddOrReplace("Tanya", 4);
    grades["George"] = 55;
    Console.WriteLine("Grades:" + string.Join(",", grades));
    Console.WriteLine("--------------------");

    Console.WriteLine("Keys: " + string.Join(", ", grades.Keys));
    Console.WriteLine("Values: " + string.Join(", ", grades.Values));
    Console.WriteLine("Count = " + string.Join(", ", grades.Count));
    Console.WriteLine("--------------------");

    grades.Remove("Peter");
    grades.Remove("George");
    grades.Remove("George");
    Console.WriteLine("Grades:" + string.Join(",", grades));
    Console.WriteLine("--------------------");

    Console.WriteLine("ContainsKey[\"Tanya\"] = " + grades.ContainsKey("Tanya"));
    Console.WriteLine("ContainsKey[\"George\"] = " + grades.ContainsKey("George"));
    Console.WriteLine("Grades[\"Tanya\"] = " + grades["Tanya"]);
    Console.WriteLine("--------------------");*/
    }
   
}

