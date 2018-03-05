using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        IEnterprise col = new Enterprise();

        Guid g = new Guid("6e844a79-9f79-45f0-8fa8-3051791eba87");
        var employee = new Employee("Vania", "Dimitrova", 1000, Position.Developer, DateTime.Today);
        var employe2 = new Employee("Pesho", "Dimitrov", 1000, Position.Developer, DateTime.Today);

        col.Add(employee);
        col.Add(employe2);

        col.RaiseSalary(0, 20);


    }
}
