using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class UpdateTeamDto
{
	[Required]
	public decimal? Id { get; set; }
	public string? Description { get; set; }
	[Required]
	public string? Name { get; set; }
	public int? Status { get; set; }

}
