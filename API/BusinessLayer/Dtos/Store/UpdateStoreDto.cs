using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class UpdateStoreDto
{
	[Required]
	public decimal? Id { get; set; }
	[Required]
	public decimal? BrandId { get; set; }
	[Required]
	public string? Address { get; set; }
	public string? Name { get; set; }

}
