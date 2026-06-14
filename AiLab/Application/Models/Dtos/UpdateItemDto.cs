namespace AiLab.Application.Model.Dtos;

public class UpdateItemDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
}
