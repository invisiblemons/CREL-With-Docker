using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class IndustryLocationDto
{
	public decimal? IndustryId { get; set; }
	public decimal? LocationId { get; set; }
	public int? Rate { get; set; }

}
