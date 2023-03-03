using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;

namespace CREL_BE.Services;

public interface IStoreService
{
    Task<StoreDto?> GetStoreDtoById(decimal id);
    Task<PagedList<StoreDto>> GetStoreDtos(StoreFilter storeFilter);
	Task<Store> Add(Store store);
	Task<Store?> GetStoreById(decimal id);
	Store Delete(Store store);
}
