using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class TeamDto
{
	public decimal? Id { get; set; }
	public string? Description { get; set; }
	public string? Name { get; set; }
	public int? Status { get; set; }

	public ICollection<AreaManagerTeamDto>? AreaManagerTeams { get; set; }
	public ICollection<WardDtWardDtoForTeamDtoo>? Wards { get; set; }
}
