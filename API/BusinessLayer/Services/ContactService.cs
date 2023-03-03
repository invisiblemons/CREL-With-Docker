using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Repositories;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;

namespace CREL_BE.Services;

public class OwnerService : IOwnerService
{
    private readonly IUnitOfWorkRepository unitOfWork;
    private readonly IMapper mapper;

    public OwnerService(IUnitOfWorkRepository unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<OwnerDto?> GetOwnerDtoById(decimal id)
    {
        return await unitOfWork.OwnerRepository.GetOwnerDtoById(id);
    }

    public Task<PagedList<OwnerDto>> GetOwnerDtos(OwnerFilter ownerFilter)
    {
        return unitOfWork.OwnerRepository.GetOwnerDtos(ownerFilter);
    }

    public async Task<Owner> Add(Owner owner)
    {
        return await unitOfWork.OwnerRepository.Add(owner);
    }

    public async Task<Owner?> GetOwnerById(decimal id)
    {
        return await unitOfWork.OwnerRepository.GetOwnerById(id);
    }

    public Owner Delete(Owner owner)
    {
        return unitOfWork.OwnerRepository.Delete(owner);
    }

}
