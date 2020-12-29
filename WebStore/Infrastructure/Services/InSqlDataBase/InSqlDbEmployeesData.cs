using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Employees.DAL;
using WebStore.Services;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Employees;

namespace WebStore.Infrastructure.Services.InSqlDataBase
{
    public class InSqlDbEmployeesData : IEmployeesDataService
    {
        private EmployeesDb _db;

        public InSqlDbEmployeesData(EmployeesDb db)
        {
            _db = db;
        }

        public int Add(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void Edit(Employee employee)
        {
            throw new NotImplementedException();
        }

        public IList<Employee> GetAll()
        {
            IQueryable<Employee> employees = _db.Employees.Include(emp => emp.Profession);
            //IList<Employee> emp = employees.ToList<Employee>();

            return employees.ToList<Employee>();
        }

        public Employee GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
