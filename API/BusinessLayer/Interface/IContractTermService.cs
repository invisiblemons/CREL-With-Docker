using CREL_BE.Dtos;
using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Helpers;

namespace CREL_BE.Services;

public interface IContractTermService
{
    Task<ContractTermDto?> GetContractTermDtoById(decimal id);
    Task<PagedList<ContractTermDto>> GetContractTermDtos(ContractTermFilter contractTermFilter);
	Task<ContractTerm> Add(ContractTerm contractTerm);
	Task<ContractTerm?> GetContractTermById(decimal id);
	ContractTerm Delete(ContractTerm contractTerm);
}
