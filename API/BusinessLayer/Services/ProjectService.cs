using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Repositories;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;

namespace CREL_BE.Services;

public class ProjectService : IProjectService
{
	private readonly IUnitOfWorkRepository unitOfWork;
    private readonly IMapper mapper;
	
	public ProjectService(IUnitOfWorkRepository unitOfWork, IMapper mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}

	public async Task<ProjectDto?> GetProjectDtoById(decimal id)
	{
		return await unitOfWork.ProjectRepository.GetProjectDtoById(id);
	}
	
    public Task<PagedList<ProjectDto>> GetProjectDtos(ProjectFilter projectFilter)
	{
		return unitOfWork.ProjectRepository.GetProjectDtos(projectFilter);
	}
	
	public async Task<Project> Add(Project project)
	{
		return await unitOfWork.ProjectRepository.Add(project);
	}
	
	public async Task<Project?> GetProjectById(decimal id)
	{
		return await unitOfWork.ProjectRepository.GetProjectById(id);
	}
	
	public Project Delete(Project project)
	{
		return unitOfWork.ProjectRepository.Delete(project);
	}
	
}
