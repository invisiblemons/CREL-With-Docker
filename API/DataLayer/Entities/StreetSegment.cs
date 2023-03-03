using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Entities;

[Index(nameof(DistrictId))]
[Table("StreetSegment")]
public partial class StreetSegment
{
    public StreetSegment()
    {
        Locations = new HashSet<Location>();
    }

    [Column("ID", TypeName = "decimal(18, 0)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;
    public int Status { get; set; }

    [Column("DistrictID", TypeName = "decimal(18, 0)")]
    public decimal DistrictId { get; set; }

    public virtual District District { get; set; } = null!;

    public virtual ICollection<Location> Locations { get; set; }
}

