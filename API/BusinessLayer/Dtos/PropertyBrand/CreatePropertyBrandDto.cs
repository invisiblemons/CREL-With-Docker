using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class CreatePropertyBrandDto
{
	[Required]
	public decimal? BrandId { get; set; }
	[Required]
	public decimal? PropertyId { get; set; }
	public int? Type { get; set; }

}
