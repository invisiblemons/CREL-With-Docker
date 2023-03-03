using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;

namespace CREL_BE.Services;

public interface IBrandRequestService
{
    Task<BrandRequestDto?> GetBrandRequestDtoById(decimal id);
    Task<PagedList<BrandRequestDto>> GetBrandRequestDtos(BrandRequestFilter brandRequestFilter);
	Task<BrandRequest> Add(BrandRequest brandRequest);
	Task<BrandRequest?> GetBrandRequestById(decimal id);
	BrandRequest Delete(BrandRequest brandRequest);
}
