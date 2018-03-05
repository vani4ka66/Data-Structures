using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());
        ProductCollection colection = new ProductCollection();


        for (int i = 0; i < n; i++)
        {
            string input = Console.ReadLine();
            string[] argums = input.Split();
            string command = argums[0];

            switch (command)
            {
                case "AddProduct":
                    AddMethod(input, colection);
                    break;
                case "DeleteProducts":
                    DeleteMethod(input, argums, colection);
                    break;
                case "FindProductsByName":
                    FindByNameMethod(input, colection);
                    break;
                case "FindProductsByProducer":
                    FindByProducerMethod(input, colection);
                    break;
                case "FindProductsByPriceRange":
                    FindByPriceRangeMethod(input, colection);
                    break;
            }
        }
    }

    private static void AddMethod(string input, ProductCollection colection)
    {
        int index = input.IndexOf(' ');
        string input2 = input.Substring(index + 1);
        string[] separatedByComma = input2.Split(';');
        string name = separatedByComma[0];
        decimal price = decimal.Parse(separatedByComma[1]);
        string producer = separatedByComma[2];

        colection.Add(name, price, producer);
        Console.WriteLine("Product added");
    }

    private static void DeleteMethod(string input, string[] argums, ProductCollection colection)
    {
        int index;
        string input2;
        index = input.IndexOf(' ');
        input2 = input.Substring(index + 1);

        if (!input2.Contains(';'))
        {
            string a = argums[1];
            int deletedItems = colection.Delete(argums[1]);
            Console.WriteLine($"{deletedItems} products deleted");
        }
        else
        {
            string[] separatedByComma = input2.Split(';');
            string name = separatedByComma[0];
            string producer = separatedByComma[1];

            int deletedItems = colection.Delete(name, producer);
            if (deletedItems == 0)
            {
                Console.WriteLine("No products found");
            }
            else
            {
                Console.WriteLine($"{deletedItems} products deleted");
            }
        }
    }

    private static void FindByNameMethod(string input, ProductCollection colection)
    {
        int index;
        index = input.IndexOf(' ');
        string findName = input.Substring(index + 1);
        var product = colection.FindProductsByName(findName);

        if (!product.Any())
        {
            Console.WriteLine("No products found");
        }
        else
        {
            foreach (var item in product)
            {
                Console.Write("{");
                Console.Write("{0};{1};{2:f2}", item.Name, item.Producer, item.Price);
                Console.WriteLine("}");
            }
        }
    }

    private static void FindByProducerMethod(string input, ProductCollection colection)
    {
        int index;
        IEnumerable<Product> product;
        index = input.IndexOf(' ');
        var findProducer = input.Substring(index + 1);
        product = colection.FindProductsByProducer(findProducer);

        if (!product.Any())
        {
            Console.WriteLine("No products found");
        }
        else
        {
            foreach (var item in product)
            {
                Console.Write("{");
                Console.Write("{0};{1};{2:f2}", item.Name, item.Producer, item.Price);
                Console.WriteLine("}");
            }
        }
    }

    private static void FindByPriceRangeMethod(string input, ProductCollection colection)
    {
        int index;
        string input2;
        string[] separatedByComma;
        index = input.IndexOf(' ');
        input2 = input.Substring(index + 1);
        separatedByComma = input2.Split(';');
        decimal startPrice = decimal.Parse(separatedByComma[0]);
        decimal endPrice = decimal.Parse(separatedByComma[1]);

        var list = colection.FindProductsByPriceRange(startPrice, endPrice);
        if (!list.Any())
        {
            Console.WriteLine("No products found");
        }
        else
        {
            foreach (var item in list)
            {
                Console.Write("{");
                Console.Write("{0};{1};{2:f2}", item.Name, item.Producer, item.Price);
                Console.WriteLine("}");
            }
        }
    }
   
}

