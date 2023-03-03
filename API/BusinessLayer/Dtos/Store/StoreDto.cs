using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class StoreDto
{
	public decimal? Id { get; set; }
	public decimal? BrandId { get; set; }
	public string? Address { get; set; }
	public string? Name { get; set; }

}
