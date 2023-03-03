using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Repositories;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;

namespace CREL_BE.Services;

public class AreaManagerService : IAreaManagerService
{
	private readonly IUnitOfWorkRepository unitOfWork;
	private readonly IMapper mapper;

	public AreaManagerService(IUnitOfWorkRepository unitOfWork, IMapper mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}

	public async Task<AreaManagerDto?> GetAreaManagerDtoById(decimal id)
	{
		return await unitOfWork.AreaManagerRepository.GetAreaManagerDtoById(id);
	}

	public Task<PagedList<AreaManagerDto>> GetAreaManagerDtos(AreaManagerFilter areaManagerFilter)
	{
		return unitOfWork.AreaManagerRepository.GetAreaManagerDtos(areaManagerFilter);
	}

	public async Task<AreaManager> Add(AreaManager areaManager)
	{
		return await unitOfWork.AreaManagerRepository.Add(areaManager);
	}

	public async Task<AreaManager?> GetAreaManagerById(decimal id)
	{
		return await unitOfWork.AreaManagerRepository.GetAreaManagerById(id);
	}

	public AreaManager Delete(AreaManager areaManager)
	{
		return unitOfWork.AreaManagerRepository.Delete(areaManager);
	}

	public async Task<AreaManager?> GetAreaManagerByUserName(string userName)
	{
		return await unitOfWork.AreaManagerRepository.GetAreaManagerByUserName(userName);
	}

    public async Task<AreaManager?> GetAreaManagerByEmail(string email)
    {
        return await unitOfWork.AreaManagerRepository.GetAreaManagerByEmail(email);
    }

    public async Task<AreaManager?> GetAreaManagerByPhoneNumber(string phoneNumber)
    {
        return await unitOfWork.AreaManagerRepository.GetAreaManagerByPhoneNumber(phoneNumber);
    }
}