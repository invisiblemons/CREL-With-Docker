using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Repositories;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;

namespace CREL_BE.Services;

public class IndustryService : IIndustryService
{
	private readonly IUnitOfWorkRepository unitOfWork;
    private readonly IMapper mapper;
	
	public IndustryService(IUnitOfWorkRepository unitOfWork, IMapper mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}

	public async Task<IndustryDto?> GetIndustryDtoById(decimal id)
	{
		return await unitOfWork.IndustryRepository.GetIndustryDtoById(id);
	}
	
    public Task<PagedList<IndustryDto>> GetIndustryDtos(IndustryFilter industryFilter)
	{
		return unitOfWork.IndustryRepository.GetIndustryDtos(industryFilter);
	}
	
	public async Task<Industry> Add(Industry industry)
	{
		return await unitOfWork.IndustryRepository.Add(industry);
	}
	
	public async Task<Industry?> GetIndustryById(decimal id)
	{
		return await unitOfWork.IndustryRepository.GetIndustryById(id);
	}
	
	public Industry Delete(Industry industry)
	{
		return unitOfWork.IndustryRepository.Delete(industry);
	}
	
}
