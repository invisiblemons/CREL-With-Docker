using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class CreateFirstLevelContractTermDto
{
	public string? Title { get; set; }
	public string? Content { get; set; }
	public int? Index { get; set; }

	public ICollection<CreateSecondLevelContractTermDto>? ContractTerms { get; set; }
}
