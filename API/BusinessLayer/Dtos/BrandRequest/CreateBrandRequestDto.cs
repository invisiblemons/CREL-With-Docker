using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class CreateBrandRequestDto
{
	[Required]
	public decimal? BrandId { get; set; }
	public string? Area { get; set; }
	public int? Amount { get; set; }
	public int? AmountFrontage { get; set; }
	public decimal? MinPrice { get; set; }
	public decimal? MaxPrice { get; set; }
	public DateTime? MinRentalTime { get; set; }
	public DateTime? MaxRentalTime { get; set; }
	public double? MinFloorArea { get; set; }
	public double? MaxFloorArea { get; set; }
	public string? Description { get; set; }
	public int Status { get; set; }

}
