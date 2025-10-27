namespace KontursvetStore.Api.Contracts;

public class CategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public bool Enabled { get; set; }  
    public DateTime LastUpdate { get; set; }
    
    public IList<ProductResponse>? Products { get; set; }
}