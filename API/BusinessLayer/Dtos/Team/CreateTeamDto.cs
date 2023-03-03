using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class CreateTeamDto
{
	public string? Description { get; set; }
	[Required]
	public string? Name { get; set; }
	public int? Status { get; set; }

}
