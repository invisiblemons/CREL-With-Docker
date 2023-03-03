using CREL_BE.Entities;
using AutoMapper;
using CREL_BE.Repositories;

namespace CREL_BE.Services;

public class IndustryLocationService : IIndustryLocationService
{
	private readonly IUnitOfWorkRepository unitOfWork;
    private readonly IMapper mapper;
	
	public IndustryLocationService(IUnitOfWorkRepository unitOfWork, IMapper mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}
	
	public async Task<IndustryLocation> Add(IndustryLocation industryProperty)
	{
		return await unitOfWork.IndustryLocationRepository.Add(industryProperty);
	}
	
	public async Task<IndustryLocation?> GetIndustryLocationByLocationIdIndustryId(decimal locationId,decimal industryId)
	{
		return await unitOfWork.IndustryLocationRepository.GetIndustryLocationByLocationIdIndustryId(locationId, industryId);
	}
	
	public IndustryLocation Delete(IndustryLocation industryProperty)
	{
		return unitOfWork.IndustryLocationRepository.Delete(industryProperty);
	}
	
}
