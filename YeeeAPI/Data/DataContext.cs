using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using YeeeAPI.Entites;
using YeeeAPI.Entities;

namespace YeeeAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}

