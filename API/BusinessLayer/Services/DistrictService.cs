using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Repositories;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;

namespace CREL_BE.Services;

public class DistrictService : IDistrictService
{
    private readonly IUnitOfWorkRepository unitOfWork;
    private readonly IMapper mapper;

    public DistrictService(IUnitOfWorkRepository unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<DistrictDto?> GetDistrictDtoById(decimal id)
    {
        return await unitOfWork.DistrictRepository.GetDistrictDtoById(id);
    }

    public Task<PagedList<DistrictDto>> GetDistrictDtos(DistrictFilter districtFilter)
    {
        return unitOfWork.DistrictRepository.GetDistrictDtos(districtFilter);
    }

    public async Task<District> Add(District district)
    {
        return await unitOfWork.DistrictRepository.Add(district);
    }

    public async Task<District?> GetDistrictById(decimal id)
    {
        return await unitOfWork.DistrictRepository.GetDistrictById(id);
    }

    public District Delete(District district)
    {
        return unitOfWork.DistrictRepository.Delete(district);
    }

}
