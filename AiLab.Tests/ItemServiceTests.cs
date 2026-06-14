using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AiLab.Application.Repositories;
using AiLab.Application.Services;
using AiLab.Data.Entities;
using Moq;
using Xunit;

namespace AiLab.Tests;

public class ItemServiceTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsList()
    {
        var repoMock = new Mock<IItemRepository>();
        repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Item>
        {
            new Item { Id = 1, BrandId = 1, Name = "Item1", Price = 10m, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Item { Id = 2, BrandId = 1, Name = "Item2", Price = 20m, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        });

        var service = new ItemService(repoMock.Object);

        var result = await service.GetAllAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Item1", result[0].Name);
    }

    [Fact]
    public async Task GetById_ReturnsModel_WhenFound()
    {
        var repoMock = new Mock<IItemRepository>();
        repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Item { Id = 1, BrandId = 1, Name = "Item1", Price = 5m, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });

        var service = new ItemService(repoMock.Object);

        var result = await service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1UL, result.Id);
        Assert.Equal("Item1", result.Name);
    }

    [Fact]
    public async Task Create_ReturnsCreatedModel()
    {
        var repoMock = new Mock<IItemRepository>();
        var input = new Item { BrandId = 1, Name = "New", Price = 9.99m };
        var created = new Item { Id = 10, BrandId = 1, Name = "New", Price = 9.99m };
        repoMock.Setup(r => r.CreateAsync(It.IsAny<Item>())).ReturnsAsync(created);

        var service = new ItemService(repoMock.Object);

        // We call service.CreateAsync with DTO, so create a DTO instance
        var dto = new AiLab.Application.Model.Dtos.CreateItemDto { BrandId = 1, Name = "New", Price = 9.99m };
        var result = await service.CreateAsync(dto);

        Assert.NotNull(result);
        Assert.Equal((ulong)10, result.Id);
        Assert.Equal("New", result.Name);
    }

    [Fact]
    public async Task Update_ThrowsKeyNotFound_WhenMissing()
    {
        var repoMock = new Mock<IItemRepository>();
        repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Item?)null);

        var service = new ItemService(repoMock.Object);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => service.UpdateAsync(1, new AiLab.Application.Model.Dtos.UpdateItemDto { Name = "X", Price = 1m }));
    }

    [Fact]
    public async Task Delete_ThrowsKeyNotFound_WhenMissing()
    {
        var repoMock = new Mock<IItemRepository>();
        repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Item?)null);

        var service = new ItemService(repoMock.Object);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => service.DeleteAsync(1));
    }
}
