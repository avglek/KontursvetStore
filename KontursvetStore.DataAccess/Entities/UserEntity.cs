namespace KontursvetStore.DataAccess.Entities;

public class UserEntity:BaseEntity
{
    public string Email { get; set; }

    public string Name { get; set; }

    public string? Surname { get; set; }

    public int Role { get; set; }

    public string Password { get; set;  }

    public string? Address { get; set;  }

    public string? Phone { get;  set; }
    
    public IList<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
}