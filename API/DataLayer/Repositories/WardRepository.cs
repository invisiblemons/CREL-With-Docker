using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Repositories;

public class WardRepository : IWardRepository
{
	private readonly CRELDBContext dbContext;
    private readonly IMapper mapper;
	
	public WardRepository(CRELDBContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<WardDto?> GetWardDtoById(decimal id)
	{
		return (await dbContext.Wards
				.Where(entity => entity.Id == id)
				.ProjectTo<WardDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync());
	}
	
    public Task<PagedList<WardDto>> GetWardDtos(WardFilter wardFilter)
	{
		return PagedList<WardDto>.CreateAsync(
			wardFilter.ApplyFilter(dbContext.Wards)
			.ProjectTo<WardDto>(mapper.ConfigurationProvider)
			.AsNoTracking()
			, wardFilter);
	}
	
	public async Task<Ward> Add(Ward ward)
	{
		return (await dbContext.Wards.AddAsync(ward)).Entity;
	}
	
	public async Task<Ward?> GetWardById(decimal id)
	{
		return (await dbContext.Wards.FirstOrDefaultAsync(entity => entity.Id == id));
	}
	
	public Ward Delete(Ward ward)
	{
		return dbContext.Wards.Remove(ward).Entity;
	}
	
}
