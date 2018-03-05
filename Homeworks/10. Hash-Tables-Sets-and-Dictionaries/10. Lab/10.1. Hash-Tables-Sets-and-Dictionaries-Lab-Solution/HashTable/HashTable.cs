using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
{
    private const int DefaultCapacity = 16;
    private const float LoadFactor = 0.75f;
    private int maxElement;

    private LinkedList<KeyValue<TKey, TValue>>[] hashTable;

    public HashTable(int capacity = DefaultCapacity)
    {
        hashTable = new LinkedList<KeyValue<TKey, TValue>>[capacity];
        maxElement = (int)(capacity * LoadFactor);
    }

    public int Count { get; private set; }

    public int Capacity => this.hashTable.Length;

    public void Add(TKey key, TValue value)
    {
        this.CheckGrouth();
        int hash = Math.Abs(key.GetHashCode()) % this.Capacity;

        if (this.hashTable[hash] == null)
        {
            this.hashTable[hash] = new LinkedList<KeyValue<TKey, TValue>>();
        }

        foreach (var keyValue in this.hashTable[hash])
        {
            if (keyValue.Key.Equals(key))
            {
                throw new ArgumentException();
            }
        }

        KeyValue<TKey, TValue> kvp = new KeyValue<TKey, TValue>(key, value);
        this.hashTable[hash].AddLast(kvp);
        this.Count++;
    }

    private void CheckGrouth()
    {
        if (this.Count >= this.maxElement)
        {
            this.Grow();
            this.maxElement = (int) (this.Capacity*LoadFactor);
        }
    }

    private void Grow()
    {
        HashTable<TKey, TValue> newTable = new HashTable<TKey, TValue>(this.Capacity * 2);

        foreach (var linkedList in this.hashTable)
        {
            if (linkedList != null)
            {
                foreach (var keyValue in linkedList)
                {
                    newTable.Add(keyValue.Key, keyValue.Value);
                }
            }
        }

        this.hashTable = newTable.hashTable;
        this.Count = newTable.Count;
    }

    public bool AddOrReplace(TKey key, TValue value)
    {
        this.CheckGrouth();
        int hash = Math.Abs(key.GetHashCode()) % this.Capacity;

        if (this.hashTable[hash] == null)
        {
            this.hashTable[hash] = new LinkedList<KeyValue<TKey, TValue>>();
        }

        foreach (var keyValue in this.hashTable[hash])
        {
            if (keyValue.Key.Equals(key))
            {
                keyValue.Value = value;
                return false;
            }
        }

        KeyValue<TKey, TValue> kvp = new KeyValue<TKey, TValue>(key, value);
        this.hashTable[hash].AddLast(kvp);
        this.Count++;
        return true;
    }

    public TValue Get(TKey key)
    {
        KeyValue<TKey, TValue> kvp = this.Find(key);

        if (kvp == null)
        {
            throw  new KeyNotFoundException();
        }

        return kvp.Value;
    }

    public TValue this[TKey key]
    {
        get
        {
            KeyValue<TKey, TValue> kvp = this.Find(key);
            if (kvp == null)
            {
                throw new KeyNotFoundException();
            }
            return kvp.Value;
        }
        set { this.AddOrReplace(key, value); }
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        KeyValue<TKey, TValue> kvp = this.Find(key);

        if (kvp != null)
        {
            value = kvp.Value;
            return true;
        }

        value = default(TValue);
        return false;
    }

    public KeyValue<TKey, TValue> Find(TKey key)
    {
        int hash = Math.Abs(key.GetHashCode())%this.Capacity;

        if (this.hashTable[hash] != null)
        {
            foreach (var kvp in this.hashTable[hash])
            {
                if (kvp.Key.Equals(key))
                {
                    return kvp;
                }
            }
        }

        return null;
    }

    public bool ContainsKey(TKey key)
    {
        return this.Find(key) != null;
    }

    public bool Remove(TKey key)
    {
        int hash = Math.Abs(key.GetHashCode())%this.Capacity;

        if (this.hashTable[hash] != null)
        {
            foreach (var keyValue in hashTable[hash])
            {
                this.hashTable[hash].Remove(keyValue);
                this.Count--;
                return true;
            }
        }

        return false;
    }

    public void Clear()
    {
        this.hashTable = new LinkedList<KeyValue<TKey, TValue>>[DefaultCapacity];
        this.maxElement = (int)(this.Capacity*LoadFactor);
        this.Count = 0;
    }

    public IEnumerable<TKey> Keys => this.hashTable.Where(list => list != null).SelectMany(x => x.Select(y => y.Key));

    public IEnumerable<TValue> Values => this.hashTable.Where(list => list != null).SelectMany(x => x.Select(y => y.Value));

    public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
    {
        foreach (var linkedList in hashTable)
        {
            if (linkedList != null)
            {
                foreach (var keyValue in linkedList)
                {
                    yield return keyValue;
                }
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
