using CREL_BE.Dtos;
using FirebaseAdmin.Messaging;
using Google.Cloud.Firestore;

namespace CREL_BE.Helpers;
public static class NotificationHelper
{
    public static FirestoreDb db;

    static NotificationHelper(){
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS",Path.Combine(Environment.CurrentDirectory, "crealestate-32189-firebase-adminsdk-4inli-b5426ea0d8.json"));
        db = FirestoreDb.Create("crel-site");
    }


    public static async Task<String> SendNotifications(NotificationDto notification)
    {
        var message = new Message()
        {
            Topic = notification.Topic,
            Data = notification.Data,
            //Notification = notification.FirebaseNotification
            Notification = new FirebaseAdmin.Messaging.Notification
            {
                Title = notification.Title,
                Body = notification.Body
            }
        };
        // write to console topic
        //Console.WriteLine("Sending message to topic: " + notification.Topic);
        await db.Collection(notification.Topic).AddAsync(new NotificationFireStoreDto(notification));
        return await FirebaseMessaging.DefaultInstance.SendAsync(message);
    }

}