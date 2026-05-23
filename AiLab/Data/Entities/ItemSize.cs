using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AiLab.Data.Entities;

[Table("item_sizes")]
[Index("ItemId", Name = "idx_item_sizes_item_id")]
[Index("SizeId", Name = "idx_item_sizes_size_id")]
[Index("ItemId", "SizeId", Name = "uk_item_size", IsUnique = true)]
public partial class ItemSize
{
    [Key]
    [Column("id")]
    public ulong Id { get; set; }

    [Column("item_id")]
    public ulong ItemId { get; set; }

    [Column("size_id")]
    public ulong SizeId { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime UpdatedAt { get; set; }

    [ForeignKey("ItemId")]
    [InverseProperty("ItemSizes")]
    public virtual Item Item { get; set; } = null!;

    [ForeignKey("SizeId")]
    [InverseProperty("ItemSizes")]
    public virtual Size Size { get; set; } = null!;
}
