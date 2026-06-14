using AiLab.Data.Entities;
using AiLab.Application.Model;
using AiLab.Application.Model.Dtos;

namespace AiLab.Application.Mappings;

public static class ItemMapper
{
    public static ItemModel ToDto(this Item e) => new ItemModel
    {
        Id = e.Id,
        BrandId = e.BrandId,
        Name = e.Name,
        Description = e.Description,
        Price = e.Price,
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt
    };

    public static Item ToEntity(this CreateItemDto dto) => new Item
    {
        BrandId = dto.BrandId,
        Name = dto.Name,
        Description = dto.Description,
        Price = dto.Price
    };

    public static void Apply(this UpdateItemDto dto, Item entity)
    {
        entity.Name = dto.Name;
        entity.Description = dto.Description;
        entity.Price = dto.Price;
    }
}
