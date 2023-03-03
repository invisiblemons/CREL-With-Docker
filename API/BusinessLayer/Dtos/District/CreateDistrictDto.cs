using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class CreateDistrictDto
{
	[Required]
	public string? Name { get; set; }
	public int? Status { get; set; }

}
