using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;

namespace CREL_BE.Repositories;

public interface IDistrictRepository
{
    Task<DistrictDto?> GetDistrictDtoById(decimal id);
    Task<PagedList<DistrictDto>> GetDistrictDtos(DistrictFilter districtFilter);
	Task<District> Add(District district);
	Task<District?> GetDistrictById(decimal id);
	District Delete(District district);
}
