using Google.Cloud.Firestore;

namespace CREL_BE.ApiLayer.Helpers;
[FirestoreData]
public class FireStoreEmail
{
    [FirestoreProperty]
    public string Email { get; set; }
    public FireStoreEmail(string email)
    {
        this.Email = email;
    }
    public FireStoreEmail()
    {
        this.Email = "";
    }
}