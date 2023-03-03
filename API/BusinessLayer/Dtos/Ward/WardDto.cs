using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class WardDto
{
	public decimal? Id { get; set; }
	public decimal? DistrictId { get; set; }
	public string? Name { get; set; }
	public int? Status { get; set; }
	public decimal? TeamId { get; set; }

	public IdNameDto? District { get; set; }
}
