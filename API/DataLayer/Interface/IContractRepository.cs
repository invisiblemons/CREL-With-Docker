using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;

namespace CREL_BE.Repositories;

public interface IContractRepository
{
    Task<ContractDto?> GetContractDtoById(decimal id);
    Task<PagedList<ContractDto>> GetContractDtos(ContractFilter contractFilter);
	Task<Contract> Add(Contract contract);
	Task<Contract?> GetContractById(decimal id);
	Contract Delete(Contract contract);
	Task<List<ContractDto>> GetDtosWithoutPaging(ContractFilter filter);
}
