using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class LocationDto
{
	public decimal? Id { get; set; }
	public int? Status { get; set; }
	public string? PlaceId { get; set; }
	public string? Address { get; set; }
	public decimal? WardId { get; set; }
	public decimal? StreetSegmentId { get; set; }
	public double? Latitude { get; set; }
	public double? Longitude { get; set; }

	public IdNameDto? StreetSegment { get; set; }
    public WardDto? Ward { get; set; }
}
