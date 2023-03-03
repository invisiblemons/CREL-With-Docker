using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Repositories;

public class BrandRepository : IBrandRepository
{
	private readonly CRELDBContext dbContext;
    private readonly IMapper mapper;
	
	public BrandRepository(CRELDBContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<BrandDto?> GetBrandDtoById(decimal id)
	{
		return (await dbContext.Brands
				.Where(entity => entity.Id == id)
				.ProjectTo<BrandDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync());
	}
	
    public Task<PagedList<BrandDto>> GetBrandDtos(BrandFilter brandFilter)
	{
		return PagedList<BrandDto>.CreateAsync(
			brandFilter.ApplyFilter(dbContext.Brands)
			.ProjectTo<BrandDto>(mapper.ConfigurationProvider)
			.AsNoTracking()
			, brandFilter);
	}
	
	public async Task<Brand> Add(Brand brand)
	{
		return (await dbContext.Brands.AddAsync(brand)).Entity;
	}
	
	public async Task<Brand?> GetBrandById(decimal id)
	{
		return (await dbContext.Brands.FirstOrDefaultAsync(entity => entity.Id == id));
	}
	
	public Brand Delete(Brand brand)
	{
		return dbContext.Brands.Remove(brand).Entity;
	}

	public async Task<Brand?> GetBrandByUserName(string userName)
	{
        return (await dbContext.Brands.FirstOrDefaultAsync(entity => entity.UserName == userName));
    }

    public async Task<Brand?> GetBrandByFirebaseId(string firebaseUid)
    {
        return (await dbContext.Brands.FirstOrDefaultAsync(entity => entity.FirebaseUid == firebaseUid));
    }

	public async Task<Brand?> GetBrandByEmail(string email)
	{
        return (await dbContext.Brands.FirstOrDefaultAsync(entity => entity.Email == email));
    }

	public async Task<Brand?> GetBrandByPhoneNumber(string phoneNumber)
	{
        return (await dbContext.Brands.FirstOrDefaultAsync(entity => entity.PhoneNumber == phoneNumber));
    }
}
