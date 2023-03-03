using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Entities;

[Index(nameof(ContractId), nameof(ProjectId), nameof(PropertyId))]
[Table("Media")]
public partial class Media
{
    [Column("ID", TypeName = "decimal(18, 0)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }

    [StringLength(4000)]
    [Column(TypeName = "varchar")]
    public string Link { get; set; } = null!;

    [StringLength(4000)]
    [Column("FileID", TypeName = "varchar")]
    public string FileId { get; set; } = null!;

    [Column("PropertyID", TypeName = "decimal(18, 0)")]
    public decimal? PropertyId { get; set; }

    [Column("ProjectID", TypeName = "decimal(18, 0)")]
    public decimal? ProjectId { get; set; }

    [Column("ContractID", TypeName = "decimal(18, 0)")]
    public decimal? ContractId { get; set; }

    public int? Type { get; set; }
    public virtual Contract? Contract { get; set; }
    public virtual Project? Project { get; set; }
    public virtual Property? Property { get; set; }
}

