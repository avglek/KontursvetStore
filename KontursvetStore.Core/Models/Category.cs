using KontursvetStore.Core.Constants;

namespace KontursvetStore.Core.Models;

public class Category : BaseModel
{
    private Category(Guid id, DateTime lastUpdated, bool enabled, string name,string description)
        :base(id,enabled,lastUpdated)
    {   
        Name = name;
        Description = description;
    }
    
    /// <summary>
    /// Имя категории
    /// </summary>
    public string Name { get;}
    /// <summary>
    /// Описание категории
    /// </summary>
    public string Description { get;}

    
    public static (Category Category,string Error) Create(Guid id, DateTime lastUpdated, bool enabled, string name, string description)
    {
        var error = string.Empty;

        if (string.IsNullOrEmpty(name) || name.Length > StoreAppConstants.MAX_NAME_LENGTH)
        {
            error = ErrorMessages.NAME_NULL_OR_LONG;
            return (null, error);
        }
        
        var category = new Category(id,lastUpdated,enabled, name, description );
        return (category, error);
    }
}