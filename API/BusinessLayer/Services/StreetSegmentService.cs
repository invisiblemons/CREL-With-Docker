using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Repositories;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;

namespace CREL_BE.Services;

public class StreetSegmentService : IStreetSegmentService
{
    private readonly IUnitOfWorkRepository unitOfWork;
    private readonly IMapper mapper;

    public StreetSegmentService(IUnitOfWorkRepository unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<StreetSegmentDto?> GetStreetSegmentDtoById(decimal id)
    {
        return await unitOfWork.StreetSegmentRepository.GetStreetSegmentDtoById(id);
    }

    public Task<PagedList<StreetSegmentDto>> GetStreetSegmentDtos(StreetSegmentFilter streetSegmentFilter)
    {
        return unitOfWork.StreetSegmentRepository.GetStreetSegmentDtos(streetSegmentFilter);
    }

    public async Task<StreetSegment> Add(StreetSegment streetSegment)
    {
        return await unitOfWork.StreetSegmentRepository.Add(streetSegment);
    }

    public async Task<StreetSegment?> GetStreetSegmentById(decimal id)
    {
        return await unitOfWork.StreetSegmentRepository.GetStreetSegmentById(id);
    }

    public StreetSegment Delete(StreetSegment streetSegment)
    {
        return unitOfWork.StreetSegmentRepository.Delete(streetSegment);
    }

}
