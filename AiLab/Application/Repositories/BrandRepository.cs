using AiLab.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AiLab.Application.Repositories;

public interface IBrandRepository
{
    Task<List<Brand>> GetAllAsync();
    Task<Brand?> GetByIdAsync(ulong id);
    Task<Brand> CreateAsync(Brand brand);
    Task UpdateAsync(Brand brand);
    Task DeleteAsync(Brand brand);
}

public class BrandRepository : IBrandRepository
{
    private readonly global::AiLab.Data.Context.AiLab _context;

    public BrandRepository(global::AiLab.Data.Context.AiLab context)
    {
        _context = context;
    }

    public async Task<List<Brand>> GetAllAsync()
    {
        return await _context.Brands.ToListAsync();
    }

    public async Task<Brand?> GetByIdAsync(ulong id)
    {
        return await _context.Brands.FindAsync(id);
    }

    public async Task<Brand> CreateAsync(Brand brand)
    {
        _context.Brands.Add(brand);
        await _context.SaveChangesAsync();
        return brand;
    }

    public async Task UpdateAsync(Brand brand)
    {
        _context.Brands.Update(brand);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Brand brand)
    {
        _context.Brands.Remove(brand);
        await _context.SaveChangesAsync();
    }
}
