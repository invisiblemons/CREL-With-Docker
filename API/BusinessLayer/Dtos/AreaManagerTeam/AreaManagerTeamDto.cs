using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class AreaManagerTeamDto
{
	public decimal? Id { get; set; }
	public decimal? TeamId { get; set; }
	public decimal? AreaManagerId { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }

}
