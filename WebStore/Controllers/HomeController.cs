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
        public IActionResult EmployeesList()
        {
            return View(EmployeesInfoProvider.Employees);
        }

        public IActionResult Details(int id)
        {
            Employee employee = EmployeesInfoProvider.Employees.FirstOrDefault(emp => emp.Id == id);

            return View(employee);
        }
    }
}
