using System;public class Person : IComparable<Person>
{
    public Person(string name, double salary)
    {
        this.Name = name;
        this.Salary = salary;
    }

    public string Name { get; set; }
    public double Salary { get; set; }

    public int CompareTo(Person other)
    {
        int compare = other.Name.Length.CompareTo(this.Name.Length);
        return compare;
    }
}
