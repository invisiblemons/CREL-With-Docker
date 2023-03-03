using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class MediaDto
{
	public decimal? Id { get; set; }
	public string? Link { get; set; }
	public string? FileId { get; set; }
	public decimal? PropertyId { get; set; }
	public decimal? ProjectId { get; set; }
	public decimal? ContractId { get; set; }
	public int? Type { get; set; }

}
