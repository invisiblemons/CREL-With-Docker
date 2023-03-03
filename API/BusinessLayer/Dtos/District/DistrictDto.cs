using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class DistrictDto
{
	public decimal? Id { get; set; }
	public string? Name { get; set; }
	public int? Status { get; set; }

}
