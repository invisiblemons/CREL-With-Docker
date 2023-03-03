using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class CreateIndustryDto
{
	[Required]
	public string? Name { get; set; }
	public int? Status { get; set; }

}
