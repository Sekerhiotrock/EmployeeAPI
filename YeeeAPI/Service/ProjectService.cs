﻿using YeeeAPI.Entites;
using YeeeAPI.Repository;

namespace YeeeAPI.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<List<object>> GetProjects()
        {
            var result = await _projectRepository.GetProjects();
            return result;
        }

        public async Task<Project> UpdateProject(Project updatedProj)
        {
            var result = await _projectRepository.UpdateProject(updatedProj);
            return result;
        }

        public async Task AddProject(Project addProj)
        {
            await _projectRepository.AddProject(addProj);
        }

        public List<Project> RemoveProject(int id)
        {
            var result = _projectRepository.RemoveProject(id);
            return result;
        }

        public async Task<List<object>> SearchProjects(string text)
        {
            var result = await _projectRepository.SearchProjects(text);
            return result;
        }
    }
}