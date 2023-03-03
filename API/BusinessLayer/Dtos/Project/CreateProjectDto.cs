using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class CreateProjectDto
{
	public int? Status { get; set; }
	public string? Description { get; set; }
	[Required]
	public string? Name { get; set; }
	[Required]
	public decimal? DistrictId { get; set; }
	public DateTime? HandoverYear { get; set; }
	public string? Company { get; set; }

}
