using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Entities;

[Index(nameof(BrandId), nameof(PropertyId))]
[Table("PropertyBrand")]
public partial class PropertyBrand
{
    [Column("BrandID", TypeName = "decimal(18, 0)")]
    public decimal BrandId { get; set; }

    [Column("PropertyID", TypeName = "decimal(18, 0)")]
    public decimal PropertyId { get; set; }
    public int Type { get; set; }

    public virtual Brand Brand { get; set; } = null!;
    public virtual Property Property { get; set; } = null!;
}

