using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Repositories;

public class PropertyRepository : IPropertyRepository
{
	private readonly CRELDBContext dbContext;
    private readonly IMapper mapper;
	
	public PropertyRepository(CRELDBContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<PropertyDto?> GetPropertyDtoById(decimal id)
	{
		return (await dbContext.Properties
				.Where(entity => entity.Id == id)
				.ProjectTo<PropertyDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync());
	}
	
    public Task<PagedList<PropertyDto>> GetPropertyDtos(PropertyFilter propertyFilter)
	{
		return PagedList<PropertyDto>.CreateAsync(
			propertyFilter.ApplyFilter(dbContext.Properties)
			.ProjectTo<PropertyDto>(mapper.ConfigurationProvider)
			.AsNoTracking()
			, propertyFilter);
	}
	
	public async Task<Property> Add(Property property)
	{
		return (await dbContext.Properties.AddAsync(property)).Entity;
	}
	
	public async Task<Property?> GetPropertyById(decimal id)
	{
		return (await dbContext.Properties.Include(p => p.Owners).FirstOrDefaultAsync(entity => entity.Id == id));
	}
	
	public Property Delete(Property property)
	{
		return dbContext.Properties.Remove(property).Entity;
	}
	
}
