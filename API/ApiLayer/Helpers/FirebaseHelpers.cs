using FirebaseAdmin.Auth;

namespace CREL_BE.Helpers;
public class FirebaseHelpers
{
    public static async Task<string> ValidateTokenAsync(string token)
    {
        return (await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token)).Uid;
    }
}
