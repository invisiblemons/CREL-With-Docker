using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class UpdateLocationDto
{
	[Required]
	public decimal? Id { get; set; }
	public int? Status { get; set; }
	public string? PlaceId { get; set; }
	[Required]
	public string? Address { get; set; }
	[Required]
	public decimal? WardId { get; set; }
	[Required]
	public decimal? StreetSegmentId { get; set; }
	public double? Latitude { get; set; }
	public double? Longitude { get; set; }

}
