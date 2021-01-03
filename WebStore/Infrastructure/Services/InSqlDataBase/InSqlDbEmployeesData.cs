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
            _db.Add(employee);
            _db.SaveChanges();

            return employee != null ? employee.Id : 0;
        }

        public void Edit(Employee employee)
        {
            _db.Entry(employee).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public IList<Employee> GetAll()
        {
            IQueryable<Employee> employees = _db.Employees.Include(emp => emp.Profession);

            return employees.ToList<Employee>();
        }

        public Employee GetById(int id)
        {
            return _db.Employees.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Profession> GetProfessions()
        {
            return _db.Professions;
        }

        public bool Remove(int id)
        {
            Employee employee = _db.Employees.FirstOrDefault(e => e.Id == id);

            if (employee is not null) {
                _db.Employees.Remove(employee);
                _db.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}
