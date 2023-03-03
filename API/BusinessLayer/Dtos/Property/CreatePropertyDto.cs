using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class CreatePropertyDto
{
	[Required]
	public decimal? LocationId { get; set; }
	public decimal? BrokerId { get; set; }
	public int? Status { get; set; }
	public string? Description { get; set; }
	public string? RejectReason { get; set; }
	public int? Type { get; set; }
	public string? Name { get; set; }
	public decimal? ProjectId { get; set; }
	public int? Direction { get; set; }
	public string? Floor { get; set; }
	public double? FloorArea { get; set; }
	public double? Area { get; set; }
	public double? Frontage { get; set; }
	public int? Certificates { get; set; }
	public int? NumberOfFrontage { get; set; }
	public double? Vertical { get; set; }
	public double? Horizontal { get; set; }
	public double? RoadWidth { get; set; }
	public string? RentalCondition { get; set; }
	public string? RentalTerm { get; set; }
	public string? DepositTerm { get; set; }
	public string? PaymentTerm { get; set; }
	public decimal? Price { get; set; }

}
