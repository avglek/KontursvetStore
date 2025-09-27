using KontursvetStore.Core.Models;

namespace KontursvetStore.DataAccess.Entities;

public class OrderEntity: BaseEntity
{

    public string Code { get; set; }

    public int Amount { get; set; }

    public string Address { get; set; }

    public int PaymentMethod { get; set; }

    public bool IsPaid { get; set; }

    public int Status { get; set; }

    public DateTime DateOfOrder { get; set; }

    public string? Comment { get; set; }
    
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    
    public IList<ProductOrderEntity> ProductOrders { get; set; } = new List<ProductOrderEntity>();
}