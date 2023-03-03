using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Repositories;

public class ProjectRepository : IProjectRepository
{
	private readonly CRELDBContext dbContext;
    private readonly IMapper mapper;
	
	public ProjectRepository(CRELDBContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<ProjectDto?> GetProjectDtoById(decimal id)
	{
		return (await dbContext.Projects
				.Where(entity => entity.Id == id)
				.ProjectTo<ProjectDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync());
	}
	
    public Task<PagedList<ProjectDto>> GetProjectDtos(ProjectFilter projectFilter)
	{
		return PagedList<ProjectDto>.CreateAsync(
			projectFilter.ApplyFilter(dbContext.Projects)
			.ProjectTo<ProjectDto>(mapper.ConfigurationProvider)
			.AsNoTracking()
			, projectFilter);
	}
	
	public async Task<Project> Add(Project project)
	{
		return (await dbContext.Projects.AddAsync(project)).Entity;
	}
	
	public async Task<Project?> GetProjectById(decimal id)
	{
		return (await dbContext.Projects.FirstOrDefaultAsync(entity => entity.Id == id));
	}
	
	public Project Delete(Project project)
	{
		return dbContext.Projects.Remove(project).Entity;
	}
	
}
