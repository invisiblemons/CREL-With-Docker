using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Entities;

[Index(nameof(BrandId), nameof(OwnerId), nameof(PropertyId), nameof(BrokerId))]
[Table("Contract")]
public partial class Contract
{

    public Contract()
    {
        Media = new HashSet<Media>();
        ContractTerms = new HashSet<ContractTerm>();
    }

    [Column("ID", TypeName = "decimal(18, 0)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }

    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }

    [Column("OwnerID", TypeName = "decimal(18, 0)")]
    public decimal? OwnerId { get; set; }

    [Column("PropertyID", TypeName = "decimal(18, 0)")]
    public decimal PropertyId { get; set; }

    [Column("BrandID", TypeName = "decimal(18, 0)")]
    public decimal BrandId { get; set; }

    [Column("BrokerID", TypeName = "decimal(18, 0)")]
    public decimal? BrokerId { get; set; }
    public int Status { get; set; }
    public string? ReasonCancel { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;

    [StringLength(200)]
    public string? PaymentTerm { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Price { get; set; }

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

    public virtual ICollection<ContractTerm>? ContractTerms { get; set; }

    public virtual Owner? Owner { get; set; }
    public virtual Broker? Broker { get; set; }
    public virtual Brand Brand { get; set; } = null!;
    public virtual Property Property { get; set; } = null!;
    public virtual ICollection<Media> Media { get; set; }
}

