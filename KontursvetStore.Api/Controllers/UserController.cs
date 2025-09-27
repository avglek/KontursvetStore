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
        var categories = await _service.GetAll();
        
        var response = categories.Select( p => new UserResponse()
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
           LastUpdate = p.LastUpdated
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
            LastUpdate = user.LastUpdated
        };
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromForm] UserRequest request)
    {
        var (user, error) = model.User.Create(
            Guid.NewGuid(),
            DateTime.UtcNow,
            request.Enabled,
            request.Name,
            request.Email,
            request.Surname,
            request.Role,
            request.Password,
            request.Address,
            request.Phone
        );

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }
        var uid = await _service.Create(user);
        return Ok(uid);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> Update(Guid id, [FromForm] UserRequest request)
    {
        var (user, error) = model.User.Create(
            id, 
            DateTime.UtcNow, 
            request.Enabled,
            request.Name,
            request.Email,
            request.Surname,
            request.Role,
            request.Password,
            request.Address,
            request.Phone
            );

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }
        
        var rows = await _service.Update(user);
        return Ok(rows);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> Delete(Guid id)
    {
        var rows = await _service.Delete(id);
        return Ok(rows);
    }
}