using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AiLab.Data.Entities;

[Table("sizes")]
[Index("Name", Name = "uk_sizes_name", IsUnique = true)]
public partial class Size
{
    [Key]
    [Column("id")]
    public ulong Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("created_at", TypeName = "timestamp")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime UpdatedAt { get; set; }

    [InverseProperty("Size")]
    public virtual ICollection<ItemSize> ItemSizes { get; set; } = new List<ItemSize>();

    [InverseProperty("Size")]
    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
