using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Services
{
    interface IEmployeesDataService
    {
        public IList<Employee> GetAll();

        public Employee GetById(int id);

        public IList<Employee> GetByName(string Name, string Surename = null, string Patronymic = null);

        public int Add(Employee employee);

        public void Edit(int id);

        public void Remove(int id);
    }
}
