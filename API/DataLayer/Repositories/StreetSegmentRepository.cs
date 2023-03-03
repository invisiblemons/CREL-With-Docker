using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Repositories;

public class StreetSegmentRepository : IStreetSegmentRepository
{
	private readonly CRELDBContext dbContext;
    private readonly IMapper mapper;
	
	public StreetSegmentRepository(CRELDBContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<StreetSegmentDto?> GetStreetSegmentDtoById(decimal id)
	{
		return (await dbContext.StreetSegments
				.Where(entity => entity.Id == id)
				.ProjectTo<StreetSegmentDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync());
	}
	
    public Task<PagedList<StreetSegmentDto>> GetStreetSegmentDtos(StreetSegmentFilter streetSegmentFilter)
	{
		return PagedList<StreetSegmentDto>.CreateAsync(
			streetSegmentFilter.ApplyFilter(dbContext.StreetSegments)
			.ProjectTo<StreetSegmentDto>(mapper.ConfigurationProvider)
			.AsNoTracking()
			, streetSegmentFilter);
	}
	
	public async Task<StreetSegment> Add(StreetSegment streetSegment)
	{
		return (await dbContext.StreetSegments.AddAsync(streetSegment)).Entity;
	}
	
	public async Task<StreetSegment?> GetStreetSegmentById(decimal id)
	{
		return (await dbContext.StreetSegments.FirstOrDefaultAsync(entity => entity.Id == id));
	}
	
	public StreetSegment Delete(StreetSegment streetSegment)
	{
		return dbContext.StreetSegments.Remove(streetSegment).Entity;
	}
	
}
