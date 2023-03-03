using System.ComponentModel.DataAnnotations;
using CREL_BE.Helpers;

namespace CREL_BE.Dtos;
public class NotificationDto
{
    public decimal? Id { get; set; }
    public decimal? BrandId { get; set; }
    public decimal? PropertyId { get; set; }
    public decimal? AppointmentId { get; set; }
    public decimal? ContractId { get; set; }
    public decimal? BrokerId { get; set; }
    public decimal? TeamId { get; set; }
    public DateTime SendDateTime { get; set; } = DateTime.Now;
    public int Type { get; set; }
    //thêm status bỏ mấy cái ở sau
    public decimal? AreaManagerId { get; set; }
    public string? BrandName { get; set; }
    public string? PropertyName { get; set; }
    public string? TeamName { get; set; }
    public int? AppointmentCount { get; set; }

    public string Topic
    {
        get
        {
            //     switch (Type)
            //     {
            //         case MyConstant.Notification.Type.AppointmentReminder:
            //         case MyConstant.Notification.Type.AppointmentCreation:
            //         case MyConstant.Notification.Type.PropertyForRentApprove:
            //         case MyConstant.Notification.Type.PropertyForRentUnapprove:
            //         case MyConstant.Notification.Type.PropertyForRentAssign:
            //         case MyConstant.Notification.Type.PropertyForRentUnassign:
            //         case MyConstant.Notification.Type.ContractReminder:
            //         case MyConstant.Notification.Type.TeamAdd:
            //         case MyConstant.Notification.Type.TeamRemove:
            //         case MyConstant.Notification.Type.BrandAssign:
            //         case MyConstant.Notification.Type.BrandUnassign:
            //             return "Broker" + BrokerId;
            //         default:
            //             return "Bug";
            //     }
            return "Broker" + BrokerId;
        }
    }

    public Dictionary<string, string> Data
    {
        get
        {
            return new Dictionary<string, string>()
                    {
                        { "OnDateTime", SendDateTime.ToString() },
                        { "Type", Type.ToString()},
                        { "BrandId", BrandId.ToString() ?? "null"},
                        { "PropertyId", PropertyId.ToString() ?? "null"},
                        { "BrokerId", BrokerId.ToString() ?? "null"},
                        { "AppointmentId", AppointmentId.ToString() ?? "null"},
                        { "ContractId", ContractId.ToString() ?? "null"},
                        { "TeamId", TeamId.ToString() ?? "null"},
                        { "AppointmentCount", AppointmentCount.ToString() ?? "null"}
                    };
        }
    }

    public string Body
    {
        get
        {
            switch (Type)
            {
                case MyConstant.Notification.Type.AppointmentReminder:
                    return $"Bạn có cuộc hẹn với {BrandName} vào lúc {SendDateTime.AddHours(1).ToString("HH:mm")} ngày {SendDateTime.ToString("dd/MM/yyyy")}";
                case MyConstant.Notification.Type.AppointmentCreation:
                    return $"{BrandName} vừa tạo cuộc hẹn mới với bạn";
                case MyConstant.Notification.Type.PropertyForRentApprove:
                    return $"Bài viết {PropertyName} của bạn vừa được duyệt";
                case MyConstant.Notification.Type.PropertyForRentUnapprove:
                    return $"Bài viết {PropertyName} của bạn vừa bị từ chối";
                case MyConstant.Notification.Type.PropertyForRentAssign:
                    return $"{PropertyName} vừa được giao cho bạn";
                case MyConstant.Notification.Type.PropertyForRentUnassign:
                    return $"{PropertyName} vừa được giao cho người khác";
                case MyConstant.Notification.Type.ContractReminder:
                    return $"Hợp đồng của {PropertyName} sẽ kết thúc vào ngày {SendDateTime.AddMonths(1).ToString("dd/MM/yyyy")} ";
                case MyConstant.Notification.Type.TeamAdd:
                    return $"Bạn vừa được thêm vào team {TeamName}";
                case MyConstant.Notification.Type.TeamRemove:
                    return $"Bạn vừa bị đuổi khỏi team {TeamName}";
                case MyConstant.Notification.Type.BrandAssign:
                    return $"{BrandName} vừa được giao cho bạn";
                case MyConstant.Notification.Type.BrandUnassign:
                    return $"{BrandName} vừa được giao cho người khác";
                case MyConstant.Notification.Type.AppointmentWaiting:
                    return $"Bạn còn {AppointmentCount} cuộc hẹn chưa duyệt";
                default:
                    return "Bug";
            }
        }
    }


    public string Title
    {
        get
        {
            switch (Type)
            {
                case MyConstant.Notification.Type.AppointmentReminder:
                    return "Bạn có cuộc hẹn sắp diễn ra";
                case MyConstant.Notification.Type.AppointmentCreation:
                    return "Bạn có cuộc hẹn mới";
                case MyConstant.Notification.Type.PropertyForRentApprove:
                    return "Bài viết của bạn được duyệt";
                case MyConstant.Notification.Type.PropertyForRentUnapprove:
                    return "Bài viết của bạn bị từ chối";
                case MyConstant.Notification.Type.PropertyForRentAssign:
                    return "Một mặt bằng được giao cho bạn";
                case MyConstant.Notification.Type.PropertyForRentUnassign:
                    return "Mặt bằng đã giao cho người khác";
                case MyConstant.Notification.Type.ContractReminder:
                    return "Một hợp đồng sắp kết thúc";
                case MyConstant.Notification.Type.TeamAdd:
                    return "Bạn vừa được thêm vào team";
                case MyConstant.Notification.Type.TeamRemove:
                    return "Bạn vừa bị đuổi khỏi team";
                case MyConstant.Notification.Type.BrandAssign:
                    return "Bạn vừa có một brand mới";
                case MyConstant.Notification.Type.BrandUnassign:
                    return "Bạn vừa mất một brand";
                case MyConstant.Notification.Type.AppointmentWaiting:
                    return "Bạn còn cuộc hẹn chưa duyệt";
                default:
                    return "Bug";
            }
        }
    }

    // public FirebaseAdmin.Messaging.Notification FirebaseNotification
    // {
    //     get
    //     {
    //         return new FirebaseAdmin.Messaging.Notification
    //         {
    //             Title = Title,
    //             Body = Body
    //         };
    //     }
    // }

    // public IdNameDto? Brand { get; set; }
    // public IdNameDto? Property { get; set; }
    // public IdNameDto? Broker { get; set; }
}
