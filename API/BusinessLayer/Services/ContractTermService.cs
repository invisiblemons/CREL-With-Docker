using CREL_BE.Entities;
using CREL_BE.Helpers;
using AutoMapper;
using CREL_BE.Dtos;
using CREL_BE.Filter;
using CREL_BE.Repositories;

namespace CREL_BE.Services;

public class ContractTermService : IContractTermService
{
	private readonly IUnitOfWorkRepository unitOfWork;
    private readonly IMapper mapper;
	
	public ContractTermService(IUnitOfWorkRepository unitOfWork, IMapper mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}

	public async Task<ContractTermDto?> GetContractTermDtoById(decimal id)
	{
		return await unitOfWork.ContractTermRepository.GetContractTermDtoById(id);
	}
	
    public Task<PagedList<ContractTermDto>> GetContractTermDtos(ContractTermFilter contractTermFilter)
	{
		return unitOfWork.ContractTermRepository.GetContractTermDtos(contractTermFilter);
	}
	
	public async Task<ContractTerm> Add(ContractTerm contractTerm)
	{
		return await unitOfWork.ContractTermRepository.Add(contractTerm);
	}
	
	public async Task<ContractTerm?> GetContractTermById(decimal id)
	{
		return await unitOfWork.ContractTermRepository.GetContractTermById(id);
	}
	
	public ContractTerm Delete(ContractTerm contractTerm)
	{
		return unitOfWork.ContractTermRepository.Delete(contractTerm);
	}
	
}
