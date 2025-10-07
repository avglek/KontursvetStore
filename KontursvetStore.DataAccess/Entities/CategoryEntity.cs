namespace KontursvetStore.DataAccess.Entities;

public class CategoryEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = null;
    
    public IList<ProductEntity> Products { get; set; } = [];
}