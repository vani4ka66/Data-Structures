using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Organization : IOrganization
{
    private Dictionary<string, List<Person>> byName;
    private Dictionary<int, Person> byId;
    private OrderedDictionary<int, List<Person>> byLength;

    public Organization()
    {
        byName = new Dictionary<string, List<Person>>();
        byId = new Dictionary<int, Person>();
        byLength = new OrderedDictionary<int, List<Person>>();
    }

    public int Count { get { return byId.Count; } }

    public bool Contains(Person person)
    {
        var name = person.Name;
        if (!byName.ContainsKey(name))
        {
            return false;
        }

        return true;
    }

    public bool ContainsByName(string name)
    {
        if (byName.ContainsKey(name))
        {
            return true;
        }

        return false;
    }

    public void Add(Person person)
    {
        var name = person.Name;

        if (!byName.ContainsKey(name))
        {
            byName.Add(name, new List<Person>());
        }

        byName[name].Add(person);

        var idNumber = this.byId.Count;
        byId[idNumber] = person;

        if (!byLength.ContainsKey(name.Length))
        {
            byLength.Add(name.Length, new List<Person>());
        }
        byLength[name.Length].Add(person);
    }

    public Person GetAtIndex(int index)
    {
        if (!byId.ContainsKey(index))
        {
            throw new IndexOutOfRangeException();
        }

        var person = byId[index];
        return person;
    }

    public IEnumerable<Person> GetByName(string name)
    {
        if (!byName.ContainsKey(name))
        {
            return Enumerable.Empty<Person>();
        }

        var result = byName[name];
        return result;
    }

    public IEnumerable<Person> FirstByInsertOrder(int count = 1)
    {
        return  byId.Values;
    }

    public IEnumerable<Person> SearchWithNameSize(int minLength, int maxLength)
    {
        var range = byLength.Range(minLength, true, maxLength, true).ToList();

        foreach (var kvp in range)
        {
            foreach (var item in kvp.Value)
            {
                yield return item;
            }
        }
    }

    public IEnumerable<Person> GetWithNameSize(int length)
    {
        if (!byLength.ContainsKey(length))
        {
            throw new ArgumentException();
        }

        return byLength[length];
    }

    public IEnumerable<Person> PeopleByInsertOrder()
    {
        foreach (var person in byId)
        {
            yield return person.Value;
        }
    }

    public IEnumerator<Person> GetEnumerator()
    {
        foreach (var person in byId)
        {
            yield return person.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}