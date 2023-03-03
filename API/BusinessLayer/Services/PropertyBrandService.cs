using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Repositories;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;

namespace CREL_BE.Services;

public class PropertyBrandService : IPropertyBrandService
{
    private readonly IUnitOfWorkRepository unitOfWork;
    private readonly IMapper mapper;

    public PropertyBrandService(IUnitOfWorkRepository unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<PropertyBrand> Add(PropertyBrand propertyBrand)
    {
        return await unitOfWork.PropertyBrandRepository.Add(propertyBrand);
    }

    public async Task<PropertyBrand?> GetPropertyBrandById(decimal propertyId, decimal brandId)
    {
        return await unitOfWork.PropertyBrandRepository.GetPropertyBrandById(propertyId, brandId);
    }

    public async Task<PropertyBrandDto?> GetPropertyBrandDtoById(decimal propertyId, decimal brandId)
    {
        return await unitOfWork.PropertyBrandRepository.GetPropertyBrandDtoById(propertyId, brandId);
    }

    public PropertyBrand Delete(PropertyBrand propertyBrand)
    {
        return unitOfWork.PropertyBrandRepository.Delete(propertyBrand);
    }

    public Task<PagedList<PropertyBrandDto>> GetPropertyBrandDtos(PropertyBrandFilter storeFilter)
    {
        return unitOfWork.PropertyBrandRepository.GetPropertyBrandDtos(storeFilter);
    }
}
