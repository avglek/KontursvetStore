using KontursvetStore.Core.Models;

namespace KontursvetStore.DataAccess.Entities;

public class OrderEntity: BaseEntity
{

    public string Code { get; set; }

    public int Amount { get; set; } = 0;

    public string Address { get; set; } = string.Empty;

    public int PaymentMethod { get; set; } = 0;

    public bool IsPaid { get; set; } = false;

    public int Status { get; set; } = 0;

    public DateTime? DateOfOrder { get; set; } = null;

    public string? Comment { get; set; } = null;
    
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    
    public IList<ProductEntity> Products { get; set; } = [];
}