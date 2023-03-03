using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class BrandProfileDto
{
	public string? UserName { get; set; }
	public string? Name { get; set; }
	public string? Email { get; set; }
	public string? PhoneNumber { get; set; }
	public string? Description { get; set; }
	public decimal? IndustryId { get; set; }
	public string? RegistrationNumber { get; set; }
	public string? AvatarLink { get; set; }
}
