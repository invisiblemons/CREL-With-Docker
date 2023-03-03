using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Repositories;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;

namespace CREL_BE.Services;

public class WardService : IWardService
{
	private readonly IUnitOfWorkRepository unitOfWork;
    private readonly IMapper mapper;
	
	public WardService(IUnitOfWorkRepository unitOfWork, IMapper mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}

	public async Task<WardDto?> GetWardDtoById(decimal id)
	{
		return await unitOfWork.WardRepository.GetWardDtoById(id);
	}
	
    public Task<PagedList<WardDto>> GetWardDtos(WardFilter wardFilter)
	{
		return unitOfWork.WardRepository.GetWardDtos(wardFilter);
	}
	
	public async Task<Ward> Add(Ward ward)
	{
		return await unitOfWork.WardRepository.Add(ward);
	}
	
	public async Task<Ward?> GetWardById(decimal id)
	{
		return await unitOfWork.WardRepository.GetWardById(id);
	}
	
	public Ward Delete(Ward ward)
	{
		return unitOfWork.WardRepository.Delete(ward);
	}
	
}
