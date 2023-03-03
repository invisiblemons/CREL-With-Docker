using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;

namespace CREL_BE.Services;

public interface IBrokerService
{
    Task<BrokerDto?> GetBrokerDtoById(decimal id);
    Task<PagedList<BrokerDto>> GetBrokerDtos(BrokerFilter brokerFilter);
	Task<Broker> Add(Broker broker);
	Task<Broker?> GetBrokerById(decimal id);
	Broker Delete(Broker broker);
    Task<Broker?> GetBrokerByUserName(string userName);
    Task<Broker?> GetBrokerByEmail(string Email);
    Task<Broker?> GetBrokerByPhoneNumber(string phoneNumber);
    Task<List<BrokerAppoinmentCount>> GetBrokerAppoinmentCountWithoutPaging(BrokerFilter brokerFilter);
}
