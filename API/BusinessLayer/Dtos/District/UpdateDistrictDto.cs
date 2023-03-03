using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class UpdateDistrictDto
{
	[Required]
	public decimal? Id { get; set; }
	[Required]
	public string? Name { get; set; }
	public int? Status { get; set; }

}
