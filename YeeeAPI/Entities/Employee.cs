﻿namespace YeeeAPI.Entites
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public required string FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; } = string.Empty;

        public string? Gender { get; set; }

        public int DepartmentID { get; set; }

        public string? JobTitle { get; set; }


    }
}
