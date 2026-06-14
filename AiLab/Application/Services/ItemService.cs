using AiLab.Application.Model;
using AiLab.Application.Repositories;
using AiLab.Application.Mappings;
using AiLab.Application.Model.Dtos;

namespace AiLab.Application.Services;

public interface IItemService
{
    Task<List<ItemModel>> GetAllAsync();
    Task<ItemModel?> GetByIdAsync(ulong id);
    Task<ItemModel> CreateAsync(CreateItemDto dto);
    Task UpdateAsync(ulong id, UpdateItemDto dto);
    Task DeleteAsync(ulong id);
}

public class ItemService : IItemService
{
    private readonly IItemRepository _repo;

    public ItemService(IItemRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<ItemModel>> GetAllAsync()
    {
        var list = await _repo.GetAllAsync();
        return list.Select(i => i.ToDto()).ToList();
    }

    public async Task<ItemModel?> GetByIdAsync(ulong id)
    {
        var e = await _repo.GetByIdAsync(id);
        return e?.ToDto();
    }

    public async Task<ItemModel> CreateAsync(CreateItemDto dto)
    {
        var entity = dto.ToEntity();
        var created = await _repo.CreateAsync(entity);
        return created.ToDto();
    }

    public async Task UpdateAsync(ulong id, UpdateItemDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null) throw new KeyNotFoundException("Item not found");
        dto.Apply(entity);
        await _repo.UpdateAsync(entity);
    }

    public async Task DeleteAsync(ulong id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null) throw new KeyNotFoundException("Item not found");
        await _repo.DeleteAsync(entity);
    }
}
