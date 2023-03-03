using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class LocationDtoForTeamDto
{
	public decimal? Id { get; set; }

	public virtual ICollection<PropertyDto>? Properties { get; set; }
}
