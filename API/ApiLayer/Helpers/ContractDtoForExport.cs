using CREL_BE.Dtos;

namespace CREL_BE.Helpers;

public class ContractInformationForExport 
{
    public decimal? Id { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? PaymentTerm { get; set; }
    public decimal? Price { get; set; }
    public DateTime? SignDate { get; set; }
    public string? SignAddress { get; set; } = "Hồ Chí Minh";//
    public string? Lessor { get; set; } = " ";
    public string? LessorName { get; set; } // owner.name 
    public DateTime? LessorBirthDate { get; set; }
    public string? LessorCccd { get; set; }
    public DateTime? LessorCccdGrantDate { get; set; }
    public string? LessorCccdGrantAddress { get; set; }
    public string? LessorAddress { get; set; }
    public string? LessorPhoneNumber { get; set; } // owner.phonenumber
    public string? LessorBankAccountNumber { get; set; }
    public string? LessorBank { get; set; }
    public string? Address { get; set; }//property.location.address
    public string? Renter { get; set; }//brand.name
    public string? RenterOfficeAddress { get; set; }
    public string? RegistrationNumber { get; set; }//brand.RegistrationNumber
    public DateTime? RegistrationNumberGrantDate { get; set; }
    public string? RegistrationNumberGrantAddress { get; set; }
    public string? BrandRepresentativeName { get; set; }
    public DateTime? BrandRepresentativeBirthday { get; set; }
    public string? BrandRepresentativeAddress { get; set; }
    public string? BrandRepresentativePhoneNumber { get; set; }
    public string? BrandRepresentativeCccd { get; set; }
    public DateTime? BrandRepresentativeCccdGrantDate { get; set; }
    public string? BrandRepresentativeCccdGrantAddress { get; set; }
    public double? Area { get; set; } // property.area
    public string? PriceInText { get; set; }// tự điền
    public int? PayDay { get; set; }
    public double? LeaseLength { get; set; }
    public DateTime? HandoverDate { get; set; }

    public ICollection<CreateFirstLevelContractTermDto>? ContractTerms { get; set; }
}
