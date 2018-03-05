﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Product : IComparable<Product>
{
    public Product(string name, decimal price, string producer)
    {
        Name = name;
        Price = price;
        Producer = producer;
    }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public string Producer { get; set; }

    public int CompareTo(Product other)
    {
        var compare = this.Name.CompareTo(other.Name);
        if (compare == 0)
        {
            compare = this.Producer.CompareTo(other.Producer);
        }
        if (compare == 0)
        {
            compare = this.Price.CompareTo(other.Price);
        }
        return compare;
    }

    public override string ToString()
    {
        return this.Name + "" + this.Producer + "" + this.Price;
    }
}

