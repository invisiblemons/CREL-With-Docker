using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Entities;

[Index(nameof(TeamId))]
[Table("Broker")]
public partial class Broker
{
    public Broker()
    {
        Appointments = new HashSet<Appointment>();
        Properties = new HashSet<Property>();
        Contracts = new HashSet<Contract>();
    }

    [Column("ID", TypeName = "decimal(18, 0)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }
    
    [StringLength(100)]
    [Column(TypeName = "varchar")]
    public string UserName { get; set; } = null!;
    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime Birthday { get; set; }
    public bool Gender { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(320)]
    [Column(TypeName = "varchar")]
    public string Email { get; set; } = null!;

    [StringLength(15)]
    [Column(TypeName = "varchar")]
    public string PhoneNumber { get; set; } = null!;
    public int Status { get; set; }

    [Column("TeamID", TypeName = "decimal(18, 0)")]
    public decimal? TeamId { get; set; }

    [StringLength(4000)]
    [Column("AvatarFileID", TypeName = "varchar")]
    public string? AvatarFileId { get; set; }

    [StringLength(4000)]
    [Column(TypeName = "varchar")]
    public string? AvatarLink { get; set; }

    public virtual Team? Team { get; set; } = null;
    public virtual ICollection<Appointment> Appointments { get; set; }
    public virtual ICollection<Property> Properties { get; set; }
    public virtual ICollection<Contract> Contracts { get; set; }
}

