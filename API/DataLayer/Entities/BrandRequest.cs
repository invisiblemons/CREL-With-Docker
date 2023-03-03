using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Entities;

[Index(nameof(BrandId))]
[Table("BrandRequest")]
public partial class BrandRequest
{
    public BrandRequest(){
        Properties = new HashSet<Property>();
    }
    [Column("ID", TypeName = "decimal(18, 0)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }
    
    [Column("BrandID", TypeName = "decimal(18, 0)")]
    public decimal BrandId { get; set; }

    [StringLength(1000)]
    public string? Area { get; set; }

    public int? Amount { get; set; }
    public int? AmountFrontage { get; set; }
    
    [Column(TypeName = "decimal(18, 0)")]
	public decimal? MinPrice { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? MaxPrice { get; set; }
    
	public DateTime? MinRentalTime { get; set; }
    public DateTime? MaxRentalTime { get; set; }

    public double? MinFloorArea { get; set; }
	public double? MaxFloorArea { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }
    public int Status { get; set; }
    public virtual Brand Brand { get; set; } = null!;
    public virtual ICollection<Property> Properties { get; set; }
}

