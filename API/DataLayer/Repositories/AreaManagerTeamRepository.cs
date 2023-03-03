using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Repositories;

public class AreaManagerTeamRepository : IAreaManagerTeamRepository
{
    private readonly CRELDBContext dbContext;
    private readonly IMapper mapper;

    public AreaManagerTeamRepository(CRELDBContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<AreaManagerTeam> Add(AreaManagerTeam industryProperty)
    {
        return (await dbContext.AreaManagerTeams.AddAsync(industryProperty)).Entity;
    }

    public async Task<AreaManagerTeam?> GetAreaManagerTeamyByAreaManagerIdTeamId(decimal areaManagerId, decimal teamyId)
    {
        return (await dbContext.AreaManagerTeams.FirstOrDefaultAsync(entity => entity.AreaManagerId == areaManagerId && entity.TeamId == teamyId && entity.EndDate == null));
    }

    public AreaManagerTeam Delete(AreaManagerTeam industryProperty)
    {
        return dbContext.AreaManagerTeams.Remove(industryProperty).Entity;
    }

}
