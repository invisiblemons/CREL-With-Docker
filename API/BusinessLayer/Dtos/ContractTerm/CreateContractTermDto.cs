using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class CreateContractTermDto
{
	public decimal? ContractTermId { get; set; }
	public string? Title { get; set; }
	public string? Content { get; set; }
	[Required]
	public int? Index { get; set; }

	public ICollection<CreateSecondLevelContractTermDto>? ContractTerms { get; set; }
}
