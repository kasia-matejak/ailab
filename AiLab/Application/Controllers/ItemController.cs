using AiLab.Application.Model;
using AiLab.Application.Model.Dtos;
using AiLab.Application.Services;
using AiLab.Application.Services.Validation;
using Microsoft.AspNetCore.Mvc;

namespace AiLab.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemService _service;
    private readonly IItemValidation _validation;

    public ItemController(IItemService service, IItemValidation validation)
    {
        _service = service;
        _validation = validation;
    }

    [HttpGet]
    public async Task<ActionResult<List<ItemModel>>> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemModel>> GetById(ulong id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ItemModel>> Create(CreateItemDto dto)
    {
        var res = _validation.CreateValidator.Validate(dto);
        if (!res.IsValid) return BadRequest(res.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(ulong id, UpdateItemDto dto)
    {
        var res = _validation.UpdateValidator.Validate(dto);
        if (!res.IsValid) return BadRequest(res.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
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
