using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CREL_BE.Entities;

[Table("Team")]
public partial class Team
{
    public Team()
    {
        AreaManagerTeams = new HashSet<AreaManagerTeam>();
        Brokers = new HashSet<Broker>();
        Wards = new HashSet<Ward>();
    }

    [Column("ID", TypeName = "decimal(18, 0)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;
    public int Status { get; set; }

    public virtual ICollection<AreaManagerTeam> AreaManagerTeams { get; set; }
    public virtual ICollection<Broker> Brokers { get; set; }
    public virtual ICollection<Ward> Wards { get; set; }
}

