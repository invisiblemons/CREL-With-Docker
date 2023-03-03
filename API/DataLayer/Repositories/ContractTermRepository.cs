using CREL_BE.Entities;
using CREL_BE.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using CREL_BE.Dtos;
using CREL_BE.Filter;

namespace CREL_BE.Repositories;

public class ContractTermRepository : IContractTermRepository
{
	private readonly CRELDBContext dbContext;
    private readonly IMapper mapper;
	
	public ContractTermRepository(CRELDBContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<ContractTermDto?> GetContractTermDtoById(decimal id)
	{
		return (await dbContext.ContractTerms
				.Where(entity => entity.Id == id)
				.ProjectTo<ContractTermDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync());
	}
	
    public Task<PagedList<ContractTermDto>> GetContractTermDtos(ContractTermFilter contractTermFilter)
	{
		return PagedList<ContractTermDto>.CreateAsync(
			contractTermFilter.ApplyFilter(dbContext.ContractTerms)
			.ProjectTo<ContractTermDto>(mapper.ConfigurationProvider)
			.AsNoTracking()
			, contractTermFilter);
	}
	
	public async Task<ContractTerm> Add(ContractTerm contractTerm)
	{
		return (await dbContext.ContractTerms.AddAsync(contractTerm)).Entity;
	}
	
	public async Task<ContractTerm?> GetContractTermById(decimal id)
	{
		return (await dbContext.ContractTerms.FirstOrDefaultAsync(entity => entity.Id == id));
	}
	
	public ContractTerm Delete(ContractTerm contractTerm)
	{
		return dbContext.ContractTerms.Remove(contractTerm).Entity;
	}
	
}
