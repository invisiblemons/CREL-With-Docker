using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Repositories;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;

namespace CREL_BE.Services;

public class PropertyService : IPropertyService
{
	private readonly IUnitOfWorkRepository unitOfWork;
    private readonly IMapper mapper;
	
	public PropertyService(IUnitOfWorkRepository unitOfWork, IMapper mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}

	public async Task<PropertyDto?> GetPropertyDtoById(decimal id)
	{
		return await unitOfWork.PropertyRepository.GetPropertyDtoById(id);
	}
	
    public Task<PagedList<PropertyDto>> GetPropertyDtos(PropertyFilter propertyFilter)
	{
		return unitOfWork.PropertyRepository.GetPropertyDtos(propertyFilter);
	}
	
	public async Task<Property> Add(Property property)
	{
		return await unitOfWork.PropertyRepository.Add(property);
	}
	
	public async Task<Property?> GetPropertyById(decimal id)
	{
		return await unitOfWork.PropertyRepository.GetPropertyById(id);
	}
	
	public Property Delete(Property property)
	{
		return unitOfWork.PropertyRepository.Delete(property);
	}
	
}
