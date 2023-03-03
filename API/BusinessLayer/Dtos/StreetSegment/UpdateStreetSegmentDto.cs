using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class UpdateStreetSegmentDto
{
	[Required]
	public decimal? Id { get; set; }
	[Required]
	public string? Name { get; set; }
	public int? Status { get; set; }
	[Required]
	public decimal? DistrictId { get; set; }

}
