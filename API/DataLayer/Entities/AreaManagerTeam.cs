using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Entities;

[Index(nameof(TeamId), nameof(AreaManagerId))]
[Table("AreaManagerTeam")]
public partial class AreaManagerTeam
{
    [Column("ID", TypeName = "decimal(18, 0)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }

    [Column("TeamID", TypeName = "decimal(18, 0)")]
    public decimal TeamId { get; set; }
    
    [Column("AreaManagerID", TypeName = "decimal(18, 0)")]
    public decimal AreaManagerId { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime? EndDate { get; set; }

    public virtual Team Team { get; set; } = null!;
    public virtual AreaManager AreaManager { get; set; } = null!;
}
