using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CREL_BE.Entities;

[Table("Industry")]
public partial class Industry
{
    public Industry()
    {
        Brands = new HashSet<Brand>();
        IndustryLocations = new HashSet<IndustryLocation>();
    }

    [Column("ID", TypeName = "decimal(18, 0)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;
    public int Status { get; set; }

    public virtual ICollection<Brand> Brands { get; set; }
    public virtual ICollection<IndustryLocation> IndustryLocations { get; set; }
}

