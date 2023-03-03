using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class CreateIndustryLocationDto
{
	[Required]
	public decimal? IndustryId { get; set; }
	[Required]
	public decimal? LocationId { get; set; }
	public int? Rate { get; set; }

}
