using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Repositories;

public class IndustryRepository : IIndustryRepository
{
	private readonly CRELDBContext dbContext;
    private readonly IMapper mapper;
	
	public IndustryRepository(CRELDBContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<IndustryDto?> GetIndustryDtoById(decimal id)
	{
		return (await dbContext.Industries
				.Where(entity => entity.Id == id)
				.ProjectTo<IndustryDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync());
	}
	
    public Task<PagedList<IndustryDto>> GetIndustryDtos(IndustryFilter industryFilter)
	{
		return PagedList<IndustryDto>.CreateAsync(
			industryFilter.ApplyFilter(dbContext.Industries)
			.ProjectTo<IndustryDto>(mapper.ConfigurationProvider)
			.AsNoTracking()
			, industryFilter);
	}
	
	public async Task<Industry> Add(Industry industry)
	{
		return (await dbContext.Industries.AddAsync(industry)).Entity;
	}
	
	public async Task<Industry?> GetIndustryById(decimal id)
	{
		return (await dbContext.Industries.FirstOrDefaultAsync(entity => entity.Id == id));
	}
	
	public Industry Delete(Industry industry)
	{
		return dbContext.Industries.Remove(industry).Entity;
	}
	
}
