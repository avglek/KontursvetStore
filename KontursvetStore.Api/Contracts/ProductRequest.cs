namespace KontursvetStore.Api.Contracts;

public class ProductRequest
{
    public string Name{get;set;}
    public Guid CategoryId{get;set;}
    public string? Code { get; set; }
    public string? Description { get; set; }
    public string? ShortDescription { get; set; }
    public string? Photo { get; set; }
    public string[]? OtherPhoto { get; set; }
    public int? Price { get; set; }
    public int? Quantity { get; set; }
    public bool Enabled { get; set; }
}