using AiLab.Application.Mappings;
using AiLab.Application.Repositories;
using AiLab.Application.Services.Pagination;
using AiLab.Application.Model;
using AiLab.Application.Model.Dtos;

namespace AiLab.Application.Services;

public interface IBrandService
{
    Task<List<BrandModel>> GetAllAsync();
    Task<BrandModel?> GetByIdAsync(ulong id);
    Task<BrandModel> CreateAsync(CreateBrandDto dto);
    Task UpdateAsync(ulong id, UpdateBrandDto dto);
    Task DeleteAsync(ulong id);
}

public class BrandService : IBrandService
{
    private readonly IBrandRepository _repo;

    public BrandService(IBrandRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<BrandModel>> GetAllAsync()
    {
        var list = await _repo.GetAllAsync();
        var paged = Paginator.Paginate(list, b => b.ToDto(), 1, int.MaxValue);
        return paged.Items;
    }

    // New paginated method (optional overload)
    public async Task<PagedResult<BrandModel>> GetAllPaginatedAsync(int page = 1, int pageSize = 20, string? sort = null, string? filters = null)
    {
        var list = await _repo.GetAllAsync();
        // Use default sorting behavior implemented in Paginator (sort by field name)
        return Paginator.Paginate(list, b => b.ToDto(), page, pageSize, null, sort, filters);
    }

    public async Task<BrandModel?> GetByIdAsync(ulong id)
    {
        var b = await _repo.GetByIdAsync(id);
        return b?.ToDto();
    }

    public async Task<BrandModel> CreateAsync(CreateBrandDto dto)
    {
        var entity = dto.ToEntity();
        var created = await _repo.CreateAsync(entity);
        return created.ToDto();
    }

    public async Task UpdateAsync(ulong id, UpdateBrandDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null)
            throw new KeyNotFoundException("Brand not found");
        dto.Apply(entity);
        await _repo.UpdateAsync(entity);
    }

    public async Task DeleteAsync(ulong id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null)
            throw new KeyNotFoundException("Brand not found");
        await _repo.DeleteAsync(entity);
    }
}
