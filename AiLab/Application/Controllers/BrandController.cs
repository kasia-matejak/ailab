using AiLab.Application.Model;
using AiLab.Application.Model.Dtos;
using AiLab.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AiLab.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandController : ControllerBase
{
    private readonly IBrandService _service;
    private readonly Services.Validation.IBrandValidation _validation;

    public BrandController(IBrandService service, Services.Validation.IBrandValidation validation)
    {
        _service = service;
        _validation = validation;
    }

    [HttpGet]
    public async Task<ActionResult<List<BrandModel>>> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BrandModel>> GetById(ulong id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<BrandModel>> Create(CreateBrandDto dto)
    {
        var createResult = _validation.CreateValidator.Validate(dto);
        if (!createResult.IsValid)
            return BadRequest(createResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(ulong id, UpdateBrandDto dto)
    {
        var updateResult = _validation.UpdateValidator.Validate(dto);
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
