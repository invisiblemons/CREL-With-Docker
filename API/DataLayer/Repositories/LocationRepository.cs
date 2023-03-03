using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Repositories;

public class LocationRepository : ILocationRepository
{
	private readonly CRELDBContext dbContext;
    private readonly IMapper mapper;
	
	public LocationRepository(CRELDBContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<LocationDto?> GetLocationDtoById(decimal id)
	{
		return (await dbContext.Locations
				.Where(entity => entity.Id == id)
				.ProjectTo<LocationDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync());
	}
	
    public Task<PagedList<LocationDto>> GetLocationDtos(LocationFilter locationFilter)
	{
		return PagedList<LocationDto>.CreateAsync(
			locationFilter.ApplyFilter(dbContext.Locations)
			.ProjectTo<LocationDto>(mapper.ConfigurationProvider)
			.AsNoTracking()
			, locationFilter);
	}
	
	public async Task<Location> Add(Location location)
	{
		return (await dbContext.Locations.AddAsync(location)).Entity;
	}
	
	public async Task<Location?> GetLocationById(decimal id)
	{
		return (await dbContext.Locations.FirstOrDefaultAsync(entity => entity.Id == id));
	}
	
	public Location Delete(Location location)
	{
		return dbContext.Locations.Remove(location).Entity;
	}
	
}
