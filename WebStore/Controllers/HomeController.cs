using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Blog() => View();

        public IActionResult BlogSingle() => View();

        public IActionResult Cart() => View();

        public IActionResult Error404() => View();

        public IActionResult Checkout() => View();

        public IActionResult ContactUs() => View();

        public IActionResult Login() => View();

        public IActionResult ProductDetails() => View();

        public IActionResult Shop() => View();


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
            EmployeesInfoProvider.Employees = EmployeesInfoProvider.Employees.Where( emp => emp.Id != id);

            return View("EmployeesList", EmployeesInfoProvider.Employees);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee employee = EmployeesInfoProvider.Employees.FirstOrDefault(emp => emp.Id == id);

            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            Employee emp = EmployeesInfoProvider.Employees.FirstOrDefault( item => item.Id == employee.Id);

            if (emp != null) {
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
