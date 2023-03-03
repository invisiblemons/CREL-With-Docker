using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;

namespace CREL_BE.Repositories;

public interface IOwnerRepository
{
    Task<OwnerDto?> GetOwnerDtoById(decimal id);
    Task<PagedList<OwnerDto>> GetOwnerDtos(OwnerFilter contactFilter);
	Task<Owner> Add(Owner contact);
	Task<Owner?> GetOwnerById(decimal id);
	Owner Delete(Owner contact);
}
