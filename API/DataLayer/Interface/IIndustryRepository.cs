using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;

namespace CREL_BE.Repositories;

public interface IIndustryRepository
{
    Task<IndustryDto?> GetIndustryDtoById(decimal id);
    Task<PagedList<IndustryDto>> GetIndustryDtos(IndustryFilter industryFilter);
	Task<Industry> Add(Industry industry);
	Task<Industry?> GetIndustryById(decimal id);
	Industry Delete(Industry industry);
}
