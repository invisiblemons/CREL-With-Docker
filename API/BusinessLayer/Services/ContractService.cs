using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Repositories;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;

namespace CREL_BE.Services;

public class ContractService : IContractService
{
	private readonly IUnitOfWorkRepository unitOfWork;
    private readonly IMapper mapper;
	
	public ContractService(IUnitOfWorkRepository unitOfWork, IMapper mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}

	public async Task<ContractDto?> GetContractDtoById(decimal id)
	{
		return await unitOfWork.ContractRepository.GetContractDtoById(id);
	}
	
    public Task<PagedList<ContractDto>> GetContractDtos(ContractFilter contractFilter)
	{
		return unitOfWork.ContractRepository.GetContractDtos( contractFilter);
	}
	
	public async Task<Contract> Add(Contract contract)
	{
		return await unitOfWork.ContractRepository.Add(contract);
	}
	
	public async Task<Contract?> GetContractById(decimal id)
	{
		return await unitOfWork.ContractRepository.GetContractById(id);
	}
	
	public Contract Delete(Contract contract)
	{
		return unitOfWork.ContractRepository.Delete(contract);
	}
	
	public async Task<List<ContractDto>> GetDtosWithoutPaging(ContractFilter filter)
    {
        return await unitOfWork.ContractRepository.GetDtosWithoutPaging(filter);
    }
}
