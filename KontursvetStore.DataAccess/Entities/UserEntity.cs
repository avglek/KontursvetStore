namespace KontursvetStore.DataAccess.Entities;

public class UserEntity:BaseEntity
{
    public string? Email { get; set; } = null;

    public string Name { get; set; } = string.Empty;

    public string? Surname { get; set; } = null;

    public int Role { get; set; } = 0;

    public string Password { get; set; } = string.Empty;

    public string? Address { get; set; } = null;

    public string? Phone { get;  set; } = null;
    
    public IList<OrderEntity> Orders { get; set; } = [];
}