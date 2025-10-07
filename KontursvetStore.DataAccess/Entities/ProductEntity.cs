namespace KontursvetStore.DataAccess.Entities;

public class ProductEntity: BaseEntity
{
    public string Name{get;set;} = string.Empty;

    public string? Code { get; set; } = null;

    public string? Description { get; set; } = null;

    public string? ShortDescription { get; set; } = null;

    public string? Photo { get; set; } = null;

    public string? OtherPhoto { get; set; }  = null;

    public int? Price { get; set; } = null;

    public int? Quantity { get; set; } = null;
    
    public Guid CategoryId { get; set; }
    public CategoryEntity Category { get; set; }
    
    public List<OrderEntity> Orders { get; set; } = [];
}