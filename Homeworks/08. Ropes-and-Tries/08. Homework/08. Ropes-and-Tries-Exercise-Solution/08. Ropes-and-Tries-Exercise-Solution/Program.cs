using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08.Ropes_and_Tries_Exercise_Solution
{
    class Program
    {
        static void Main(string[] args)
        {
            TextEditor text = new TextEditor();
            
            string input = Console.ReadLine();

            while (input != "end")
            {
                if (input.Contains("\""))
                {
                    string[] argum = input.Split(new[] { '\"' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] arr = argum[0].Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);
                    if (arr.Length == 2)
                    {
                        string user = arr[0];
                        string prepend = arr[1];
                        string str = argum[1];

                        if (prepend == "prepend")
                        {
                            text.Prepend(user, str);
                        }
                    }
                    if (arr.Length == 3)
                    {
                        string user = arr[0];
                        string insert = arr[1];
                        string index = arr[2];
                        string str = argum[1];

                        if (insert == "insert")
                        {
                            text.Insert(user, int.Parse(index), str);
                        }
                    }
                }
                else
                {
                    string[] argum = input.Split(' ');

                    if (argum.Length == 1)
                    {
                        foreach (var user in text.Users())
                        {
                            Console.WriteLine(user);
                        }
                    }
                    else if (argum.Length == 2)
                    {
                        string command1 = argum[0];
                        string command2 = argum[1];

                        if (command1 == "login")
                        {
                            text.Login(command2);
                        }
                        if (command1 != "login")
                        {
                            if (command2 == "print")
                            {
                                Console.WriteLine(text.Print(command1));
                            }
                            if (command2 == "length")
                            {
                                Console.WriteLine(text.Length(command1));
                            }
                            if (command2 == "clear")
                            {
                                text.Clear(command1);
                            }
                            if (command2 == "undo")
                            {
                                text.Undo(command1);
                            }
                            if (command1 == "logout")
                            {
                                text.Logout(command2);
                            }
                            if (command1 == "users")
                            {
                                foreach (var user in text.Users(command2))
                                {
                                    Console.WriteLine(user);
                                }
                            }
                        }
                    }
                    else if (argum.Length == 4)
                    {
                        string command1 = argum[0];
                        string command2 = argum[1];
                        string command3 = argum[2];
                        string command4 = argum[3];

                        if (command2 == "delete")
                        {
                            text.Delete(command1, int.Parse(command3), int.Parse(command4));
                        }
                        if (command2 == "substring")
                        {
                            text.Substring(command1, int.Parse(command3), int.Parse(command4));
                        }
                    }
                }

                input = Console.ReadLine();
            }


            /*text.Login("Vania");
            text.Login("Pesho");
            text.Login("Gosho");

            text.Prepend("Vania","pesho");
            text.Prepend("Vania","text ");
            text.Prepend("Vania", "treto ");

            //Console.WriteLine(text.Print("Vania"));

            //text.Undo("Vania");
            //text.Undo("Vania");

            //text.Insert("Vania", 6,"edno ");
            //text.Substring("Vania",0, 3);
            Console.WriteLine(text.Print("Vania"));

            text.Delete("Vania", 0, 4);
            //text.Clear("Vania");
            Console.WriteLine(text.Print("Vania"));*/
        }
    }
}
