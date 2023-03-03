using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Entities;

[Index(nameof(BrandId), nameof(BrokerId), nameof(PropertyId))]
[Table("Appointment")]
public partial class Appointment
{
    [Column("ID", TypeName = "decimal(18, 0)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }

    [Column("BrandID", TypeName = "decimal(18, 0)")]
    public decimal BrandId { get; set; }

    [Column("BrokerID", TypeName = "decimal(18, 0)")]
    public decimal BrokerId { get; set; }

    [Column("PropertyID", TypeName = "decimal(18, 0)")]
    public decimal PropertyId { get; set; }

    public DateTime OnDateTime { get; set; }

    public int Status { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    public DateTime CreateDateTime { get; set; } = DateTime.Now;

    [StringLength(1000)]
    public string? RejectMessage { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Broker Broker { get; set; } = null!;

    public virtual Property Property { get; set; } = null!;
}

