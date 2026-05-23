using AiLab.Application.Model.Dtos;
using AiLab.Application.Repositories;
using AiLab.Application.Services;
using AiLab.Data.Entities;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AiLab.Tests;

public class BrandServiceTests
{
    [Fact]
    public async Task CreateAsync_CreatesBrand()
    {
        var repoMock = new Mock<IBrandRepository>();
        repoMock.Setup(r => r.CreateAsync(It.IsAny<Brand>())).ReturnsAsync((Brand b) => { b.Id = 123; return b; });

        var service = new BrandService(repoMock.Object);

        var dto = new CreateBrandDto { Name = "Test" };
        var result = await service.CreateAsync(dto);

        Assert.Equal((ulong)123, result.Id);
        Assert.Equal("Test", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
    {
        var repoMock = new Mock<IBrandRepository>();
        repoMock.Setup(r => r.GetByIdAsync(It.IsAny<ulong>())).ReturnsAsync((Brand?)null);

        var service = new BrandService(repoMock.Object);

        var result = await service.GetByIdAsync(1);
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_Throws_WhenNotFound()
    {
        var repoMock = new Mock<IBrandRepository>();
        repoMock.Setup(r => r.GetByIdAsync(It.IsAny<ulong>())).ReturnsAsync((Brand?)null);

        var service = new BrandService(repoMock.Object);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => service.UpdateAsync(1, new UpdateBrandDto { Name = "X" }));
    }
}
