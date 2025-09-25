namespace KontursvetStore.Api.Contracts;

public class CategoryRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Enabled { get; set; }
}