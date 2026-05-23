using AiLab.Application.Services;
using AiLab.Application.Repositories;
using AiLab.Data.Entities;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using AiLab.Controllers;
using Microsoft.Extensions.Logging;
using AiLab.Application.Services.Validation;
using AiLab.Application.Model;

namespace AiLab.Tests;

public class StockServiceTests
{
    [Fact]
    public async Task GetAllInStockAsync_ReturnsPagedResult()
    {
        var repoMock = new Mock<IStockRepository>();
        repoMock.Setup(r => r.GetAllInStockAsync()).ReturnsAsync(new List<Stock>
        {
            new Stock { Id = 1, ItemId = 1, Quantity = 5, Item = new Item { Id = 1, Name = "Item1" }, Size = new Size { Id = 1, Name = "S" } },
            new Stock { Id = 2, ItemId = 2, Quantity = 3, Item = new Item { Id = 2, Name = "Item2" }, Size = new Size { Id = 2, Name = "M" } }
        });

        var service = new StockService(repoMock.Object);

        var result = await service.GetAllInStockAsync(page: 1, pageSize: 10);

        Assert.NotNull(result);
        Assert.Equal(2, result.Items.Count);
        Assert.Equal(1, result.Meta.PageNumber);
    }

    [Fact]
    public async Task GetById_ReturnsOk_WhenFound()
    {
        var mockService = new Mock<IStockService>();
        var dto = new StockModel { Id = 1, ItemId = 1, ItemName = "Item1", SizeId = 1, SizeName = "S", Quantity = 5 };
        mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(dto);

        var logger = new Mock<ILogger<StockController>>();
        var controller = new StockController(mockService.Object, logger.Object, new StockValidation());

        var action = await controller.GetById(1);
        var ok = Assert.IsType<OkObjectResult>(action.Result);
        var value = Assert.IsType<StockModel>(ok.Value);
        Assert.Equal(1UL, value.Id);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction()
    {
        var mockService = new Mock<IStockService>();
        var input = new StockModel { ItemId = 1, SizeId = 1, Quantity = 5 };
        var created = new StockModel { Id = 10, ItemId = 1, SizeId = 1, Quantity = 5 };
        mockService.Setup(s => s.CreateAsync(input)).ReturnsAsync(created);

        var logger = new Mock<ILogger<StockController>>();
        var controller = new StockController(mockService.Object, logger.Object, new StockValidation());

        var action = await controller.Create(input);
        var createdResult = Assert.IsType<CreatedAtActionResult>(action.Result);
        var value = Assert.IsType<StockModel>(createdResult.Value);
        Assert.Equal((ulong)10, value.Id);
    }

    [Fact]
    public async Task Update_ReturnsNoContent_WhenSuccess()
    {
        var mockService = new Mock<IStockService>();
        mockService.Setup(s => s.UpdateAsync(1, It.IsAny<StockModel>())).Returns(Task.CompletedTask);

        var logger = new Mock<ILogger<StockController>>();
        var controller = new StockController(mockService.Object, logger.Object, new StockValidation());

        var result = await controller.Update(1, new StockModel { ItemId = 1, SizeId = 1, Quantity = 9 });
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenMissing()
    {
        var mockService = new Mock<IStockService>();
        mockService.Setup(s => s.UpdateAsync(1, It.IsAny<StockModel>())).ThrowsAsync(new KeyNotFoundException());

        var logger = new Mock<ILogger<StockController>>();
        var controller = new StockController(mockService.Object, logger.Object, new StockValidation());

        var result = await controller.Update(1, new StockModel { ItemId = 1, SizeId = 1, Quantity = 9 });
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenSuccess()
    {
        var mockService = new Mock<IStockService>();
        mockService.Setup(s => s.DeleteAsync(1)).Returns(Task.CompletedTask);

        var logger = new Mock<ILogger<StockController>>();
        var controller = new StockController(mockService.Object, logger.Object, new StockValidation());

        var result = await controller.Delete(1);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenMissing()
    {
        var mockService = new Mock<IStockService>();
        mockService.Setup(s => s.DeleteAsync(1)).ThrowsAsync(new KeyNotFoundException());

        var logger = new Mock<ILogger<StockController>>();
        var controller = new StockController(mockService.Object, logger.Object, new StockValidation());

        var result = await controller.Delete(1);
        Assert.IsType<NotFoundResult>(result);
    }
}
