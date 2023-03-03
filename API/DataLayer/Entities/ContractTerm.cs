using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Entities;

[Index(nameof(ContractTermId), nameof(ContractId))]
[Table("ContractTerm")]
public partial class ContractTerm
{
    public ContractTerm()
    {
        ContractTerms = new HashSet<ContractTerm>();
    }

    [Column("ID", TypeName = "decimal(18, 0)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }

    [Column("ContractID", TypeName = "decimal(18, 0)")]
    public decimal? ContractId { get; set; }

    [Column("ContractTermID", TypeName = "decimal(18, 0)")]
    public decimal? ContractTermId { get; set; }

    public string? Title { get; set; }
    public string? Content { get; set; }
    public int Index { get; set; }

    public virtual Contract Contract { get; set; } = null!;
    public virtual ICollection<ContractTerm> ContractTerms { get; set; }
}

