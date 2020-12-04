using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Models;
using WebStore.Services;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        IEmployeesDataService _employeesDataService;

        public EmployeesController(IEmployeesDataService employeesDataService)
        {
            _employeesDataService = employeesDataService;
        }

        public IActionResult EmployeesList()
        {
            return View(_employeesDataService.GetAll());
        }

        public IActionResult Details(int id)
        {
            Employee employee = _employeesDataService.GetById(id);

            if(employee is not null)
                return View(employee);
            else
                return View("~/Views/Home/Error404.cshtml");
        }

        public IActionResult Delete(int id)
        {
            if(_employeesDataService.Remove(id))
                return View("EmployeesList", EmployeesInfoProvider.Employees);
            else
                return View("~/Views/Home/Error404.cshtml");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee employee = _employeesDataService.GetById(id);

            if(employee is null)
                return View("~/Views/Home/Error404.cshtml");
            else
                return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            _employeesDataService.Edit(employee);

            return View("EmployeesList", EmployeesInfoProvider.Employees);
        }
    }
}
