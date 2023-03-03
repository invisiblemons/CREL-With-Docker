using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Repositories;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;

namespace CREL_BE.Services;

public class BrokerService : IBrokerService
{
    private readonly IUnitOfWorkRepository unitOfWork;
    private readonly IMapper mapper;

    public BrokerService(IUnitOfWorkRepository unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<BrokerDto?> GetBrokerDtoById(decimal id)
    {
        return await unitOfWork.BrokerRepository.GetBrokerDtoById(id);
    }

    public Task<PagedList<BrokerDto>> GetBrokerDtos(BrokerFilter brokerFilter)
    {
        return unitOfWork.BrokerRepository.GetBrokerDtos(brokerFilter);
    }

    public async Task<Broker> Add(Broker broker)
    {
        return await unitOfWork.BrokerRepository.Add(broker);
    }

    public async Task<Broker?> GetBrokerById(decimal id)
    {
        return await unitOfWork.BrokerRepository.GetBrokerById(id);
    }

    public Broker Delete(Broker broker)
    {
        return unitOfWork.BrokerRepository.Delete(broker);
    }

    public async Task<Broker?> GetBrokerByUserName(string userName)
    {
        return await unitOfWork.BrokerRepository.GetBrokerByUserName(userName);
    }

    public async Task<Broker?> GetBrokerByEmail(string email)
    {
        return await unitOfWork.BrokerRepository.GetBrokerByEmail(email);
    }

    public async Task<Broker?> GetBrokerByPhoneNumber(string phoneNumber)
    {
        return await unitOfWork.BrokerRepository.GetBrokerByPhoneNumber(phoneNumber);
    }

    public Task<List<BrokerAppoinmentCount>> GetBrokerAppoinmentCountWithoutPaging(BrokerFilter brokerFilter)
    {
        return unitOfWork.BrokerRepository.GetBrokerAppoinmentCountWithoutPaging(brokerFilter);
    }
}
