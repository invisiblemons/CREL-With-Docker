using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class UpdateAreaManagerTeamDto
{
	[Required]
	public decimal? Id { get; set; }
	[Required]
	public decimal? TeamId { get; set; }
	[Required]
	public decimal? AreaManagerId { get; set; }
	[Required]
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }

}
