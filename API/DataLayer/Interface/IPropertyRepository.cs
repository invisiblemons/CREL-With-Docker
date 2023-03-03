using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;

namespace CREL_BE.Repositories;

public interface IPropertyRepository
{
    Task<PropertyDto?> GetPropertyDtoById(decimal id);
    Task<PagedList<PropertyDto>> GetPropertyDtos(PropertyFilter propertyFilter);
	Task<Property> Add(Property property);
	Task<Property?> GetPropertyById(decimal id);
	Property Delete(Property property);
}
