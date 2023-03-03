using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Repositories;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;

namespace CREL_BE.Services;

public class LocationService : ILocationService
{
	private readonly IUnitOfWorkRepository unitOfWork;
    private readonly IMapper mapper;
	
	public LocationService(IUnitOfWorkRepository unitOfWork, IMapper mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}

	public async Task<LocationDto?> GetLocationDtoById(decimal id)
	{
		return await unitOfWork.LocationRepository.GetLocationDtoById(id);
	}
	
    public Task<PagedList<LocationDto>> GetLocationDtos(LocationFilter locationFilter)
	{
		return unitOfWork.LocationRepository.GetLocationDtos(locationFilter);
	}
	
	public async Task<Location> Add(Location location)
	{
		return await unitOfWork.LocationRepository.Add(location);
	}
	
	public async Task<Location?> GetLocationById(decimal id)
	{
		return await unitOfWork.LocationRepository.GetLocationById(id);
	}
	
	public Location Delete(Location location)
	{
		return unitOfWork.LocationRepository.Delete(location);
	}
	
}
