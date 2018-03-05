using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;

public class Program
{
    static void Main(string[] args)
    {
        FirstLastList<int> list = new FirstLastList<int>();
        /*list.Add(5);
        list.Add(10);
        list.Add(-2);
        list.Add(10);
        list.Add(7);
        list.Add(70);*/


        /*Console.WriteLine(list.Count);

        var removed = list.RemoveAll(10);
        var returnedItems = list.First(3).ToList();

        Console.WriteLine(removed);
        Console.WriteLine(string.Join(" ", returnedItems));
        //Console.WriteLine("List: " + string.Join(" ", list.Count));*/

        var items = FirstLastListFactory.Create<Product>();
        items.Add(new Product(1.11m, "first"));
        items.Add(new Product(0.50m, "coffee"));
        items.Add(new Product(2.50m, "chocolate"));
        items.Add(new Product(1.20m, "mint drops"));
        items.Add(new Product(1.20m, "beer"));
        items.Add(new Product(0.50m, "candy"));
        items.Add(new Product(1.20m, "cola"));

        var countRemoved = items.RemoveAll(new Product(1.20m, null));
        var returnedItems = items.Last(3).Select(p => p.Title).ToList();

        Console.WriteLine(string.Join(" ", returnedItems));

        // Assert
        //Assert.AreEqual(3, countRemoved);
        var expectedItems = new string[] { "candy", "chocolate", "coffee" };
        //CollectionAssert.AreEqual(expectedItems, returnedItems);
    }

    public class Product : IComparable<Product>
    {
        public string Title { get; set; }
        public decimal Price { get; set; }

        public Product(decimal price, string title)
        {
            this.Price = price;
            this.Title = title;
        }

        public int CompareTo(Product other)
        {
            return this.Price.CompareTo(other.Price);

        }
    }
}
