using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Repositories;

public class AreaManagerRepository : IAreaManagerRepository
{
	private readonly CRELDBContext dbContext;
	private readonly IMapper mapper;

	public AreaManagerRepository(CRELDBContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<AreaManagerDto?> GetAreaManagerDtoById(decimal id)
	{
		return (await dbContext.AreaManagers
				.Where(entity => entity.Id == id)
				.ProjectTo<AreaManagerDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync());
	}

	public Task<PagedList<AreaManagerDto>> GetAreaManagerDtos(AreaManagerFilter areaManagerFilter)
	{
		return PagedList<AreaManagerDto>.CreateAsync(
			areaManagerFilter.ApplyFilter(dbContext.AreaManagers)
			.ProjectTo<AreaManagerDto>(mapper.ConfigurationProvider)
			.AsNoTracking()
			, areaManagerFilter);
	}

	public async Task<AreaManager> Add(AreaManager areaManager)
	{
		return (await dbContext.AreaManagers.AddAsync(areaManager)).Entity;
	}

	public async Task<AreaManager?> GetAreaManagerById(decimal id)
	{
		return (await dbContext.AreaManagers.FirstOrDefaultAsync(entity => entity.Id == id));
	}

	public AreaManager Delete(AreaManager areaManager)
	{
		return dbContext.AreaManagers.Remove(areaManager).Entity;
	}

	public async Task<AreaManager?> GetAreaManagerByUserName(string userName)
	{
		return (await dbContext.AreaManagers.FirstOrDefaultAsync(entity => entity.UserName == userName));
	}

    public async Task<AreaManager?> GetAreaManagerByEmail(string email)
    {
        return (await dbContext.AreaManagers.FirstOrDefaultAsync(entity => entity.Email == email));
    }

    public async Task<AreaManager?> GetAreaManagerByPhoneNumber(string phoneNumber)
    {
        return (await dbContext.AreaManagers.FirstOrDefaultAsync(entity => entity.PhoneNumber == phoneNumber));
    }
}