using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AiLab.Data.Entities;

[Table("brands")]
[Index("Name", Name = "uk_brands_name", IsUnique = true)]
public partial class Brand
{
    [Key]
    [Column("id")]
    public ulong Id { get; set; }

    [Column("name")]
    [StringLength(150)]
    public string Name { get; set; } = null!;

    [Column("created_at", TypeName = "timestamp")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime UpdatedAt { get; set; }

    [InverseProperty("Brand")]
    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
