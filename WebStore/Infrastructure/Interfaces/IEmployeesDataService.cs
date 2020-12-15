using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Services
{
    public interface IEmployeesDataService
    {
        public IList<Employee> GetAll();

        public Employee GetById(int id);

        public int Add(Employee employee);

        public void Edit(Employee employee);

        public bool Remove(int id);
    }
}
