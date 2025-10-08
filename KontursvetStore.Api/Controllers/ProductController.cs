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
            LastUpdate = p.LastUpdated,
            Category = p.Category == null ? null : new CategoryResponse()
            {
                Id = p.Category.Id,
                Name = p.Category.Name,
                Description = p.Category.Description,
                Enabled = p.Category.Enabled,
                LastUpdate = p.Category.LastUpdated
            }
         });
    
         return Ok(response);
     }

    //[HttpGet("{id}")]
    // public async Task<ActionResult<ProductResponse>> GetById(Guid id)
    // {
    //     var product = await _service.GetById(id);
    //     
    //     if (product == null)
    //     {
    //         return NotFound();
    //     }
    //     
    //     var response = new ProductResponse()
    //     {
    //         Id = product.Id,
    //         Name = product.Name,
    //         Code = product.Code,
    //         Description = product.Description,
    //         ShortDescription = product.ShortDescription,
    //         Photo = product.Photo,
    //         OtherPhoto = product.OtherPhoto,
    //         Price = product.Price,
    //         Quantity = product.Quantity,
    //         Enabled = product.Enabled,
    //         LastUpdate = product.LastUpdated
    //     };
    //     
    //     return Ok(response);
    // }

    [HttpPost]
     public async Task<ActionResult<Guid>> Create([FromForm] ProductRequest request)
     {
         var photos = request.OtherPhoto.FirstOrDefault();
         var arrayPhoto = photos?.Split(",");
         
         var result = Product.Create(
             id: Guid.NewGuid(), 
             categoryId: request.CategoryId,
             lastUpdate: DateTime.Now, 
             enabled: request.Enabled,
             name: request.Name,
             code: request.Code,
             description: request.Description,
             shortDescription: request.ShortDescription,
             photo: request.Photo,
             otherPhoto: arrayPhoto,
             price: request.Price,
             quantity: request.Quantity,
             orders:  [],
             category: null
         );
    
         if (result.IsFailure)
         {
             return BadRequest(result.Error);
         }
         var uid = await _service.Create(result.Value);
         return Ok(uid);
     }

    // [HttpPut("{id}")]
    // public async Task<ActionResult<int>> Update(Guid id, [FromForm] ProductRequest request)
    // {
    //     var (product, error) = Product.Create(
    //         id,
    //         DateTime.UtcNow,
    //         request.Enabled,
    //         request.Name,
    //         request.Code,
    //         request.Description,
    //         request.ShortDescription,
    //         request.Photo,
    //         request.OtherPhoto,
    //         request.Price,
    //         request.Quantity
    //         );
    //
    //     if (!string.IsNullOrEmpty(error))
    //     {
    //         return BadRequest(error);
    //     }
    //     
    //     var rows = await _service.Update(product);
    //     return Ok(rows);
    // }

    // [HttpDelete("{id}")]
    // public async Task<ActionResult<int>> Delete(Guid id)
    // {
    //     var rows = await _service.Delete(id);
    //     return Ok(rows);
    // }
}