using Coravel.Invocable;
using CREL_BE.Filter;
using CREL_BE.Helpers;
using CREL_BE.Dtos;
using CREL_BE.Services;
using CREL_BE.Entities;
using CREL_BE.ApiLayer.Helpers;

namespace CREL_BE.Invocables;
public class HourlyCornJob : IInvocable
{
    private readonly IUnitOfWorkService unitOfWorkService;

    public HourlyCornJob(IUnitOfWorkService unitOfWorkService)
    {
        this.unitOfWorkService = unitOfWorkService;
    }

    public async Task Invoke()
    {
        await AppointmentReminder();
        await AppointmentAutoStatus();
        
    }

    private async Task AppointmentReminder()
    {
        DevelopHelper.WriteLog(@$"Appointment Reminder start at {DateTime.Now}");

        List<AppointmentDto> appointments = await unitOfWorkService.AppointmentService.GetDtosWithoutPaging(new AppointmentFilter()
        {
            MinOnDateTime = DateTime.Now.AddMinutes(30),
            MaxOnDateTime = DateTime.Now.AddMinutes(90),
            Status = MyConstant.Appointment.Status.Approved
        });

        DevelopHelper.WriteLog(
            @$"Reminder
            DateTime.Now = {DateTime.Now}
            MinOnDateTime = {DateTime.Now.AddMinutes(30)}
            MaxOnDateTime = {DateTime.Now.AddMinutes(90)}
            Appointments = {Newtonsoft.Json.JsonConvert.SerializeObject(appointments)}
            "
        );

        foreach (AppointmentDto appointment in appointments) {
            await NotificationHelper.SendNotifications(
                new NotificationDto
                {
                    BrandId = appointment.BrandId,
                    BrokerId = appointment.BrokerId,
                    AppointmentId = appointment.Id,
                    BrandName = (appointment.Brand ?? new BrandDto()).Name,
                    SendDateTime = DateTime.Now,
                    Type = MyConstant.Notification.Type.AppointmentReminder,
                }
            );
        }

    }

    private async Task AppointmentAutoStatus()
    {
        DevelopHelper.WriteLog(@$"Appointment Auto Status start at {DateTime.Now}");

        List<Appointment> appointments = await unitOfWorkService.AppointmentService.GetAppointmentsWithoutPaging(new AppointmentFilter()
        {
            MaxOnDateTime = DateTime.Now.AddMinutes(+30),
            Status = MyConstant.Appointment.Status.Waiting
        });

        DevelopHelper.WriteLog(
            @$"Auto status
            DateTime.Now = {DateTime.Now}
            MaxOnDateTime = {DateTime.Now.AddMinutes(+30)}
            Appointments = {Newtonsoft.Json.JsonConvert.SerializeObject(appointments)}
            "
        );

        foreach (Appointment appointment in appointments) {
            appointment.Status = MyConstant.Appointment.Status.Rejected;
        }

        await unitOfWorkService.CommitAsync();
    }
}
