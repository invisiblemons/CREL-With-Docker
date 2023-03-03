using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class UpdateContractTermDto
{
	[Required]
	public decimal? Id { get; set; }
	public decimal? ContractId { get; set; }
	public decimal? ContractTermId { get; set; }
	public string? Title { get; set; }
	public string? Content { get; set; }
	[Required]
	public int? Index { get; set; }

}
