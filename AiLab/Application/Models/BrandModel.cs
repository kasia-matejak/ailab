namespace AiLab.Application.Model;

public class BrandModel
{
    public ulong Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
