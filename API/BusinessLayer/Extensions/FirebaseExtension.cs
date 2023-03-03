using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace CREL_BE.Extensions
{
    public static class FirebaseExtension
    {
        public static void Init(){
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Path.Combine(Environment.CurrentDirectory, "crealestate-32189-firebase-adminsdk-4inli-b5426ea0d8.json")),
            });
        }
    }
}