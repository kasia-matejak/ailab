using AiLab.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AiLab.Application.Repositories;

public interface IStockRepository
{
    Task<List<Stock>> GetAllInStockAsync();
    Task<Stock?> GetByIdAsync(ulong id);
    Task<Stock> CreateAsync(Stock entity);
    Task UpdateAsync(Stock entity);
    Task DeleteAsync(Stock entity);
}

public class StockRepository : IStockRepository
{
    private readonly Data.Context.AiLab _context;

    public StockRepository(Data.Context.AiLab context)
    {
        _context = context;
    }

    public async Task<List<Stock>> GetAllInStockAsync()
    {
        return await _context.Stocks
            .Include(s => s.Item)
            .Include(s => s.Size)
            .Where(s => s.Quantity > 0 && s.Item.DeletedAt == null)
            .ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(ulong id)
    {
        return await _context.Stocks
            .Include(s => s.Item)
            .Include(s => s.Size)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Stock> CreateAsync(Stock entity)
    {
        _context.Stocks.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Stock entity)
    {
        _context.Stocks.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Stock entity)
    {
        _context.Stocks.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
