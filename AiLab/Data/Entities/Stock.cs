using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AiLab.Data.Entities;

[Table("stock")]
[Index("ItemId", Name = "idx_stock_item_id")]
[Index("SizeId", Name = "idx_stock_size_id")]
[Index("ItemId", "SizeId", Name = "uk_stock_item_size", IsUnique = true)]
public partial class Stock
{
    [Key]
    [Column("id")]
    public ulong Id { get; set; }

    [Column("item_id")]
    public ulong ItemId { get; set; }

    [Column("size_id")]
    public ulong SizeId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime UpdatedAt { get; set; }

    [ForeignKey("ItemId")]
    [InverseProperty("Stocks")]
    public virtual Item Item { get; set; } = null!;

    [ForeignKey("SizeId")]
    [InverseProperty("Stocks")]
    public virtual Size Size { get; set; } = null!;
}
