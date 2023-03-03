using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;

namespace CREL_BE.Services;

public interface IStreetSegmentService
{
    Task<StreetSegmentDto?> GetStreetSegmentDtoById(decimal id);
    Task<PagedList<StreetSegmentDto>> GetStreetSegmentDtos(StreetSegmentFilter streetSegmentFilter);
	Task<StreetSegment> Add(StreetSegment streetSegment);
	Task<StreetSegment?> GetStreetSegmentById(decimal id);
	StreetSegment Delete(StreetSegment streetSegment);
}
