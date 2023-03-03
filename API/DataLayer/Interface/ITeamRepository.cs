using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;

namespace CREL_BE.Repositories;

public interface ITeamRepository
{
    Task<TeamDto?> GetTeamDtoById(decimal id);
    Task<PagedList<TeamDto>> GetTeamDtos(TeamFilter teamFilter);
	Task<Team> Add(Team team);
	Task<Team?> GetTeamById(decimal id);
	Team Delete(Team team);
}
