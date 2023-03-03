using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;

namespace CREL_BE.Repositories;

public interface ILocationRepository
{
    Task<LocationDto?> GetLocationDtoById(decimal id);
    Task<PagedList<LocationDto>> GetLocationDtos(LocationFilter locationFilter);
	Task<Location> Add(Location location);
	Task<Location?> GetLocationById(decimal id);
	Location Delete(Location location);
}
