using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Models;
using WebStore.Services;
using WebStore.ViewModels;

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

            if (employee is not null)
                return View(employee);
            else
                return View("~/Views/Home/Error404.cshtml");
        }

        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            Employee employee = _employeesDataService.GetById(id);

            return View(new EmployeeViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Surename = employee.Surename,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
                Gender = employee.Gender,
                Profession = employee.Profession
            });
        }

        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            if(id <= 0)
                return BadRequest();

            _employeesDataService.Remove(id);

            return View("EmployeesList", _employeesDataService.GetAll());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id <= 0)
                return BadRequest();

            Employee employee = _employeesDataService.GetById(id);

            if(employee is null)
                return View("~/Views/Home/Error404.cshtml");
            else
                return View(new EmployeeViewModel() { 
                    Id = employee.Id,
                    Name = employee.Name,
                    Surename = employee.Surename,
                    Patronymic = employee.Patronymic,
                    Age = employee.Age,
                    Gender = employee.Gender,
                    Profession = employee.Profession
                });
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel employeeViewModel)
        {
            if (employeeViewModel is null)
                throw new ArgumentNullException(nameof(employeeViewModel));

            Employee employee = new Employee()
            {
                Id = employeeViewModel.Id,
                Name = employeeViewModel.Name,
                Surename = employeeViewModel.Surename,
                Patronymic = employeeViewModel.Patronymic,
                Age = employeeViewModel.Age,
                Gender = employeeViewModel.Gender,
                Profession = employeeViewModel.Profession
            };

            _employeesDataService.Edit(employee);

            return View("EmployeesList", _employeesDataService.GetAll());
        }

        [HttpGet]
        public IActionResult Add() {
            return View();
        }

        [HttpPost]
        public IActionResult Add(EmployeeViewModel employeeViewModel)
        {
            if (employeeViewModel is null)
                throw new ArgumentNullException(nameof(employeeViewModel));

            Employee employee = new Employee()
            {
                Id = employeeViewModel.Id,
                Name = employeeViewModel.Name,
                Surename = employeeViewModel.Surename,
                Patronymic = employeeViewModel.Patronymic,
                Age = employeeViewModel.Age,
                Gender = employeeViewModel.Gender,
                Profession = employeeViewModel.Profession
            };

            _employeesDataService.Add(employee);

            return View("EmployeesList", _employeesDataService.GetAll());
        }
    }
}
