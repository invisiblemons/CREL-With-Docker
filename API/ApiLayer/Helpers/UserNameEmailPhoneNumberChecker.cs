using CREL_BE.Entities;
using CREL_BE.Services;

namespace CREL_BE.ApiLayer.Helpers;

public static class UserNameEmailPhoneNumberChecker
{
    public static async Task<bool> CheckUserName(string? userName, IUnitOfWorkService unitOfWorkService)
    {
        if (userName != null)
        {
            var brokerByUserName = await unitOfWorkService.BrokerService.GetBrokerByUserName(userName);
            if (brokerByUserName != default(Broker))
            {
                return true;
            }
            var brandByUserName = await unitOfWorkService.BrandService.GetBrandByUserName(userName);
            if (brandByUserName != default(Brand))
            {
                return true;
            }
            var areaManagerByUserName = await unitOfWorkService.AreaManagerService.GetAreaManagerByUserName(userName);
            if (areaManagerByUserName != default(AreaManager))
            {
                return true;
            }
        }
        return false;
    }

    public static async Task<bool> CheckEmail( string? email, IUnitOfWorkService unitOfWorkService)
    {
        if (email != null)
        {
            var brokerByEmail = await unitOfWorkService.BrokerService.GetBrokerByEmail(email);
            if (brokerByEmail != default(Broker))
            {
                return true;
            }
            var brandByEmail = await unitOfWorkService.BrandService.GetBrandByEmail(email);
            if (brandByEmail != default(Brand))
            {
                return true;
            }
            var areaManagerByEmail = await unitOfWorkService.AreaManagerService.GetAreaManagerByEmail(email);
            if (areaManagerByEmail != default(AreaManager))
            {
                return true;
            }
        }
        return false;
    }

    public static async Task<bool> CheckPhoneNumber(string? phoneNumber, IUnitOfWorkService unitOfWorkService)
    {
        if (phoneNumber != null)
        {
            var brokerByPhoneNumber = await unitOfWorkService.BrokerService.GetBrokerByPhoneNumber(phoneNumber);
            if (brokerByPhoneNumber != default(Broker))
            {
                return true;
            }
            var brandByPhoneNumber = await unitOfWorkService.BrandService.GetBrandByPhoneNumber(phoneNumber);
            if (brandByPhoneNumber != default(Brand))
            {
                return true;
            }
            var areaManagerByPhoneNumber = await unitOfWorkService.AreaManagerService.GetAreaManagerByPhoneNumber(phoneNumber);
            if (areaManagerByPhoneNumber != default(AreaManager))
            {
                return true;
            }
        }
        return false;
    }

}
