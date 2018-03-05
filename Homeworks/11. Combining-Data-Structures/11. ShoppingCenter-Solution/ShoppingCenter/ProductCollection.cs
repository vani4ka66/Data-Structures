using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;

public class ProductCollection
{
    private Dictionary<string, OrderedBag<Product>> byName;
    private Dictionary<string, OrderedBag<Product>> byProducer;
    private OrderedDictionary<decimal, Bag<Product>> byPrice;
    private Dictionary<string, OrderedBag<Product>> byNameAndProducer;

    public ProductCollection()
    {
        byName = new Dictionary<string, OrderedBag<Product>>();
        byProducer = new Dictionary<string, OrderedBag<Product>>();
        byPrice = new OrderedDictionary<decimal, Bag<Product>>();
        byNameAndProducer = new Dictionary<string, OrderedBag<Product>>();
    }
    public void Add(string name, decimal price, string producer)
    {
        Product product = new Product(name, price, producer);

        if (!byName.ContainsKey(name))
        {
            byName.Add(name, new OrderedBag<Product>());
        }
        byName[name].Add(product);

        if (!byProducer.ContainsKey(producer))
        {
            byProducer.Add(producer, new OrderedBag<Product>());
        }
        byProducer[producer].Add(product);

        if (!byPrice.ContainsKey(price))
        {
            byPrice.Add(price, new Bag<Product>());
        }
        byPrice[price].Add(product);

        if (!byNameAndProducer.ContainsKey(name + "" + producer))
        {
            byNameAndProducer.Add(name + "" + producer, new OrderedBag<Product>());
        }
        byNameAndProducer[name + "" + producer].Add(product);
    }

    public int Delete(string producer)
    {
        var productsToDelete = byProducer[producer];
        this.byProducer.Remove(producer);
        //int n = byProducer[producer].RemoveMany(productsToDelete);

        foreach (var product in productsToDelete)
        {
            this.byName[product.Name].Remove(product);
            this.byNameAndProducer[product.Name + product.Producer].Remove(product);
            this.byPrice[product.Price].Remove(product);
        }

        return productsToDelete.Count;
    }

    public int Delete(string name, string producer)
    {
        var key = name + "" + producer;
        if (!byNameAndProducer.ContainsKey(key))
        {
            return 0;
        }

        var productsToDelete = byNameAndProducer[key];
        byNameAndProducer.Remove(key);

        foreach (var product in productsToDelete)
        {
            this.byName[product.Name].Remove(product);
            this.byProducer[product.Producer].Remove(product);
            this.byPrice[product.Price].Remove(product);
        }

        return productsToDelete.Count;
    }

    public IEnumerable<Product> FindProductsByName(string name)
    {
        if (!byName.ContainsKey(name) || byName[name].Count == 0)
        {
            return Enumerable.Empty<Product>();
        }

        return byName[name];
    }

    public IEnumerable<Product> FindProductsByProducer(string producer)
    {
        if (!byProducer.ContainsKey(producer) || byProducer[producer].Count == 0)
        {
            return Enumerable.Empty<Product>();
        }

        return byProducer[producer];
    }

    public IEnumerable<Product> FindProductsByPriceRange(decimal startPrice, decimal endPrice)
    {
        var range = byPrice.Range(startPrice, true, endPrice, true);

        foreach (var product in range)
        {
            foreach (var item in product.Value)
            {
                yield return item;
            }
        }
    }
}

