using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Entities;

[Index(nameof(WardId), nameof(StreetSegmentId))]
[Table("Location")]
public partial class Location
{
    public Location()
    {
        IndustryLocations = new HashSet<IndustryLocation>();
        Properties = new HashSet<Property>();
    }

    [Column("ID", TypeName = "decimal(18, 0)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }
    public int Status { get; set; }

    [StringLength(1000)]
    public string? PlaceId { get; set; }

    [StringLength(200)]
    public string Address { get; set; } = null!;

    [Column("WardID", TypeName = "decimal(18, 0)")]
    public decimal WardId { get; set; }

    [Column("StreetSegmentID", TypeName = "decimal(18, 0)")]
    public decimal StreetSegmentId { get; set; }

    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    //public DateTime? LastCalculateDateTime { get; set; }

    public virtual Ward Ward { get; set; } = null!;
    public virtual StreetSegment StreetSegment { get; set; } = null!;
    public virtual ICollection<Property> Properties { get; set; }
    public virtual ICollection<IndustryLocation> IndustryLocations { get; set; }
}
