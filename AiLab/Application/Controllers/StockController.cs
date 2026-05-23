using AiLab.Application.Services;
using Microsoft.AspNetCore.Mvc;
using AiLab.Application.Model;

namespace AiLab.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly IStockService _service;
    private readonly ILogger<StockController> _logger;
    private readonly Application.Services.Validation.IStockValidation _validation;

    public StockController(IStockService service, ILogger<StockController> logger, Application.Services.Validation.IStockValidation validation)
    {
        _service = service;
        _logger = logger;
        _validation = validation;
    }

    [HttpGet("in-stock")]
    public async Task<ActionResult<PagedResult<StockModel>>> GetInStock([FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? sort = null, [FromQuery] string? filters = null)
    {
        try
        {
            var result = await _service.GetAllInStockAsync(page, pageSize, sort, filters);
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Error while getting in-stock items");
            return StatusCode(503, new { error = "Database unavailable", detail = ex.ToString() });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StockModel>> GetById(ulong id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<StockModel>> Create([FromBody] StockModel dto)
    {
        var createResult = _validation.StockValidator.Validate(dto);
        if (!createResult.IsValid)
            return BadRequest(createResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(ulong id, [FromBody] StockModel dto)
    {
        var updateResult = _validation.StockValidator.Validate(dto);
        if (!updateResult.IsValid)
            return BadRequest(updateResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
        try
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(ulong id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
