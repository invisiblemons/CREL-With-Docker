using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Entities;

[Index(nameof(DistrictId))]
[Table("Project")]
public partial class Project
{
    public Project()
    {
        Media = new HashSet<Media>();
        Properties = new HashSet<Property>();
    }

    [Column("ID", TypeName = "decimal(18, 0)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }
    public int Status { get; set; }

    [StringLength(4000)]
    public string? Description { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("DistrictID", TypeName = "decimal(18, 0)")]
    public decimal DistrictId { get; set; }

    public DateTime? HandoverYear { get; set; }
	public string? Company  { get; set; }

    public virtual District District { get; set; } = null!;
    public virtual ICollection<Media> Media { get; set; }
    public virtual ICollection<Property> Properties { get; set; }
}

