using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class HashSet<T> : IEnumerable<T>
    {
        private HashTable<T, T> hashTable;

        public HashSet()
        {
            this.hashTable = new HashTable<T, T>();
        }

        public void Add(T item)
        {
            this.hashTable.AddOrReplace(item, item);
        }

        public HashSet<T> UnionWith(IEnumerable<T> other)
        {
            HashSet<T> result = new HashSet<T>();

            foreach (var item in this)
            {
                result.Add(item);
            }

            foreach (var item in other)
            {
                result.Add(item);
            }

            return result;
        }

        public HashSet<T> IntersectWith(IEnumerable<T> other)
        {
            HashSet<T> result = new HashSet<T>();

            foreach (var item in other)
            {
                if (this.hashTable.ContainsKey(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var key in hashTable.Keys)
            {
                yield return key;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
