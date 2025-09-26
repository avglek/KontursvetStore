using KontursvetStore.Api.Contracts;
using KontursvetStore.Core.Abstractions;
using KontursvetStore.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace KontursvetStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController: ControllerBase
{
    private readonly ICategoryService _service;
    
    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryResponse>>> GetAll()
    {
        var categories = await _service.GetAll();
        
        var response = categories.Select( p => new CategoryResponse()
        {
           Id = p.Id,
           Name = p.Name,
           Description = p.Description,
           Enabled = p.Enabled,
           LastUpdate = p.LastUpdated
        });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryResponse>> GetById(Guid id)
    {
        var category = await _service.GetById(id);
        
        if (category == null)
        {
            return NotFound();
        }
        
        var response = new CategoryResponse()
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Enabled = category.Enabled,
            LastUpdate = category.LastUpdated
        };
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromForm] CategoryRequest request)
    {
        var (category, error) = Category.Create(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.Enabled,
            DateTime.UtcNow
        );

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }
        var uid = await _service.Create(category);
        return Ok(uid);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> Update(Guid id, [FromForm] CategoryRequest request)
    {
        var (category, error) = Category.Create(id, request.Name, request.Description, request.Enabled, DateTime.UtcNow);

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }
        
        var rows = await _service.Update(category);
        return Ok(rows);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> Delete(Guid id)
    {
        var rows = await _service.Delete(id);
        return Ok(rows);
    }
}