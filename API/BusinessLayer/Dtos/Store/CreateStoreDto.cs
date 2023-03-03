using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class CreateStoreDto
{
	[Required]
	public decimal? BrandId { get; set; }
	[Required]
	public string? Address { get; set; }
	public string? Name { get; set; }

}
