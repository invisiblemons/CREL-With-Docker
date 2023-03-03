using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Repositories;

public class OwnerRepository : IOwnerRepository
{
	private readonly CRELDBContext dbContext;
    private readonly IMapper mapper;
	
	public OwnerRepository(CRELDBContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<OwnerDto?> GetOwnerDtoById(decimal id)
	{
		return (await dbContext.Owners
				.Where(entity => entity.Id == id)
				.ProjectTo<OwnerDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync());
	}
	
    public Task<PagedList<OwnerDto>> GetOwnerDtos(OwnerFilter contactFilter)
	{
		return PagedList<OwnerDto>.CreateAsync(
			contactFilter.ApplyFilter(dbContext.Owners)
			.ProjectTo<OwnerDto>(mapper.ConfigurationProvider)
			.AsNoTracking()
			, contactFilter);
	}
	
	public async Task<Owner> Add(Owner contact)
	{
		return (await dbContext.Owners.AddAsync(contact)).Entity;
	}
	
	public async Task<Owner?> GetOwnerById(decimal id)
	{
		return (await dbContext.Owners.FirstOrDefaultAsync(entity => entity.Id == id));
	}
	
	public Owner Delete(Owner contact)
	{
		return dbContext.Owners.Remove(contact).Entity;
	}
	
}
