using System.ComponentModel.DataAnnotations;
using CREL_BE.Helpers;
using Google.Cloud.Firestore;
using Newtonsoft.Json;

namespace CREL_BE.Dtos;
[FirestoreData]
public class NotificationFireStoreDto
{
    [FirestoreProperty]
    public int? BrandId { get; set; }
    [FirestoreProperty]
    public int? PropertyId { get; set; }
    [FirestoreProperty]
    public int? AppointmentId { get; set; }
    [FirestoreProperty]
    public int? ContractId { get; set; }
    [FirestoreProperty]
    public int? BrokerId { get; set; }
    [FirestoreProperty]
    public int? TeamId { get; set; }
    [FirestoreProperty]
    public string? SendDateTime { get; set; }
    [FirestoreProperty]
    public int Type { get; set; }
    [FirestoreProperty]
    public string? Body { get; set; }
    [FirestoreProperty]
    public string? Title { get; set; }
    [FirestoreProperty]
    public int status { get; set; } = 1;

    public NotificationFireStoreDto(NotificationDto notificationDto)
    {
        BrandId = (int?)notificationDto.BrandId;
        PropertyId = (int?)notificationDto.PropertyId;
        AppointmentId = (int?)notificationDto.AppointmentId;
        ContractId = (int?)notificationDto.ContractId;
        BrokerId = (int?)notificationDto.BrokerId;
        TeamId = (int?)notificationDto.TeamId;
        SendDateTime = notificationDto.SendDateTime.ToString("yyyy/MM/dd HH:mm");
        Type = notificationDto.Type;
        Body = notificationDto.Body;
        Title = notificationDto.Title;
    }

    public NotificationFireStoreDto()
    {
    }
}
