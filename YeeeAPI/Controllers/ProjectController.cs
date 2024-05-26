using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YeeeAPI.Entites;
using YeeeAPI.Service;

namespace YeeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("GetAllProjects")]
        public async Task<ActionResult<List<object>>> GetProjects()
        {
            var project = await _projectService.GetProjects();
            return Ok(project);
        }

        [HttpPut("UpdateProjects")]
        public async Task<ActionResult<Project>> UpdateProject(Project updatedProj)
        {
            if (string.IsNullOrEmpty(updatedProj.ProjectName))
            {
                return BadRequest("Please Enter Project's name.");
            }

            if (updatedProj.DepartmentID == 0)
            {
                return BadRequest("Please Enter DepartmentID.");
            }
            await _projectService.UpdateProject(updatedProj);
            return Ok(updatedProj);
        }

        [HttpPost("AddNewProject")]
        public async Task<ActionResult> AddProject(Project addProj)
        {
            if (string.IsNullOrEmpty(addProj.ProjectName))
            {
                return BadRequest("Please Enter Project's name.");
            }

            if (addProj.DepartmentID == 0)
            {
                return BadRequest("Please Enter DepartmentID.");
            }
            await _projectService.AddProject(addProj);
            return Ok(addProj);
        }

        [HttpDelete("DeleteProject")]
        public ActionResult<List<Project>> DeleteProject(int id)
        {
            var result = _projectService.RemoveProject(id);
            return result is null ? NotFound("Project was Not Found.") : Ok("Successfully!!.");
        }

        [HttpGet("SearchProjects")]
        public async Task<ActionResult<List<object>>> SearchProjects(string? text)
        {
            var result = await _projectService.SearchProjects(text);
            return result.Count > 0 ? Ok(result) : NotFound("Your Project was Not Found.");
        }
    }
}