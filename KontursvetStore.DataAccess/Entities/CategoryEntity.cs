namespace KontursvetStore.DataAccess.Entities;

public class CategoryEntity : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    
    public IList<ProductEntity> Products { get; set; } = new List<ProductEntity>();
}