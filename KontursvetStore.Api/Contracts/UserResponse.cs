using KontursvetStore.Core.Constants;

namespace KontursvetStore.Api.Contracts;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string? Surname { get; set; }
    public UserRole Role { get; set; }
    public string Password { get; set;  }
    public string? Address { get; set;  }
    public string? Phone { get;  set; }
    public bool Enabled { get; set; }  
    public DateTime LastUpdate { get; set; }
}