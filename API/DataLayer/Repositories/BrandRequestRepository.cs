using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Repositories;

public class BrandRequestRepository : IBrandRequestRepository
{
	private readonly CRELDBContext dbContext;
    private readonly IMapper mapper;
	
	public BrandRequestRepository(CRELDBContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<BrandRequestDto?> GetBrandRequestDtoById(decimal id)
	{
		return (await dbContext.BrandRequests
				.Where(entity => entity.Id == id)
				.ProjectTo<BrandRequestDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync());
	}
	
    public Task<PagedList<BrandRequestDto>> GetBrandRequestDtos(BrandRequestFilter brandRequestFilter)
	{
		return PagedList<BrandRequestDto>.CreateAsync(
			brandRequestFilter.ApplyFilter(dbContext.BrandRequests)
			.ProjectTo<BrandRequestDto>(mapper.ConfigurationProvider)
			.AsNoTracking()
			, brandRequestFilter);
	}
	
	public async Task<BrandRequest> Add(BrandRequest brandRequest)
	{
		return (await dbContext.BrandRequests.AddAsync(brandRequest)).Entity;
	}
	
	public async Task<BrandRequest?> GetBrandRequestById(decimal id)
	{
		return (await dbContext.BrandRequests.Include(br => br.Properties).FirstOrDefaultAsync(entity => entity.Id == id));
	}
	
	public BrandRequest Delete(BrandRequest brandRequest)
	{
		return dbContext.BrandRequests.Remove(brandRequest).Entity;
	}
	
}
