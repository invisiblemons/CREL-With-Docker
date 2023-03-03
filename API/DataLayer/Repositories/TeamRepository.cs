using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Repositories;

public class TeamRepository : ITeamRepository
{
	private readonly CRELDBContext dbContext;
    private readonly IMapper mapper;
	
	public TeamRepository(CRELDBContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<TeamDto?> GetTeamDtoById(decimal id)
	{
		return (await dbContext.Teams
				.Where(entity => entity.Id == id)
				.ProjectTo<TeamDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync());
	}
	
    public Task<PagedList<TeamDto>> GetTeamDtos(TeamFilter teamFilter)
	{
		return PagedList<TeamDto>.CreateAsync(
			teamFilter.ApplyFilter(dbContext.Teams)
			.ProjectTo<TeamDto>(mapper.ConfigurationProvider)
			.AsNoTracking()
			, teamFilter);
	}
	
	public async Task<Team> Add(Team team)
	{
		return (await dbContext.Teams.AddAsync(team)).Entity;
	}
	
	public async Task<Team?> GetTeamById(decimal id)
	{
		return (await dbContext.Teams.FirstOrDefaultAsync(entity => entity.Id == id));
	}
	
	public Team Delete(Team team)
	{
		return dbContext.Teams.Remove(team).Entity;
	}
	
}
