namespace CREL_BE.Dtos;
public class ContractDto
{
    public decimal? Id { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? OwnerId { get; set; }
    public decimal? PropertyId { get; set; }
    public decimal? BrandId { get; set; }
    public decimal? BrokerId { get; set; }
    public int? Status { get; set; }
    public string? ReasonCancel { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? PaymentTerm { get; set; }
    public decimal? Price { get; set; }

    public ICollection<ShortMediaDto>? Media { get; set; }
    public PropertyDto? Property { get; set; }
    public BrandDto? Brand { get; set; }
    public OwnerDto? Owner { get; set; }

    //new
    public DateTime? SignDate { get; set; }
    public string? SignAddress { get; set; } = "Hồ Chí Minh";//
    public string? Lessor { get; set; } = " ";
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

    public string getAddress()
    {
        string result = "";
        if (Property != null)
        {
            if (Property.Location != null)
            {
                result += Property.Location.Address + ", ";
                if (Property.Location.StreetSegment != null)
                {
                    result += Property.Location.StreetSegment.Name + ", ";
                }
                if (Property.Location.Ward != null)
                {
                    result += Property.Location.Ward.Name + ", ";
                    if (Property.Location.Ward.District != null)
                    {
                        result += Property.Location.Ward.District.Name + ", ";
                    }
                }
            }
        }
        return result + " Thành Phố Hồ Chí Minh";
    }
}
