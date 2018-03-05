using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Wintellect.PowerCollections;

public class Enterprise : IEnterprise
{
    private Dictionary<Guid, Employee> byId;
    private Dictionary<Position, List<Employee>> byPosition;
    private Dictionary<string, List<Employee>> byFirstName;
    private Dictionary<string, List<Employee>> byFirstLastPosition;
    private OrderedDictionary<double, List<Employee>> bySalary;
    private Dictionary<string, List<Employee>> bySalaryPosition;
    private Dictionary<string, List<Employee>> byPositionSalary;

    public Enterprise()
    {
        byId = new Dictionary<Guid, Employee>();
        byPosition = new Dictionary<Position, List<Employee>>();
        byFirstName = new Dictionary<string, List<Employee>>();
        byFirstLastPosition = new Dictionary<string, List<Employee>>();
        bySalary = new OrderedDictionary<double, List<Employee>>();
        bySalaryPosition = new Dictionary<string, List<Employee>>();
        byPositionSalary = new Dictionary<string, List<Employee>>();
    }

    public int Count { get { return this.byId.Count; }}

    public void Add(Employee employee)
    {
        if (byId.ContainsKey(employee.Id))
        {
            throw new ArgumentException();
        }

        byId[employee.Id] = employee;

        if (!byPosition.ContainsKey(employee.Position))
        {
            byPosition.Add(employee.Position, new List<Employee>());
        }
        byPosition[employee.Position].Add(employee);

        if (!byFirstName.ContainsKey(employee.FirstName))
        {
            byFirstName.Add(employee.FirstName, new List<Employee>());
        }
        byFirstName[employee.FirstName].Add(employee);

        var key = employee.FirstName + employee.LastName + employee.Position;
        if (!byFirstLastPosition.ContainsKey(key))
        {
            byFirstLastPosition.Add(key, new List<Employee>());
        }
        byFirstLastPosition[key].Add(employee);

        if (!bySalary.ContainsKey(employee.Salary))
        {
            bySalary.Add(employee.Salary, new List<Employee>());
        }
        bySalary[employee.Salary].Add(employee);

        if (!bySalaryPosition.ContainsKey(employee.Salary + "" + employee.Position))
        {
            bySalaryPosition.Add(employee.Salary + "" + employee.Position, new List<Employee>());
        }
        bySalaryPosition[employee.Salary + "" + employee.Position].Add(employee);

        if (!byPositionSalary.ContainsKey(employee.Position + "" + employee.Salary))
        {
            byPositionSalary.Add(employee.Position + "" + employee.Salary, new List<Employee>());
        }
        byPositionSalary[employee.Position + "" + employee.Salary].Add(employee);
    }

    public IEnumerable<Employee> AllWithPositionAndMinSalary(Position position, double minSalary)
    {
        if (!byPositionSalary.ContainsKey(position + "" + minSalary))
        {
            return Enumerable.Empty<Employee>();
        }

        var result = byPositionSalary[position + "" + minSalary];
        return result;
    }

    public bool Change(Guid guid, Employee employee)
    {
        if (!byId.ContainsKey(guid))
        {
            return false;
        }

        //byId.Remove(guid);
        //byId[employee.Id] = employee;
        var result = byId[guid];
        result.FirstName = employee.FirstName;
        result.HireDate = employee.HireDate;
        result.LastName = employee.LastName;
        result.Position = employee.Position;
        result.Salary = employee.Salary;

        

        return true;
    }

    public bool Contains(Guid guid)
    {
        if (byId.ContainsKey(guid))
        {
            return true;
        }
        return false;
    }

    public bool Contains(Employee employee)
    {
        if (byId.ContainsKey(employee.Id))
        {
            return true;
        }

        return false;
    }

    public bool Fire(Guid guid)
    {
        if (byId.ContainsKey(guid))
        {
            byId.Remove(guid);
            return true;
        }
        return false;
    }

    public Employee GetByGuid(Guid guid)
    {
        if (!byId.ContainsKey(guid))
        {
            throw new ArgumentException();
        }
        var result = byId[guid];
        return result;
    }

    public IEnumerable<Employee> GetByPosition(Position position)
    {
        foreach (var employee in byPosition[position])
        {
            yield return employee;
        }
    }

    public IEnumerable<Employee> GetBySalary(double minSalary)
    {
        var result = bySalary.Where(x => x.Key >= minSalary).SelectMany(x=> x.Value);
        if (result.Equals(null))
        {
            throw new InvalidOperationException();
        }
        return result;
    }

    public IEnumerable<Employee> GetBySalaryAndPosition(double salary, Position position)
    {
        if (!byPosition.ContainsKey(position))
        {
            throw new InvalidOperationException();
        }

        if (!bySalary.ContainsKey(salary))
        {
            throw new InvalidOperationException();
        }
        var result = bySalaryPosition[salary + "" + position];


        return result;
    }

    public Position PositionByGuid(Guid guid)
    {
        if (!byId.ContainsKey(guid))
        {
            throw new InvalidOperationException();
        }

        var person = byId[guid];
        return person.Position;
    }

    public bool RaiseSalary(int months, int percent)
    {
        var isRaiseSalary = false;
        foreach (var employee in byId)
        {
            var days = DateTime.Now - employee.Value.HireDate;
            var timespan = days.Days/30;
            if (timespan >= months)
            {
                employee.Value.Salary += percent*employee.Value.Salary/100;
                isRaiseSalary = true;
            }
        }

        return isRaiseSalary;
    }

    public IEnumerable<Employee> SearchByFirstName(string firstName)
    {
        var result = byFirstName[firstName];
        return result;
    }

    public IEnumerable<Employee> SearchByNameAndPosition(string firstName, string lastName, Position position)
    {
        var result = byFirstLastPosition[firstName + lastName + position];
        return result;
    }

    public IEnumerable<Employee> SearchByPosition(IEnumerable<Position> positions)
    {
        List<Employee> list = new List<Employee>();
        foreach (var position in positions)
        {
            if (byPosition.ContainsKey(position))
            {
                var employee = byPosition[position];
                list.AddRange(employee);
            }
        }
        return list;
    }

    public IEnumerable<Employee> SearchBySalary(double minSalary, double maxSalary)
    {
        var list = new List<Employee>();
        var range = bySalary.Range(minSalary, true, maxSalary, true);
        foreach (var kvp in range)
        {
            list.AddRange(kvp.Value);
        }

        return list;
    }

    public IEnumerator<Employee> GetEnumerator()
    {
        foreach (var employee in byId)
        {
            yield return employee.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}

