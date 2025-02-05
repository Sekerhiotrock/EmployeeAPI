﻿using Microsoft.EntityFrameworkCore;
using YeeeAPI.Data;
using YeeeAPI.Entites;

namespace YeeeAPI.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;
        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<object>> GetEmployees()
        {
            var data = await (from emp in _context.Employees
                              join dept in _context.Departments on emp.DepartmentID equals dept.DepartmentID
                              join proj in _context.Projects on dept.DepartmentID equals proj.DepartmentID into projects
                              select new
                              {
                                  emp.FirstName,
                                  emp.LastName,
                                  emp.Email,
                                  emp.Gender,
                                  dept.DepartmentName,
                                  emp.JobTitle,
                                  Projects = projects.Select(p => p.ProjectName).ToList()
                              }).ToListAsync<object>();
            return data;
        }
        public async Task<Employee> UpdateEmployee(Employee updatedEmp)
        {
            var dbEmp = await _context.Employees.FindAsync(updatedEmp.EmployeeID);

            dbEmp.FirstName = updatedEmp.FirstName;
            dbEmp.LastName = updatedEmp.LastName;
            dbEmp.Email = updatedEmp.Email;
            dbEmp.Gender = updatedEmp.Gender;
            dbEmp.DepartmentID = updatedEmp.DepartmentID;
            dbEmp.JobTitle = updatedEmp.JobTitle;

            _context.Entry(dbEmp).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return dbEmp;
        }
        public async Task AddEmployee(Employee addEmp)
        {
            _context.Employees.Add(addEmp);
            await _context.SaveChangesAsync();
        }
        public List<Employee> RemoveEmployee(int id)
        {
            var dbEmp = _context.Employees.Find(id);
            if (dbEmp == null) return null;
            _context.Employees.Remove(dbEmp);
            _context.Entry(dbEmp).State = EntityState.Deleted;
            _context.SaveChangesAsync();
            return _context.Employees.ToList();
        }
        public async Task<List<object>> SearchEmployees(string? text)
        {
            var data = await (from emp in _context.Employees
                              join dept in _context.Departments on emp.DepartmentID equals dept.DepartmentID
                              join proj in _context.Projects on dept.DepartmentID equals proj.DepartmentID into projects
                              where string.IsNullOrEmpty(text)
                                    || emp.FirstName.Contains(text)
                                    || emp.LastName.Contains(text)
                                    || emp.JobTitle.Contains(text)
                              select new
                              {
                                  emp.FirstName,
                                  emp.LastName,
                                  emp.Email,
                                  emp.Gender,
                                  dept.DepartmentName,
                                  emp.JobTitle,
                                  Projects = projects.Select(p => p.ProjectName).ToList()
                              }).ToListAsync<object>();
            return data;
        }
    }
}