using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class ShortMediaDto
{
	public decimal? Id { get; set; }
	public string? Link { get; set; }
	public string? FileId { get; set; }
	public int? Type { get; set; }

}
