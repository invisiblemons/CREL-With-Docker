using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class PropertyBrandDto
{
	public decimal? BrandId { get; set; }
	public decimal? PropertyId { get; set; }
	public int? Type { get; set; }

}
