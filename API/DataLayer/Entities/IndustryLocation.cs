using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Entities;

[Index(nameof(IndustryId), nameof(LocationId))]
[Table("IndustryLocation")]
public partial class IndustryLocation
{
    [Column("IndustryID", TypeName = "decimal(18, 0)")]
    public decimal IndustryId { get; set; }

    [Column("LocationID", TypeName = "decimal(18, 0)")]
    public decimal LocationId { get; set; }
    public int Rate { get; set; }

    public virtual Industry Industry { get; set; } = null!;
    public virtual Location Location { get; set; } = null!;
}

