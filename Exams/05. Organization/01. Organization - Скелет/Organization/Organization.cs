using System.Collections;
using System.Collections.Generic;

public class Organization : IOrganization
{
    public IEnumerator<Person> GetEnumerator()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public int Count { get; }
    public bool Contains(Person person)
    {
        throw new System.NotImplementedException();
    }

    public bool ContainsByName(string name)
    {
        throw new System.NotImplementedException();
    }

    public void Add(Person person)
    {
        throw new System.NotImplementedException();
    }

    public Person GetAtIndex(int index)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<Person> GetByName(string name)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<Person> FirstByInsertOrder(int count = 1)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<Person> SearchWithNameSize(int minLength, int maxLength)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<Person> GetWithNameSize(int length)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<Person> PeopleByInsertOrder()
    {
        throw new System.NotImplementedException();
    }
}