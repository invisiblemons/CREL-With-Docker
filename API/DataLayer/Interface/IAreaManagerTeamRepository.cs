using CREL_BE.Entities;

namespace CREL_BE.Repositories;

public interface IAreaManagerTeamRepository
{
	Task<AreaManagerTeam> Add(AreaManagerTeam industryProperty);
	Task<AreaManagerTeam?> GetAreaManagerTeamyByAreaManagerIdTeamId(decimal areaManagerId,decimal teamyId);
	AreaManagerTeam Delete(AreaManagerTeam industryProperty);
}
