using KontursvetStore.Api.Contracts;
using KontursvetStore.Core.Abstractions;
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

    // [HttpGet]
    // public async Task<ActionResult<List<OrderResponse>>> GetAll()
    // {
    //     var categories = await _service.GetAll();
    //     
    //     var response = categories.Select( p => new OrderResponse()
    //     {
    //        Id = p.Id,
    //        Code = p.Code,
    //        Amount = p.Amount,
    //        Address = p.Address,
    //        PaymentMethod = p.PaymentMethod,
    //        IsPaid = p.IsPaid,
    //        Status = p.Status,
    //        DateOfOrder = p.DateOfOrder,
    //        Comment = p.Comment,
    //        Enabled = p.Enabled,
    //        LastUpdate = p.LastUpdated
    //     });
    //
    //     return Ok(response);
    // }
    //
    // [HttpGet("{id}")]
    // public async Task<ActionResult<OrderResponse>> GetById(Guid id)
    // {
    //     var order = await _service.GetById(id);
    //     
    //     if (order == null)
    //     {
    //         return NotFound();
    //     }
    //     
    //     var response = new OrderResponse()
    //     {
    //         Id = order.Id,
    //         Code = order.Code,
    //         Amount = order.Amount,
    //         Address = order.Address,
    //         PaymentMethod = order.PaymentMethod,
    //         IsPaid = order.IsPaid,
    //         Status = order.Status,
    //         DateOfOrder = order.DateOfOrder,
    //         Comment = order.Comment,
    //         Enabled = order.Enabled,
    //         LastUpdate = order.LastUpdated
    //     };
    //     
    //     return Ok(response);
    // }
    //
    // [HttpPost]
    // public async Task<ActionResult<Guid>> Create([FromForm] OrderRequest request)
    // {
    //     var (order, error) = Order.Create(
    //         Guid.NewGuid(),
    //         request.UserId,
    //         DateTime.UtcNow,
    //         request.Enabled,
    //         request.Code,
    //         request.Amount,
    //         request.Address,
    //         request.PaymentMethod,
    //         request.IsPaid,
    //         request.Status,
    //         request.DateOfOrder,
    //         request.Comment,
    //         new List<Product>()
    //     );
    //
    //     if (!string.IsNullOrEmpty(error))
    //     {
    //         return BadRequest(error);
    //     }
    //     var uid = await _service.Create(order);
    //     return Ok(uid);
    // }
    //
    // [HttpPut("{id}")]
    // public async Task<ActionResult<int>> Update(Guid id, [FromForm] OrderRequest request)
    // {
    //     var (order, error) = Order.Create(
    //         id,
    //         request.UserId,
    //         DateTime.UtcNow, 
    //         request.Enabled,
    //         request.Code,
    //         request.Amount,
    //         request.Address,
    //         request.PaymentMethod,
    //         request.IsPaid,
    //         request.Status,
    //         request.DateOfOrder,
    //         request.Comment,
    //         null
    //         );
    //
    //     if (!string.IsNullOrEmpty(error))
    //     {
    //         return BadRequest(error);
    //     }
    //     
    //     var rows = await _service.Update(order);
    //     return Ok(rows);
    // }
    //
    // [HttpDelete("{id}")]
    // public async Task<ActionResult<int>> Delete(Guid id)
    // {
    //     var rows = await _service.Delete(id);
    //     return Ok(rows);
    // }
}