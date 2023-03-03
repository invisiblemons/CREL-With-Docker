using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CREL_BE.Entities;

[Table("District")]
public partial class District
{
    public District()
    {
        Projects = new HashSet<Project>();
        StreetSegments = new HashSet<StreetSegment>();
        Wards = new HashSet<Ward>();
    }

    [Column("ID", TypeName = "decimal(18, 0)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;
    public int Status { get; set; }

    public virtual ICollection<Project> Projects { get; set; }
    public virtual ICollection<StreetSegment> StreetSegments { get; set; }
    public virtual ICollection<Ward> Wards { get; set; }
}
