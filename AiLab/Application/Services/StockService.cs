using AiLab.Application.Model;
using AiLab.Application.Repositories;
using AiLab.Application.Services.Pagination;

namespace AiLab.Application.Services;

public interface IStockService
{
    Task<PagedResult<StockModel>> GetAllInStockAsync(int page = 1, int pageSize = 20, string? sort = null, string? filters = null);
    Task<StockModel?> GetByIdAsync(ulong id);
    Task<StockModel> CreateAsync(StockModel dto);
    Task UpdateAsync(ulong id, StockModel dto);
    Task DeleteAsync(ulong id);
}

public class StockService : IStockService
{
    private readonly IStockRepository _repository;

    public StockService(IStockRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<StockModel>> GetAllInStockAsync(int page = 1, int pageSize = 20, string? sort = null, string? filters = null)
    {
        var all = await _repository.GetAllInStockAsync();
        return Paginator.Paginate(all,
            s => new StockModel
            {
                Id = s.Id,
                ItemId = s.ItemId,
                ItemName = s.Item?.Name ?? string.Empty,
                SizeId = s.SizeId,
                SizeName = s.Size?.Name ?? string.Empty,
                Quantity = s.Quantity
            },
            page, pageSize, null, sort, filters);
    }

    public async Task<StockModel?> GetByIdAsync(ulong id)
    {
        var s = await _repository.GetByIdAsync(id);
        if (s is null) return null;
        return new StockModel
        {
            Id = s.Id,
            ItemId = s.ItemId,
            ItemName = s.Item?.Name ?? string.Empty,
            SizeId = s.SizeId,
            SizeName = s.Size?.Name ?? string.Empty,
            Quantity = s.Quantity
        };
    }

    public async Task<StockModel> CreateAsync(StockModel dto)
    {
        var entity = new Data.Entities.Stock
        {
            ItemId = dto.ItemId,
            SizeId = dto.SizeId,
            Quantity = dto.Quantity
        };
        var created = await _repository.CreateAsync(entity);
        return new StockModel
        {
            Id = created.Id,
            ItemId = created.ItemId,
            ItemName = created.Item?.Name ?? string.Empty,
            SizeId = created.SizeId,
            SizeName = created.Size?.Name ?? string.Empty,
            Quantity = created.Quantity
        };
    }

    public async Task UpdateAsync(ulong id, StockModel dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null) throw new KeyNotFoundException("Stock not found");
        entity.ItemId = dto.ItemId;
        entity.SizeId = dto.SizeId;
        entity.Quantity = dto.Quantity;
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(ulong id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null) throw new KeyNotFoundException("Stock not found");
        await _repository.DeleteAsync(entity);
    }
}