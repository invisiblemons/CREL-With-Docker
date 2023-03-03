using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Repositories;

public class BrokerRepository : IBrokerRepository
{
	private readonly CRELDBContext dbContext;
    private readonly IMapper mapper;
	
	public BrokerRepository(CRELDBContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<BrokerDto?> GetBrokerDtoById(decimal id)
	{
		return (await dbContext.Brokers
				.Where(entity => entity.Id == id)
				.ProjectTo<BrokerDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync());
	}
	
    public Task<PagedList<BrokerDto>> GetBrokerDtos(BrokerFilter brokerFilter)
	{
		return PagedList<BrokerDto>.CreateAsync(
			brokerFilter.ApplyFilter(dbContext.Brokers)
			.ProjectTo<BrokerDto>(mapper.ConfigurationProvider)
			.AsNoTracking()
			, brokerFilter);
	}
	
	public async Task<Broker> Add(Broker broker)
	{
		return (await dbContext.Brokers.AddAsync(broker)).Entity;
	}
	
	public async Task<Broker?> GetBrokerById(decimal id)
	{
		return (await dbContext.Brokers.FirstOrDefaultAsync(entity => entity.Id == id));
	}
	
	public Broker Delete(Broker broker)
	{
		return dbContext.Brokers.Remove(broker).Entity;
	}

    public async Task<Broker?> GetBrokerByUserName(string userName)
    {
        return (await dbContext.Brokers.FirstOrDefaultAsync(entity => entity.UserName == userName));
    }

	public async Task<Broker?> GetBrokerByEmail(string email)
    {
        return (await dbContext.Brokers.FirstOrDefaultAsync(entity => entity.Email == email));
    }

    public async Task<Broker?> GetBrokerByPhoneNumber(string phoneNumber)
    {
        return (await dbContext.Brokers.FirstOrDefaultAsync(entity => entity.PhoneNumber == phoneNumber));
    }

    public async Task<List<BrokerAppoinmentCount>> GetBrokerAppoinmentCountWithoutPaging(BrokerFilter brokerFilter)
    {
	return await brokerFilter.ApplyFilter(dbContext.Brokers)
			.ProjectTo<BrokerAppoinmentCount>(mapper.ConfigurationProvider)
			.AsNoTracking().ToListAsync();
    }
}
