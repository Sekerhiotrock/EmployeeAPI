using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YeeeAPI.Entites;
using YeeeAPI.Service;

namespace YeeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("ShowAllEmployee")]
        public async Task<ActionResult<List<object>>> GetEmployee()
        {
            var result = await _employeeService.GetEmployees();
            return Ok(result);
        }

        [HttpPut("UpdateEmployee")]
        public async Task<ActionResult> UpdateEmployee(Employee updatedEmp)
        {
            if (string.IsNullOrEmpty(updatedEmp.FirstName))
            {
                return BadRequest("Please Enter Employee's Firstname.");
            }

            if (updatedEmp.DepartmentID == 0)
            {
                return BadRequest("Please Enter DepartmentID.");
            }

            await _employeeService.UpdateEmployee(updatedEmp);
            return Ok("Employee was updated successfully");
        }

        [HttpPost("NewEmployee")]
        public async Task<ActionResult<List<object>>> AddEmployee(Employee addEmp)
        {
            if (string.IsNullOrEmpty(addEmp.FirstName))
            {
                return BadRequest("Please Enter Employee's firstname.");
            }

            if (addEmp.DepartmentID == 0)
            {
                return BadRequest("Please Enter DepartmentID.");
            }

            await _employeeService.AddEmployee(addEmp);
            return Ok("Employee was added successfully");
        }

        [HttpDelete("DeleteEmployee")]
        public ActionResult<List<Employee>> RemoveEmployee(int id)
        {

            var result = _employeeService.RemoveEmployee(id);
            return result is null ? NotFound("Employee was Not Found.") : Ok("Congratulations!! Employee was removed successfully.");
        }

        [HttpGet("SearchEmployee")]
        public async Task<ActionResult<List<object>>> SearchEmployees(string? text)
        {
            var result = await _employeeService.SearchEmployees(text);
            return result.Count > 0 ? Ok(result) : NotFound("Employee was Not Found.");
        }
    }

}