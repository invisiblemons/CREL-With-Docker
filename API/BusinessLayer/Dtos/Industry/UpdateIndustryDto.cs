using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class UpdateIndustryDto
{
	[Required]
	public decimal? Id { get; set; }
	[Required]
	public string? Name { get; set; }
	public int? Status { get; set; }

}
