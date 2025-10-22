using KontursvetStore.Core.Constants;
using CSharpFunctionalExtensions;

namespace KontursvetStore.Core.Models;

public class Category : BaseModel
{
    private Category(Guid id, DateTime lastUpdated, bool enabled, string name,string description,string imageUrl, List<Product>  products)
        :base(id,enabled,lastUpdated)
    {   
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        Products = products;
    }
    
    
    /// <summary>
    /// Имя категории
    /// </summary>
    public string Name { get;}

    /// <summary>
    /// Описание категории
    /// </summary>
    public string? Description { get; } = null;
    
    public string? ImageUrl { get; } = null;
    
    public IList<Product>  Products { get; set; } = [];

    
    public static Result<Category> Create(
        Guid id, DateTime lastUpdated, bool enabled, string name, string? description = null, string? imageUrl = null,  List<Product> products = null)
    {
        var error = string.Empty;

        if (string.IsNullOrEmpty(name) || name.Length > StoreAppConstants.MAX_NAME_LENGTH)
        {
            error = ErrorMessages.NAME_NULL_OR_LONG;
            return Result.Failure<Category>(error);
        }
        
        var category = new Category(id,lastUpdated,enabled, name, description, imageUrl,products );
        
        return Result.Success(category);
    }
}