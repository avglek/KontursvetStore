using KontursvetStore.Api.Contracts;
using KontursvetStore.Core.Abstractions;
using KontursvetStore.Core.Models;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace KontursvetStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController: ControllerBase
{
    private readonly ICategoryService _service;
    private readonly ILogger _logger;
    
    public CategoryController(ICategoryService service,ILogger logger)
    {
        _service = service;
        _logger = logger;
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
           ImageUrl = p.ImageUrl,
           Enabled = p.Enabled,
           LastUpdate = p.LastUpdated,
           Products = p.Products.Select( t=> new ProductResponse()
           {
               Id = t.Id,
               Name = t.Name,
               Code = t.Code,
               Description = t.Description,
               ShortDescription = t.ShortDescription,
               Photo = t.Photo,
               OtherPhoto = t.OtherPhoto,
               Price = t.Price,
               Quantity = t.Quantity,
               Enabled = t.Enabled,
               LastUpdate = t.LastUpdated,
           }).ToList()
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
            ImageUrl = category.ImageUrl,
            Enabled = category.Enabled,
            LastUpdate = category.LastUpdated,
            Products = category.Products.Select( t=> new ProductResponse()
            {
                Id = t.Id,
                Name = t.Name,
                Code = t.Code,
                Description = t.Description,
                ShortDescription = t.ShortDescription,
                Photo = t.Photo,
                OtherPhoto = t.OtherPhoto,
                Price = t.Price,
                Quantity = t.Quantity,
                Enabled = t.Enabled,
                LastUpdate = t.LastUpdated
            }).ToList()
        };
        
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromForm] CategoryRequest request)
    {
        var result = Category.Create(
            id: Guid.NewGuid(), 
            lastUpdated: DateTime.UtcNow,
            enabled: request.Enabled,
            name: request.Name,
            description: request.Description,
            imageUrl: request.ImageUrl
        );
        
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        var uid = await _service.Create(result.Value);
        return Ok(uid);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<int>> Update(Guid id, [FromForm] CategoryRequest request)
    {
        var result = Category.Create(
            id, 
            DateTime.UtcNow, 
            request.Enabled,
            request.Name, 
            request.Description,
            request.ImageUrl
            );
    
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        var rows = await _service.Update(result.Value);
        return Ok(rows);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> Delete(Guid id)
    {
        var rows = await _service.Delete(id);
        return Ok(rows);
    }
}