using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Repositories;

public class PropertyBrandRepository : IPropertyBrandRepository
{
    private readonly CRELDBContext dbContext;
    private readonly IMapper mapper;

    public PropertyBrandRepository(CRELDBContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<PropertyBrand> Add(PropertyBrand propertyBrand)
    {
        return (await dbContext.PropertyBrands.AddAsync(propertyBrand)).Entity;
    }

    public async Task<PropertyBrand?> GetPropertyBrandById(decimal propertyId, decimal brandId)
    {
        return (await dbContext.PropertyBrands.FirstOrDefaultAsync(entity => entity.PropertyId == propertyId && entity.BrandId == brandId));
    }

    public async Task<PropertyBrandDto?> GetPropertyBrandDtoById(decimal propertyId, decimal brandId)
	{
		return (await dbContext.PropertyBrands
                .Where(entity => entity.PropertyId == propertyId && entity.BrandId == brandId)
				.ProjectTo<PropertyBrandDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync());
	}

    public PropertyBrand Delete(PropertyBrand propertyBrand)
    {
        return dbContext.PropertyBrands.Remove(propertyBrand).Entity;
    }

    public Task<PagedList<PropertyBrandDto>> GetPropertyBrandDtos(PropertyBrandFilter storeFilter)
	{
		return PagedList<PropertyBrandDto>.CreateAsync(
			storeFilter.ApplyFilter(dbContext.PropertyBrands)
			.ProjectTo<PropertyBrandDto>(mapper.ConfigurationProvider)
			.AsNoTracking()
			, storeFilter);
	}
}
