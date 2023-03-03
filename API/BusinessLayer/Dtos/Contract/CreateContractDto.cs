using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class CreateContractDto
{
    [Required]
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? OwnerId { get; set; }
    [Required]
    public decimal? PropertyId { get; set; }
    [Required]
    public decimal? BrandId { get; set; }
    public decimal? BrokerId { get; set; }
    public int? Status { get; set; }
    public string? ReasonCancel { get; set; }
    [Required]
    public string? PaymentTerm { get; set; }
    [Required]
    public decimal? Price { get; set; }

    //NEW
    public string? Lessor { get; set; }
    public DateTime? SignDate { get; set; }
    public DateTime? LessorBirthDate { get; set; }
    public string? LessorCccd { get; set; }
    public DateTime? LessorCccdGrantDate { get; set; }
    public string? LessorCccdGrantAddress { get; set; }
    public string? LessorAddress { get; set; }
    public string? LessorBankAccountNumber { get; set; }
    public string? LessorBank { get; set; }
    public string? RenterOfficeAddress { get; set; }
    public DateTime? RegistrationNumberGrantDate { get; set; }
    public string? RegistrationNumberGrantAddress { get; set; }
    public string? BrandRepresentativeName { get; set; }
    public DateTime? BrandRepresentativeBirthday { get; set; }
    public string? BrandRepresentativeAddress { get; set; }
    public string? BrandRepresentativePhoneNumber { get; set; }
    public string? BrandRepresentativeCccd { get; set; }
    public DateTime? BrandRepresentativeCccdGrantDate { get; set; }
    public string? BrandRepresentativeCccdGrantAddress { get; set; }
    public int? PayDay { get; set; }
    public double? LeaseLength { get; set; }
    public DateTime? HandoverDate { get; set; }

    public ICollection<CreateFirstLevelContractTermDto>? ContractTerms { get; set; }
}
