using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;

namespace CREL_BE.Services;

public interface IProjectService
{
    Task<ProjectDto?> GetProjectDtoById(decimal id);
    Task<PagedList<ProjectDto>> GetProjectDtos(ProjectFilter projectFilter);
	Task<Project> Add(Project project);
	Task<Project?> GetProjectById(decimal id);
	Project Delete(Project project);
}
