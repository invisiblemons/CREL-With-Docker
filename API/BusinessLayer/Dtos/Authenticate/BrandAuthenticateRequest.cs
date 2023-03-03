using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class BrandAuthenticateRequest
{
    public string? Token{ get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
}
