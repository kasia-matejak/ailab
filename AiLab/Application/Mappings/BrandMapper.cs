using AiLab.Application.Model;
using AiLab.Application.Model.Dtos;
using AiLab.Data.Entities;

namespace AiLab.Application.Mappings;

public static class BrandMapper
{
    public static BrandModel ToDto(this Brand b) => new BrandModel
    {
        Id = b.Id,
        Name = b.Name,
        CreatedAt = b.CreatedAt,
        UpdatedAt = b.UpdatedAt
    };

    public static Brand ToEntity(this CreateBrandDto dto) => new Brand
    {
        Name = dto.Name
    };

    public static void Apply(this UpdateBrandDto dto, Brand entity)
    {
        entity.Name = dto.Name;
    }
}
