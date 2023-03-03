using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class AuthenticateRequest
{
    [Required]
    public string? UserName { get; set; }
    [Required]
    public string? Password { get; set; }
}
