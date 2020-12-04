using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Services
{
    public class InMemoryEmployeesData : IEmployeesDataService
    {
        IList<Employee> _employees;

        public InMemoryEmployeesData()
        {
            _employees = EmployeesInfoProvider.Employees;
        }

        public int? Add(Employee employee)
        {
            if (employee is null)
                return null;

            employee.Id = _employees.Max(x => x.Id) + 1;
            _employees.Add(employee);

            return employee.Id;
        }

        public bool Edit(Employee employee)
        {
            if (employee is null)
                return false;

            Employee emp = _employees.FirstOrDefault(e => e.Id == employee.Id);

            if (emp is null)
                return false;

            emp.Id = employee.Id;
            emp.Name = employee.Name;
            emp.Surename = employee.Surename;
            emp.Patronymic = employee.Patronymic;
            emp.Age = employee.Age;
            emp.Gender = employee.Gender;
            emp.Profession = employee.Profession;

            return true;
        }

        public IList<Employee> GetAll() => _employees;

        public Employee GetById(int id)
        {
            return _employees.FirstOrDefault(x => x.Id == id);
        }        

        public bool Remove(int id)
        {
            Employee emp = GetById(id);

            if (emp is null)
                return false;

            _employees.Remove(emp);

            return true;
        }
    }
}
