using KontursvetStore.Api.Contracts;
using KontursvetStore.Core.Abstractions;
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
        });

        return Ok(response);
    }
}