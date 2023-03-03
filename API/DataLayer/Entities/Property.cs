using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Entities;

[Index(nameof(BrokerId), nameof(ProjectId), nameof(LocationId))]
[Table("Property")]
public partial class Property
{
    public Property()
    {
        Media = new HashSet<Media>();
        PropertyBrands = new HashSet<PropertyBrand>();
        Appointments = new HashSet<Appointment>();
        Owners = new HashSet<Owner>();
        Contracts = new HashSet<Contract>();
        BrandRequests = new HashSet<BrandRequest>();
    }

    [Column("ID", TypeName = "decimal(18, 0)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }

    [Column("LocationID", TypeName = "decimal(18, 0)")]
    public decimal LocationId { get; set; }

    [Column("BrokerID", TypeName = "decimal(18, 0)")]
    public decimal? BrokerId { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public DateTime LastUpdateDate { get; set; }
    public int Status { get; set; }

    [StringLength(4000)]
    public string? Description { get; set; }

    [StringLength(200)]
    public string? RejectReason { get; set; }
    public int Type { get; set; }

    [StringLength(100)]
    public string? Name { get; set; } = null!;
    public decimal? ProjectId { get; set; }
    public int? Direction { get; set; }

    [StringLength(100)]
    public string? Floor { get; set; }
    public double? FloorArea { get; set; }
    public double? Area { get; set; }
    public double? Frontage { get; set; }
    public int? NumberOfFrontage { get; set; }
    public int? Certificates { get; set; }
    public double? Vertical { get; set; }
    public double? Horizontal { get; set; }
    public double? RoadWidth { get; set; }

    [StringLength(4000)]
    public string? RentalCondition { get; set; }

    [StringLength(200)]
    public string? RentalTerm { get; set; }

    [StringLength(200)]
    public string? DepositTerm { get; set; }

    [StringLength(200)]
    public string? PaymentTerm { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? Price  { get; set; }

    public virtual Broker? Broker { get; set; }
    public virtual Location Location { get; set; } = null!;
    public virtual Project? Project { get; set; }

    public virtual ICollection<Media> Media { get; set; }
    public virtual ICollection<PropertyBrand> PropertyBrands { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; }
    public virtual ICollection<Owner> Owners { get; set; }
    public virtual ICollection<Contract> Contracts { get; set; }
    public virtual ICollection<BrandRequest> BrandRequests { get; set; }
}

