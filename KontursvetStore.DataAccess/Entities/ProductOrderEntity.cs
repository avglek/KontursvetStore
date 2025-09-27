namespace KontursvetStore.DataAccess.Entities;

public class ProductOrderEntity
{
    public Guid ProductId { get; set; }
    public ProductEntity Product { get; set; }
    public Guid OrderId { get; set; }
    public OrderEntity Order { get; set; }
}