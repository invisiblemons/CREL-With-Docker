using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Entities;

[Index(nameof(BrandId))]
[Table("Store")]
public partial class Store
{
    [Column("ID", TypeName = "decimal(18, 0)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }

    [Column("BrandID", TypeName = "decimal(18, 0)")]
    public decimal BrandId { get; set; }

    [StringLength(1000)]
    public string Address { get; set; } = null!;

    [StringLength(100)]
    public string? Name { get; set; }

    public virtual Brand Brand { get; set; } = null!;
}

