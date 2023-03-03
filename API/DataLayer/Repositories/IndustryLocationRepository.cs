using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Repositories;

public class IndustryLocationRepository : IIndustryLocationRepository
{
	private readonly CRELDBContext dbContext;
    private readonly IMapper mapper;
	
	public IndustryLocationRepository(CRELDBContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}
	
	public async Task<IndustryLocation> Add(IndustryLocation industryProperty)
	{
		return (await dbContext.IndustryLocations.AddAsync(industryProperty)).Entity;
	}
	
	public async Task<IndustryLocation?> GetIndustryLocationByLocationIdIndustryId(decimal locationId,decimal industryId)
	{
		return (await dbContext.IndustryLocations.FirstOrDefaultAsync(entity => entity.LocationId == locationId && entity.IndustryId == industryId));
	}
	
	public IndustryLocation Delete(IndustryLocation industryProperty)
	{
		return dbContext.IndustryLocations.Remove(industryProperty).Entity;
	}
	
}
