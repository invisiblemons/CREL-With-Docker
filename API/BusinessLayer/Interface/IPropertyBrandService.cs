using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;

namespace CREL_BE.Services;

public interface IPropertyBrandService
{
	Task<PropertyBrand> Add(PropertyBrand propertyBrand);
	Task<PropertyBrand?> GetPropertyBrandById(decimal propertyId, decimal brandId);
	PropertyBrand Delete(PropertyBrand propertyBrand);
    Task<PropertyBrandDto?> GetPropertyBrandDtoById(decimal propertyId, decimal brandId);
    Task<PagedList<PropertyBrandDto>> GetPropertyBrandDtos(PropertyBrandFilter propertyBrandFilter);
}
