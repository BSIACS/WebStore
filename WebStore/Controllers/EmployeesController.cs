using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult EmployeesList()
        {
            return View(EmployeesInfoProvider.Employees);
        }

        public IActionResult Details(int id)
        {
            Employee employee = EmployeesInfoProvider.Employees.FirstOrDefault(emp => emp.Id == id);

            return View(employee);
        }

        public IActionResult Delete(int id)
        {
            Employee employee = EmployeesInfoProvider.Employees.FirstOrDefault(emp => emp.Id == id);

            if (employee == null)
                return View("~/Views/Home/Error404.cshtml");

            EmployeesInfoProvider.Employees.Remove(employee);
            return View("EmployeesList", EmployeesInfoProvider.Employees);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee employee = EmployeesInfoProvider.Employees.FirstOrDefault(emp => emp.Id == id);

            if (employee == null)
                return View("~/Views/Home/Error404.cshtml");

            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            Employee emp = EmployeesInfoProvider.Employees.FirstOrDefault(item => item.Id == employee.Id);

            if (emp != null)
            {
                emp.Name = employee.Name;
                emp.Surename = employee.Surename;
                emp.Patronymic = employee.Patronymic;
                emp.Age = employee.Age;
                emp.Profession = employee.Profession;
            }

            return View("EmployeesList", EmployeesInfoProvider.Employees);
        }
    }
}
