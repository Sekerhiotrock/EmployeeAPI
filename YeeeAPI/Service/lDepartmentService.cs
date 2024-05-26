using YeeeAPI.Entities;
using YeeeAPI.Entities;

namespace YeeeAPI.Service
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetDepartments();
        Task<List<object>> GetDepartmentById(int id);
        Task<Department> UpdateDepartment(Department updatedDept);
        Task AddDepartment(Department addDept);
        Task<Department> RemoveDepartment(int id);
        Task<List<object>> SearchDepartments(string text);
    }
}