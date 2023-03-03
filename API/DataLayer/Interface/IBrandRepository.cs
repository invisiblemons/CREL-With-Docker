using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;

namespace CREL_BE.Repositories;

public interface IBrandRepository
{
    Task<BrandDto?> GetBrandDtoById(decimal id);
    Task<PagedList<BrandDto>> GetBrandDtos(BrandFilter brandFilter);
    Task<Brand> Add(Brand brand);
    Task<Brand?> GetBrandById(decimal id);
    Brand Delete(Brand brand);
    Task<Brand?> GetBrandByUserName(string userName);
    Task<Brand?> GetBrandByFirebaseId(string firebaseUid);
    Task<Brand?> GetBrandByEmail(string email);
    Task<Brand?> GetBrandByPhoneNumber(string phoneNumber);
}
