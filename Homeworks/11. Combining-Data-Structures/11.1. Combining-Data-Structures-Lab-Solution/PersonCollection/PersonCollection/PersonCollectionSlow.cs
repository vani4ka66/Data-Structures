using System;
using System.Collections.Generic;
using System.Linq;

public class PersonCollectionSlow : IPersonCollection
{
    private List<Person> list;

    public PersonCollectionSlow()
    {
        this.list = new List<Person>();
    }

    public bool AddPerson(string email, string name, int age, string town)
    {
        var person = new Person(email, name, age, town);
        if (this.FindPerson(email) == null)
        {
            this.list.Add(person);
            return true;
        }

        return false;
    }

    public int Count => this.list.Count;
   

    public Person FindPerson(string email)
    {
        return list.FirstOrDefault(x => x.Email.Equals(email));
    }

    public bool DeletePerson(string email)
    {
        var person = this.FindPerson(email);

        return this.list.Remove(person);
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        return this.list.Where(x => x.Email.EndsWith("@" + emailDomain)).OrderBy(y => y.Email);
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        return this.list.Where(x => x.Name == name && x.Town == town).OrderBy(x => x.Email);
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        return this.list.Where(x => x.Age >= startAge && x.Age <= endAge).OrderBy(x => x.Age).ThenBy(x => x.Email);
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
    {
        return this.list
            .Where(x=> x.Town == town)
            .Where(x => x.Age >= startAge && x.Age <= endAge)
            .OrderBy(x => x.Age).ThenBy(x => x.Email);
    }
}
