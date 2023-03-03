using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Repositories;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;

namespace CREL_BE.Services;

public class BrandService : IBrandService
{
	private readonly IUnitOfWorkRepository unitOfWork;
    private readonly IMapper mapper;
	
	public BrandService(IUnitOfWorkRepository unitOfWork, IMapper mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}

	public async Task<BrandDto?> GetBrandDtoById(decimal id)
	{
		return await unitOfWork.BrandRepository.GetBrandDtoById(id);
	}
	
    public Task<PagedList<BrandDto>> GetBrandDtos(BrandFilter brandFilter)
	{
		return unitOfWork.BrandRepository.GetBrandDtos(brandFilter);
	}
	
	public async Task<Brand> Add(Brand brand)
	{
		return await unitOfWork.BrandRepository.Add(brand);
	}
	
	public async Task<Brand?> GetBrandById(decimal id)
	{
		return await unitOfWork.BrandRepository.GetBrandById(id);
	}
	
	public Brand Delete(Brand brand)
	{
		return unitOfWork.BrandRepository.Delete(brand);
	}

	public async Task<Brand?> GetBrandByUserName(string userName)
	{
        return await unitOfWork.BrandRepository.GetBrandByUserName(userName);
    }

    public async Task<Brand?> GetBrandByFirebaseId(string firebaseUid)
    {
        return await unitOfWork.BrandRepository.GetBrandByFirebaseId( firebaseUid);
    }

	public async Task<Brand?> GetBrandByEmail(string email)
	{
        return await unitOfWork.BrandRepository.GetBrandByEmail( email);
    }

	public async Task<Brand?> GetBrandByPhoneNumber(string phoneNumber)
	{
        return await unitOfWork.BrandRepository.GetBrandByPhoneNumber( phoneNumber);
    }
}
