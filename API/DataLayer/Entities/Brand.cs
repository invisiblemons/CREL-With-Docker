using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Entities;

[Index(nameof(IndustryId))]
[Table("Brand")]
public partial class Brand
{
    public Brand()
    {
        Appointments = new HashSet<Appointment>();
        BrandRequests = new HashSet<BrandRequest>();
        Contracts = new HashSet<Contract>();
        PropertyBrands = new HashSet<PropertyBrand>();
        Stores = new HashSet<Store>();
    }

    [Column("ID", TypeName = "decimal(18, 0)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }

    [StringLength(100)]
    [Column(TypeName = "varchar")]
    public string? UserName { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(4000)]
    [Column("FirebaseUID" ,TypeName = "varchar")]
    public string? FirebaseUid { get; set; }

    [StringLength(320)]
    [Column(TypeName = "varchar")]
    public string? Email { get; set; }

    [StringLength(15)]
    [Column(TypeName = "varchar")]
    public string? PhoneNumber { get; set; }
    public int Status { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [StringLength(1000)]
    public string? RejectMessage { get; set; }

    [StringLength(4000)]
    [Column("AvatarFileID", TypeName = "varchar")]
    public string? AvatarFileId { get; set; }

    [StringLength(4000)]
    [Column(TypeName = "varchar")]
    public string? AvatarLink { get; set; }

    [Column("IndustryID", TypeName = "decimal(18, 0)")]
    public decimal? IndustryId { get; set; }

    [StringLength(15)]
    [Column(TypeName = "varchar")]
    public string? RegistrationNumber { get; set; }

    public virtual Industry? Industry { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; }
    public virtual ICollection<BrandRequest> BrandRequests { get; set; }
    public virtual ICollection<Contract> Contracts { get; set; }
    public virtual ICollection<PropertyBrand> PropertyBrands { get; set; }
    public virtual ICollection<Store> Stores { get; set; }
}

