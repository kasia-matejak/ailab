using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AiLab.Data.Entities;

[Table("items")]
[Index("BrandId", Name = "idx_items_brand_id")]
[Index("DeletedAt", Name = "idx_items_deleted_at")]
[Index("Name", Name = "idx_items_name")]
[Index("Price", Name = "idx_items_price")]
public partial class Item
{
    [Key]
    [Column("id")]
    public ulong Id { get; set; }

    [Column("brand_id")]
    public ulong BrandId { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [Column("price")]
    [Precision(12, 2)]
    public decimal Price { get; set; }

    [Column("deleted_at", TypeName = "timestamp")]
    public DateTime? DeletedAt { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime UpdatedAt { get; set; }

    [ForeignKey("BrandId")]
    [InverseProperty("Items")]
    public virtual Brand Brand { get; set; } = null!;

    [InverseProperty("Item")]
    public virtual ICollection<ItemSize> ItemSizes { get; set; } = new List<ItemSize>();

    [InverseProperty("Item")]
    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
