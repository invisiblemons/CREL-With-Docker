using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CREL_BE.Entities;

[Table("AreaManager")]
public partial class AreaManager
{
    public AreaManager()
    {
        AreaManagerTeams = new HashSet<AreaManagerTeam>();
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
    public int Status { get; set; }

    [StringLength(4000)]
    [Column("AvatarFileID", TypeName = "varchar")]
    public string? AvatarFileId { get; set; }

    [StringLength(4000)]
    [Column(TypeName = "varchar")]
    public string? AvatarLink { get; set; }

    [StringLength(15)]
    [Column(TypeName = "varchar")]
    public string PhoneNumber { get; set; } = null!;
    
    [StringLength(320)]
    [Column(TypeName = "varchar")]
    public string Email { get; set; } = null!;
    public virtual ICollection<AreaManagerTeam> AreaManagerTeams { get; set; }
}

