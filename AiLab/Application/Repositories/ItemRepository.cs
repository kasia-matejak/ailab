using AiLab.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AiLab.Application.Repositories;

public interface IItemRepository
{
    Task<List<Item>> GetAllAsync();
    Task<Item?> GetByIdAsync(ulong id);
    Task<Item> CreateAsync(Item item);
    Task UpdateAsync(Item item);
    Task DeleteAsync(Item item);
}

public class ItemRepository : IItemRepository
{
    private readonly global::AiLab.Data.Context.AiLab _context;

    public ItemRepository(global::AiLab.Data.Context.AiLab context)
    {
        _context = context;
    }

    public async Task<List<Item>> GetAllAsync()
    {
        return await _context.Items.ToListAsync();
    }

    public async Task<Item?> GetByIdAsync(ulong id)
    {
        return await _context.Items.FindAsync(id);
    }

    public async Task<Item> CreateAsync(Item item)
    {
        _context.Items.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task UpdateAsync(Item item)
    {
        _context.Items.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Item item)
    {
        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
    }
}
