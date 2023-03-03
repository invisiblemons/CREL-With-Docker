using Coravel.Invocable;
using CREL_BE.Filter;
using CREL_BE.Helpers;
using CREL_BE.Dtos;
using CREL_BE.Services;
using CREL_BE.Entities;
using CREL_BE.ApiLayer.Helpers;

namespace CREL_BE.Invocables;
public class DailyAt7amCornJob : IInvocable
{
    private readonly IUnitOfWorkService unitOfWorkService;

    public DailyAt7amCornJob(IUnitOfWorkService unitOfWorkService)
    {
        this.unitOfWorkService = unitOfWorkService;
    }

    public async Task Invoke()
    {
        await AppointmentWaiting();
        await DailyBrandRequest();
    }

    private async Task DailyBrandRequest()
    {
        DevelopHelper.WriteLog(@$"Daily Brand Request start at {DateTime.Now}");
        var brandRequests = await unitOfWorkService.BrandRequestService.GetBrandRequestDtos(new BrandRequestFilter()
        {
            Status = 1,
            Amount = 1
        });

        DevelopHelper.WriteLog(
            @$"Daily Brand Request
            DateTime.Now = {DateTime.Now}
            BrandRequests = {Newtonsoft.Json.JsonConvert.SerializeObject(brandRequests)}
            "
        );

        foreach (var brandRequest in brandRequests)
        {
            EmailHelper.SendEmailBrandRequest(brandRequest);
        }

    }

    private async Task AppointmentWaiting()
    {
        DevelopHelper.WriteLog(@$"Appointment Waiting 7am start at {DateTime.Now}");
        List<BrokerAppoinmentCount> brokers = await unitOfWorkService.BrokerService.GetBrokerAppoinmentCountWithoutPaging(new BrokerFilter()
        {
            Status = MyConstant.Broker.Status.HaveTeam
        });

        DevelopHelper.WriteLog(
            @$"Count Appointment Waiting 7am
            DateTime.Now = {DateTime.Now}
            Appointment Count = {Newtonsoft.Json.JsonConvert.SerializeObject(brokers)}
            "
        );

        foreach (BrokerAppoinmentCount broker in brokers)
        {
            if (broker.AppoinmentCount > 0)
            {
                await NotificationHelper.SendNotifications(
                new NotificationDto
                {
                    BrokerId = broker.Id,
                    AppointmentCount = broker.AppoinmentCount,
                    SendDateTime = DateTime.Now,
                    Type = MyConstant.Notification.Type.AppointmentWaiting,
                }
            );
            }
        }

    }
}
