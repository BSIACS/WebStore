﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Domain.Employees;
using WebStore.Services;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    [Authorize(Roles = "Administrators")]
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
                return View(new EmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Surename = employee.Surename,
                    Patronymic = employee.Patronymic,
                    Age = employee.Age,
                    Gender = employee.Gender,
                    Profession = _employeesDataService.GetProfessions().FirstOrDefault(p => p.Id == employee.ProfessionId),
                });
            else
                return NotFound();
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
                Profession = _employeesDataService.GetProfessions().FirstOrDefault(p => p.Id == employee.ProfessionId),
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

            if (employee is null)
                return NotFound();
            else
            {
                var professions = _employeesDataService.GetProfessions();

                ViewBag.Professions = new SelectList(professions, "Id", "Name", "4");

                return View(new EmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Surename = employee.Surename,
                    Patronymic = employee.Patronymic,
                    Age = employee.Age,
                    Gender = employee.Gender,
                    ProfessionId = _employeesDataService.GetProfessions().FirstOrDefault(e => e.Id == employee.ProfessionId).Id,                    
                });
            }    
                
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel employeeViewModel)
        {
            if (!ModelState.IsValid)
                return View(employeeViewModel);

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
                ProfessionId = _employeesDataService.GetProfessions().FirstOrDefault(e => e.Id == employeeViewModel.ProfessionId).Id,
            };

            _employeesDataService.Edit(employee);

            return View("EmployeesList", _employeesDataService.GetAll());
        }

        [HttpGet]
        public IActionResult Add() {
            var professions = _employeesDataService.GetProfessions();

            ViewBag.Professions = new SelectList(professions, "Id", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult Add(EmployeeViewModel employeeViewModel)
        {
            if (!ModelState.IsValid)
                return View(employeeViewModel);

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
                Profession = _employeesDataService.GetProfessions().Where(p => p.Id == employeeViewModel.ProfessionId).FirstOrDefault(),
            };

            _employeesDataService.Add(employee);

            return View("EmployeesList", _employeesDataService.GetAll());
        }
    }
}
