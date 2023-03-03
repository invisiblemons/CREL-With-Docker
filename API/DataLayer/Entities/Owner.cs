using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CREL_BE.Entities;

[Table("Owner")]
public partial class Owner
{
    public Owner()
    {
        Contracts = new HashSet<Contract>();
        Properties = new HashSet<Property>();
    }

    [Column("ID", TypeName = "decimal(18, 0)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(15)]
    [Column(TypeName = "varchar")]
    public string PhoneNumber { get; set; } = null!;

    [StringLength(320)]
    [Column(TypeName = "varchar")]
    public string? Email { get; set; }
    public bool? Gender { get; set; }
    public int Status { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; }

    public virtual ICollection<Property> Properties { get; set; }
}

