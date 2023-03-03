using Google.Cloud.Firestore;

namespace CREL_BE.ApiLayer.Helpers;

public static class FireStoreHelper
{
    public static FirestoreDb db;
    static FireStoreHelper()
    {
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", Path.Combine(Environment.CurrentDirectory, "crealestate-32189-firebase-adminsdk-4inli-b5426ea0d8.json"));
        db = FirestoreDb.Create("crel-site");
    }
    public static async Task AddEmailAsync(string email)
    {
        if (!await IsContainAsync(email))
        {
            await db.Collection("UpdatePassword").AddAsync(new FireStoreEmail(email));
        }
    }

    public static async Task<bool> IsContainAsync(string email)
    {
        return (await db.Collection("UpdatePassword").WhereEqualTo("Email", email).GetSnapshotAsync()).Any();
    }

    public static async Task RemoveEmailAsync(string email)
    {
        if (await IsContainAsync(email))
        {
            await (await db.Collection("UpdatePassword").WhereEqualTo("Email", email).GetSnapshotAsync()).First().Reference.DeleteAsync();
        }
    }

}
