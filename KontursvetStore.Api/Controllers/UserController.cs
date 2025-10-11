using KontursvetStore.Api.Contracts;
using KontursvetStore.Core.Abstractions;
using model = KontursvetStore.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace KontursvetStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    
    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserResponse>>> GetAll()
    {
        var users = await _service.GetAll();
        
        var response = users.Select( p => new UserResponse()
        {
           Id = p.Id,
           Name = p.Name,
           Email = p.Email,
           Surname = p.Surname,
           Role = p.Role,
           Password = p.Password,
           Address = p.Address,
           Phone = p.Phone,
           Enabled = p.Enabled,
           LastUpdate = p.LastUpdated,
           Orders = p.Orders.Select(o=> new OrderResponse()
           {
               Id = o.Id,
               Amount = o.Amount,
               Address = o.Address,
               Comment = o.Comment,
               Code = o.Code,
               DateOfOrder = o.DateOfOrder,
               PaymentMethod = o.PaymentMethod,
               IsPaid = o.IsPaid,
               LastUpdate = o.LastUpdated,
               Status = o.Status,
               Enabled = o.Enabled,
               Products = o.Products.Select( p => new ProductResponse()
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
           }).ToList()
        });
    
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> GetById(Guid id)
    {
        var user = await _service.GetById(id);
        
        if (user == null)
        {
            return NotFound();
        }
        
        var response = new UserResponse()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Surname = user.Surname,
            Role = user.Role,
            Password = user.Password,
            Address = user.Address,
            Phone = user.Phone,
            Enabled = user.Enabled,
            LastUpdate = user.LastUpdated,
            Orders = user.Orders.Select(o=> new OrderResponse()
            {
                Id = o.Id,
                Amount = o.Amount,
                Address = o.Address,
                Comment = o.Comment,
                Code = o.Code,
                DateOfOrder = o.DateOfOrder,
                PaymentMethod = o.PaymentMethod,
                IsPaid = o.IsPaid,
                LastUpdate = o.LastUpdated,
                Status = o.Status,
                Enabled = o.Enabled,
                Products = o.Products.Select( p => new ProductResponse()
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
            }).ToList()
        };
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromForm] UserRequest request)
    {
        var result = model.User.Create(
            id: Guid.NewGuid(), 
            lastUpdate: DateTime.Now, 
            enabled: request.Enabled,
            name: request.Name,
            email: request.Email,
            surName: request.Surname,
            role: request.Role,
            password: request.Password,
            address: request.Address,
            phone: request.Phone,
            orders:[]
        );
    
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        var uid = await _service.Create(result.Value);
        return Ok(uid);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> Update(Guid id, [FromForm] UserRequest request)
    {
        var result = model.User.Create(
            id: id, 
            lastUpdate: DateTime.Now, 
            enabled: request.Enabled,
            name: request.Name,
            email: request.Email,
            surName: request.Surname,
            role: request.Role,
            password: request.Password,
            address: request.Address,
            phone: request.Phone
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