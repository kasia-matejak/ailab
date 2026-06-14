namespace AiLab.Application.Model;

public class ItemModel
{
    public ulong Id { get; set; }
    public ulong BrandId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
