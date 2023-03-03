namespace CREL_BE.Dtos;
public class ContractTermDto
{
	public decimal? Id { get; set; }
	public decimal? ContractId { get; set; }
	public decimal? ContractTermId { get; set; }
	public string? Title { get; set; }
	public string? Content { get; set; }
	public int? Index { get; set; }

	public ICollection<ContractTermDto>? ContractTerms { get; set; }
}
