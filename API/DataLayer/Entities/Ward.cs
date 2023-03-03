using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Entities;

[Index(nameof(TeamId), nameof(DistrictId))]
[Table("Ward")]
public partial class Ward
{
    public Ward()
    {
        Locations = new HashSet<Location>();
    }

    [Column("ID", TypeName = "decimal(18, 0)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }

    [Column("DistrictID", TypeName = "decimal(18, 0)")]
    public decimal DistrictId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;
    public int Status { get; set; }

    [Column("TeamID", TypeName = "decimal(18, 0)")]
    public decimal? TeamId { get; set; }

    public virtual District District { get; set; } = null!;
    public virtual Team? Team { get; set; }
    public virtual ICollection<Location> Locations { get; set; }
}

