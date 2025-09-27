using KontursvetStore.Api.Contracts;
using KontursvetStore.Core.Abstractions;
using KontursvetStore.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace KontursvetStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController: ControllerBase
{
        private readonly IProductService _service;
    
    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductResponse>>> GetAll()
    {
        var categories = await _service.GetAll();
        
        var response = categories.Select( p => new ProductResponse()
        {
           Id = p.Id,
           Name = p.Name,
           Code = p.Code,
           Description = p.Description,
           ShortDescription = p.ShortDescription,
           Photo = p.Photo,
           OtherPhoto = p.OtherPhoto,
           Price = p.Price,
           Quantity = p.Quantity,
           Enabled = p.Enabled,
           LastUpdate = p.LastUpdated
        });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponse>> GetById(Guid id)
    {
        var product = await _service.GetById(id);
        
        if (product == null)
        {
            return NotFound();
        }
        
        var response = new ProductResponse()
        {
            Id = product.Id,
            Name = product.Name,
            Code = product.Code,
            Description = product.Description,
            ShortDescription = product.ShortDescription,
            Photo = product.Photo,
            OtherPhoto = product.OtherPhoto,
            Price = product.Price,
            Quantity = product.Quantity,
            Enabled = product.Enabled,
            LastUpdate = product.LastUpdated
        };
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromForm] ProductRequest request)
    {
        var (product, error) = Product.Create(
            Guid.NewGuid(),
            DateTime.UtcNow,
            request.Enabled,
            request.Name,
            request.Code,
            request.Description,
            request.ShortDescription,
            request.Photo,
            request.OtherPhoto,
            request.Price,
            request.Quantity
        );

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }
        var uid = await _service.Create(product);
        return Ok(uid);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> Update(Guid id, [FromForm] ProductRequest request)
    {
        var (product, error) = Product.Create(
            id,
            DateTime.UtcNow,
            request.Enabled,
            request.Name,
            request.Code,
            request.Description,
            request.ShortDescription,
            request.Photo,
            request.OtherPhoto,
            request.Price,
            request.Quantity
            );

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }
        
        var rows = await _service.Update(product);
        return Ok(rows);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> Delete(Guid id)
    {
        var rows = await _service.Delete(id);
        return Ok(rows);
    }
}