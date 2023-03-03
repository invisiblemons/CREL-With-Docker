using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class StreetSegmentDto
{
	public decimal? Id { get; set; }
	public string? Name { get; set; }
	public int? Status { get; set; }
	public decimal? DistrictId { get; set; }

}
