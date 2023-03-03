using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CREL_BE.Helpers;
public class TokenHelpers
{
    public static string CreateToken(string nameId, string role, IConfiguration Config)
    {
        var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["TokenKey"]));
        var claims = new List<Claim>
            {
                //new Claim(JwtRegisteredClaimNames.Name, User.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, nameId),
                new Claim(ClaimTypes.Role, role)
            };

        var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);

        var TokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = Creds
        };

        var TokenHandler = new JwtSecurityTokenHandler();

        var Token = TokenHandler.CreateToken(TokenDescriptor);

        return TokenHandler.WriteToken(Token);
    }
}
