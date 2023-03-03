using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Repositories;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;

namespace CREL_BE.Services;

public class BrandRequestService : IBrandRequestService
{
	private readonly IUnitOfWorkRepository unitOfWork;
    private readonly IMapper mapper;
	
	public BrandRequestService(IUnitOfWorkRepository unitOfWork, IMapper mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}

	public async Task<BrandRequestDto?> GetBrandRequestDtoById(decimal id)
	{
		return await unitOfWork.BrandRequestRepository.GetBrandRequestDtoById(id);
	}
	
    public Task<PagedList<BrandRequestDto>> GetBrandRequestDtos(BrandRequestFilter brandRequestFilter)
	{
		return unitOfWork.BrandRequestRepository.GetBrandRequestDtos(brandRequestFilter);
	}
	
	public async Task<BrandRequest> Add(BrandRequest brandRequest)
	{
		return await unitOfWork.BrandRequestRepository.Add(brandRequest);
	}
	
	public async Task<BrandRequest?> GetBrandRequestById(decimal id)
	{
		return await unitOfWork.BrandRequestRepository.GetBrandRequestById(id);
	}
	
	public BrandRequest Delete(BrandRequest brandRequest)
	{
		return unitOfWork.BrandRequestRepository.Delete(brandRequest);
	}
	
}
