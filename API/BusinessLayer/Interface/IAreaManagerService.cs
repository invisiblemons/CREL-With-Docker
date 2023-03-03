using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;

namespace CREL_BE.Services;

public interface IAreaManagerService
{
    Task<AreaManagerDto?> GetAreaManagerDtoById(decimal id);
    Task<PagedList<AreaManagerDto>> GetAreaManagerDtos(AreaManagerFilter areaManagerFilter);
	Task<AreaManager> Add(AreaManager areaManager);
	Task<AreaManager?> GetAreaManagerById(decimal id);
	AreaManager Delete(AreaManager areaManager);
    Task<AreaManager?> GetAreaManagerByUserName(string userName);
    Task<AreaManager?> GetAreaManagerByEmail(string email);
    Task<AreaManager?> GetAreaManagerByPhoneNumber(string phoneNumber);
}
