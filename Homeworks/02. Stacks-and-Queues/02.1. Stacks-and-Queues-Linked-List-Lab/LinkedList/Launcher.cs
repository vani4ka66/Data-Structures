﻿using System;

class Launcher
{
    public static void Main()
    {
        LinkedList<int> list = new LinkedList<int>();

        list.AddFirst(1);
        list.AddLast(2);
        list.AddLast(5);
        //list.AddLast(6);



        list.RemoveFirst();
        list.RemoveLast();


        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
    }
}
