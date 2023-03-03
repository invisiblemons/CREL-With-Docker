using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class ProjectDto
{
	public decimal? Id { get; set; }
	public int? Status { get; set; }
	public string? Description { get; set; }
	public string? Name { get; set; }
	public decimal? DistrictId { get; set; }
	public DateTime? HandoverYear { get; set; }
	public string? Company { get; set; }

	public ICollection<ShortMediaDto>? Media { get; set; }
}
