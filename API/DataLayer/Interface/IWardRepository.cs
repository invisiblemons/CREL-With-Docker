using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;

namespace CREL_BE.Repositories;

public interface IWardRepository
{
    Task<WardDto?> GetWardDtoById(decimal id);
    Task<PagedList<WardDto>> GetWardDtos(WardFilter wardFilter);
	Task<Ward> Add(Ward ward);
	Task<Ward?> GetWardById(decimal id);
	Ward Delete(Ward ward);
}
