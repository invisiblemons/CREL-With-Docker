using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Repositories;

public class DistrictRepository : IDistrictRepository
{
	private readonly CRELDBContext dbContext;
    private readonly IMapper mapper;
	
	public DistrictRepository(CRELDBContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<DistrictDto?> GetDistrictDtoById(decimal id)
	{
		return (await dbContext.Districts
				.Where(entity => entity.Id == id)
				.ProjectTo<DistrictDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync());
	}
	
    public Task<PagedList<DistrictDto>> GetDistrictDtos(DistrictFilter districtFilter)
	{
		return PagedList<DistrictDto>.CreateAsync(
			districtFilter.ApplyFilter(dbContext.Districts)
			.ProjectTo<DistrictDto>(mapper.ConfigurationProvider)
			.AsNoTracking()
			, districtFilter);
	}
	
	public async Task<District> Add(District district)
	{
		return (await dbContext.Districts.AddAsync(district)).Entity;
	}
	
	public async Task<District?> GetDistrictById(decimal id)
	{
		return (await dbContext.Districts.FirstOrDefaultAsync(entity => entity.Id == id));
	}
	
	public District Delete(District district)
	{
		return dbContext.Districts.Remove(district).Entity;
	}
	
}
