using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;

public class ChangePasswordRequest
{
    [Required]
    public string? NewPassword { get; set; }
    public string? OldPassword { get; set; }
}
