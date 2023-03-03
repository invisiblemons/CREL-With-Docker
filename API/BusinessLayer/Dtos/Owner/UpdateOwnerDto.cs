using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class UpdateOwnerDto
{
	[Required]
	public decimal? Id { get; set; }
	[Required]
	public string? Name { get; set; }
	[Required]
	public string? PhoneNumber { get; set; }
	public string? Email { get; set; }
	public bool? Gender { get; set; }
	public int? Status { get; set; }

}
