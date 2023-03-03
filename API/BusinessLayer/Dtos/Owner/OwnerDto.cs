using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class OwnerDto
{
	public decimal? Id { get; set; }
	public string? Name { get; set; }
	public string? PhoneNumber { get; set; }
	public string? Email { get; set; }
	public bool? Gender { get; set; }
	public int? Status { get; set; }

}
