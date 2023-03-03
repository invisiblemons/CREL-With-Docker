using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class WardDtWardDtoForTeamDtoo
{
	public decimal? Id { get; set; }
	public decimal? DistrictId { get; set; }
	public decimal? TeamId { get; set; }
	public ICollection<LocationDtoForTeamDto>? Locations { get; set; }
}
