using KontursvetStore.Api.Contracts;
using KontursvetStore.Core.Abstractions;
using KontursvetStore.Core.Constants;
using KontursvetStore.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace KontursvetStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;
    
    public OrderController(IOrderService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderResponse>>> GetAll()
    {
        var orders = await _service.GetAll();
        
        var response = orders.Select( p => new OrderResponse()
        {
           Id = p.Id,
           Code = p.Code,
           Amount = p.Amount,
           Address = p.Address,
           PaymentMethod = p.PaymentMethod,
           IsPaid = p.IsPaid,
           Status = p.Status,
           DateOfOrder = p.DateOfOrder,
           Comment = p.Comment,
           Enabled = p.Enabled,
           LastUpdate = p.LastUpdated,
           Products = p.Products.Select( p => new ProductResponse()
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
               Category = new CategoryResponse()
               {
                   Id = p.Category.Id,
                   Name = p.Category.Name,
                   Description = p.Category.Description,
                   Enabled = p.Category.Enabled,
                   LastUpdate = p.Category.LastUpdated
               }
           }).ToList()
        });
    
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderResponse>> GetById(Guid id)
    {
        var order = await _service.GetById(id);
        
        if (order == null)
        {
            return NotFound();
        }
        
        var response = new OrderResponse()
        {
            Id = order.Id,
            Code = order.Code,
            Amount = order.Amount,
            Address = order.Address,
            PaymentMethod = order.PaymentMethod,
            IsPaid = order.IsPaid,
            Status = order.Status,
            DateOfOrder = order.DateOfOrder,
            Comment = order.Comment,
            Enabled = order.Enabled,
            LastUpdate = order.LastUpdated,
            Products = order.Products.Select( p => new ProductResponse()
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
                Category = new CategoryResponse()
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name,
                    Description = p.Category.Description,
                    Enabled = p.Category.Enabled,
                    LastUpdate = p.Category.LastUpdated
                }
            }).ToList()
        };
        
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromForm] OrderRequest request)
    {
        var result = Order.Create(
            id: Guid.NewGuid(), 
            userId:  request.UserId,
            lastUpdated: DateTime.Now, 
            enabled: request.Enabled,
            code: request.Code,
            amount: request.Amount,
            address: request.Address,
            paymentMethod: request.PaymentMethod,
            isPaid: request.IsPaid,
            status: request.Status,
            dateOfOrder: request.DateOfOrder,
            comment: request.Comment,
            products: [],
            user: null
        );
    
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        var uid = await _service.Create(result.Value);
        return Ok(uid);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<int>> Update(Guid id, [FromForm] OrderRequest request)
    {
        var result = Order.Create(
            id: id, 
            userId:  request.UserId,
            lastUpdated: DateTime.Now, 
            enabled: request.Enabled,
            code: request.Code,
            amount: request.Amount,
            address: request.Address,
            paymentMethod: request.PaymentMethod,
            isPaid: request.IsPaid,
            status: request.Status,
            dateOfOrder: request.DateOfOrder,
            comment: request.Comment,
            products: [],
            user: null
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