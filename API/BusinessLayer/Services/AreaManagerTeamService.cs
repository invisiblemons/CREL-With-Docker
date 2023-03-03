using CREL_BE.Entities;
using AutoMapper;
using CREL_BE.Repositories;

namespace CREL_BE.Services;

public class AreaManagerTeamService : IAreaManagerTeamService
{
    private readonly IUnitOfWorkRepository unitOfWork;
    private readonly IMapper mapper;

    public AreaManagerTeamService(IUnitOfWorkRepository unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<AreaManagerTeam> Add(AreaManagerTeam industryProperty)
    {
        return await unitOfWork.AreaManagerTeamRepository.Add(industryProperty);
    }

    public async Task<AreaManagerTeam?> GetAreaManagerTeamyByAreaManagerIdTeamId(decimal areaManagerId, decimal teamyId)
    {
        return await unitOfWork.AreaManagerTeamRepository.GetAreaManagerTeamyByAreaManagerIdTeamId(areaManagerId, teamyId);
    }

    public AreaManagerTeam Delete(AreaManagerTeam industryProperty)
    {
        return unitOfWork.AreaManagerTeamRepository.Delete(industryProperty);
    }

}
