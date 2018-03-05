using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Wintellect.PowerCollections;

public class PersonCollection : IPersonCollection
{
    private Dictionary<string, Person> map;
    private Dictionary<string, SortedSet<Person>> personsByEmailDomain;
    private Dictionary<string, SortedSet<Person>> personsByNameandTown;
    private OrderedDictionary<int, SortedSet<Person>> personsByAge;
    private Dictionary<string, OrderedDictionary<int, SortedSet<Person>>>  personsByTownAndAge;


    public PersonCollection()
    {
        map = new Dictionary<string, Person>();
        personsByEmailDomain = new Dictionary<string, SortedSet<Person>>();
        personsByNameandTown = new Dictionary<string, SortedSet<Person>>();
        personsByAge = new OrderedDictionary<int, SortedSet<Person>>();
        personsByTownAndAge = new Dictionary<string, OrderedDictionary<int, SortedSet<Person>>>();
    }

    public bool AddPerson(string email, string name, int age, string town)
    {
        var person = new Person(email, name, age, town);

        if (!map.ContainsKey(email))
        {
            map.Add(email, person);
            var emailDomain = this.ExtractEmailDomain(email);
            this.personsByEmailDomain.AppendValueToKey(emailDomain, person);

            string key = name + "!" + town;
            this.personsByNameandTown.AppendValueToKey(key, person);
            this.personsByAge.AppendValueToKey(age, person);

            this.personsByTownAndAge.EnsureKeyExists(town);
            this.personsByTownAndAge[town].AppendValueToKey(age, person);

            return true;
        }

        return false;
    }

    private string ExtractEmailDomain(string email)
    {
        var domain = email.Split('@')[1];
        return domain;
    }

    public int Count => this.map.Count;

    public Person FindPerson(string email)
    {
        Person person;
        var personExists = this.map.TryGetValue(email, out person);

        return person;
    }

    public bool DeletePerson(string email)
    {
        var person = this.FindPerson(email);
        if (person == null)
        {
            return false;
        }

        var personDeleted = this.map.Remove(email);
        var emailDomain = this.ExtractEmailDomain(email);
        this.personsByEmailDomain[emailDomain].Remove(person);
        string combinedName = person.Name + "!" + person.Town;
        this.personsByNameandTown[combinedName].Remove(person);
        this.personsByAge[person.Age].Remove(person);
        this.personsByTownAndAge[person.Town][person.Age].Remove(person);

        return true;
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        return this.personsByEmailDomain.GetValuesForKey(emailDomain);
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        var nameTown = name + "!" + town;
        return this.personsByNameandTown.GetValuesForKey(nameTown);
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        var range = this.personsByAge.Range(startAge, true, endAge, true);

        foreach (var person in range)
        {
            foreach (var person1 in person.Value)
            {
                yield return person1;
            }
        }
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
    {
        if (!this.personsByTownAndAge.ContainsKey(town))
        {
            return Enumerable.Empty<Person>();
        }

        var personInRange = this.personsByTownAndAge[town].Range(startAge, true, endAge, true);
        var result = new List<Person>();

        foreach (var person in personInRange)
        {
            foreach (var person1 in person.Value)
            {
                result.Add(person1);
            }
        }
        return result;

    }
}
