using Coravel.Invocable;
using CREL_BE.Filter;
using CREL_BE.Helpers;
using CREL_BE.Dtos;
using CREL_BE.Services;
using CREL_BE.Entities;
using CREL_BE.ApiLayer.Helpers;

namespace CREL_BE.Invocables;
public class DailyAt7pmCornJob : IInvocable
{
    private readonly IUnitOfWorkService unitOfWorkService;

    public DailyAt7pmCornJob(IUnitOfWorkService unitOfWorkService)
    {
        this.unitOfWorkService = unitOfWorkService;
    }

    public async Task Invoke()
    {
        await AppointmentWaiting();
    }

    private async Task AppointmentWaiting()
    {
        DevelopHelper.WriteLog(@$"Appointment Waiting 7pm start at {DateTime.Now}");

        List<BrokerAppoinmentCount> brokers = await unitOfWorkService.BrokerService.GetBrokerAppoinmentCountWithoutPaging(new BrokerFilter()
        {
            Status = MyConstant.Broker.Status.HaveTeam
        });

        DevelopHelper.WriteLog(
            @$"Count Appointment Waiting 7pm
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
