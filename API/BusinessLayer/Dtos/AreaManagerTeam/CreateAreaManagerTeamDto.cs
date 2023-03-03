using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class CreateAreaManagerTeamDto
{
	[Required]
	public decimal? TeamId { get; set; }
	[Required]
	public decimal? AreaManagerId { get; set; }
	[Required]
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }

}
