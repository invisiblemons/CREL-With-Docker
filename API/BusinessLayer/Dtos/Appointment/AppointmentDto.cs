using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class AppointmentDto
{
	public decimal? Id { get; set; }
	public decimal? BrandId { get; set; }
	public decimal? BrokerId { get; set; }
	public decimal? PropertyId { get; set; }
	public DateTime? OnDateTime { get; set; }
	public int? Status { get; set; }
	public string? Description { get; set; }
	public DateTime? CreateDateTime { get; set; }
	public string? RejectMessage { get; set; }
	public BrandDto? Brand { get; set; }
	public PropertyDto? Property { get; set; }
}
