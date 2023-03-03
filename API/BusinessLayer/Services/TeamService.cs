using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Repositories;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;

namespace CREL_BE.Services;

public class TeamService : ITeamService
{
    private readonly IUnitOfWorkRepository unitOfWork;
    private readonly IMapper mapper;

    public TeamService(IUnitOfWorkRepository unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<TeamDto?> GetTeamDtoById(decimal id)
    {
        return await unitOfWork.TeamRepository.GetTeamDtoById(id);
    }

    public Task<PagedList<TeamDto>> GetTeamDtos(TeamFilter teamFilter)
    {
        return unitOfWork.TeamRepository.GetTeamDtos(teamFilter);
    }

    public async Task<Team> Add(Team team)
    {
        return await unitOfWork.TeamRepository.Add(team);
    }

    public async Task<Team?> GetTeamById(decimal id)
    {
        return await unitOfWork.TeamRepository.GetTeamById(id);
    }

    public Team Delete(Team team)
    {
        return unitOfWork.TeamRepository.Delete(team);
    }

}
