using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Repositories;

public class StoreRepository : IStoreRepository
{
	private readonly CRELDBContext dbContext;
    private readonly IMapper mapper;
	
	public StoreRepository(CRELDBContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<StoreDto?> GetStoreDtoById(decimal id)
	{
		return (await dbContext.Stores
				.Where(entity => entity.Id == id)
				.ProjectTo<StoreDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync());
	}
	
    public Task<PagedList<StoreDto>> GetStoreDtos(StoreFilter storeFilter)
	{
		return PagedList<StoreDto>.CreateAsync(
			storeFilter.ApplyFilter(dbContext.Stores)
			.ProjectTo<StoreDto>(mapper.ConfigurationProvider)
			.AsNoTracking()
			, storeFilter);
	}
	
	public async Task<Store> Add(Store store)
	{
		return (await dbContext.Stores.AddAsync(store)).Entity;
	}
	
	public async Task<Store?> GetStoreById(decimal id)
	{
		return (await dbContext.Stores.FirstOrDefaultAsync(entity => entity.Id == id));
	}
	
	public Store Delete(Store store)
	{
		return dbContext.Stores.Remove(store).Entity;
	}
	
}
