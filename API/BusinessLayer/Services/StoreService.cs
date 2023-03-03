using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Repositories;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;

namespace CREL_BE.Services;

public class StoreService : IStoreService
{
	private readonly IUnitOfWorkRepository unitOfWork;
    private readonly IMapper mapper;
	
	public StoreService(IUnitOfWorkRepository unitOfWork, IMapper mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}

	public async Task<StoreDto?> GetStoreDtoById(decimal id)
	{
		return await unitOfWork.StoreRepository.GetStoreDtoById(id);
	}
	
    public Task<PagedList<StoreDto>> GetStoreDtos(StoreFilter storeFilter)
	{
		return unitOfWork.StoreRepository.GetStoreDtos(storeFilter);
	}
	
	public async Task<Store> Add(Store store)
	{
		return await unitOfWork.StoreRepository.Add(store);
	}
	
	public async Task<Store?> GetStoreById(decimal id)
	{
		return await unitOfWork.StoreRepository.GetStoreById(id);
	}
	
	public Store Delete(Store store)
	{
		return unitOfWork.StoreRepository.Delete(store);
	}
	
}
