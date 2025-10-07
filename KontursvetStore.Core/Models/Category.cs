using KontursvetStore.Core.Constants;
using CSharpFunctionalExtensions;

namespace KontursvetStore.Core.Models;

public class Category : BaseModel
{
    private Category(Guid id, DateTime lastUpdated, bool enabled, string name,string description,List<Product>  products)
        :base(id,enabled,lastUpdated)
    {   
        Name = name;
        Description = description;
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
    
    public IList<Product>  Products { get; set; } = [];

    
    public static Result<Category> Create(
        Guid id, DateTime lastUpdated, bool enabled, string name, string? description = null, List<Product> products = null)
    {
        var error = string.Empty;

        if (string.IsNullOrEmpty(name) || name.Length > StoreAppConstants.MAX_NAME_LENGTH)
        {
            error = ErrorMessages.NAME_NULL_OR_LONG;
            return Result.Failure<Category>(error);
        }
        
        var category = new Category(id,lastUpdated,enabled, name, description, products );
        
        return Result.Success(category);
    }
}