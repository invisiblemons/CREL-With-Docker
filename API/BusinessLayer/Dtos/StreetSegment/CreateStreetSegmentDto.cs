using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class CreateStreetSegmentDto
{
	[Required]
	public string? Name { get; set; }
	public int? Status { get; set; }
	[Required]
	public decimal? DistrictId { get; set; }

}
