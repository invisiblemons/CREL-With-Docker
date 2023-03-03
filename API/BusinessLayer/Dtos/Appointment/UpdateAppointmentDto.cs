using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class UpdateAppointmentDto
{
	[Required]
	public decimal? Id { get; set; }
	[Required]
	public decimal? BrandId { get; set; }
	[Required]
	public decimal? BrokerId { get; set; }
	[Required]
	public decimal? PropertyId { get; set; }
	[Required]
	public DateTime? OnDateTime { get; set; }
	public int? Status { get; set; }
	public string? Description { get; set; }
	public string? RejectMessage { get; set; }

}
