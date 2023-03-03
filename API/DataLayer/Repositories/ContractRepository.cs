using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Repositories;

public class ContractRepository : IContractRepository
{
	private readonly CRELDBContext dbContext;
    private readonly IMapper mapper;
	
	public ContractRepository(CRELDBContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<ContractDto?> GetContractDtoById(decimal id)
	{
		return (await dbContext.Contracts
				.Where(entity => entity.Id == id)
				.ProjectTo<ContractDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync());
	}
	
    public Task<PagedList<ContractDto>> GetContractDtos(ContractFilter contractFilter)
	{
		return PagedList<ContractDto>.CreateAsync(
			contractFilter.ApplyFilter(dbContext.Contracts)
			.ProjectTo<ContractDto>(mapper.ConfigurationProvider)
			.AsNoTracking()
			, contractFilter);
	}
	
	public async Task<Contract> Add(Contract contract)
	{
		return (await dbContext.Contracts.AddAsync(contract)).Entity;
	}
	
	public async Task<Contract?> GetContractById(decimal id)
	{
		return (await dbContext.Contracts.Include(contract => contract.ContractTerms).ThenInclude(contractTerms => contractTerms.ContractTerms).FirstOrDefaultAsync(entity => entity.Id == id));
	}
	
	public Contract Delete(Contract contract)
	{
		return dbContext.Contracts.Remove(contract).Entity;
	}
	
	public async Task<List<ContractDto>> GetDtosWithoutPaging(ContractFilter filter)
    {
        return await filter.ApplyFilter(dbContext.Contracts).ProjectTo<ContractDto>(mapper.ConfigurationProvider).AsNoTracking().ToListAsync();
    }
}
