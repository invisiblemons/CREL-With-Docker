using System.Security.Claims;

namespace CREL_BE.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value!;
        }

        public static decimal GetUserId(this ClaimsPrincipal user)
        {
            return decimal.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        }

        public static string GetUserRole(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Role)?.Value!;
        }
    }
}