using KontursvetStore.Core.Constants;

namespace KontursvetStore.Core.Models;

public class Category : BaseModel
{
    private Category(Guid id, string name,string description,bool enabled, DateTime lastUpdated)
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
    
    public static (Category Category,string Error) Create(Guid id, string name, string description, bool enabled, DateTime lastUpdated)
    {
        var error = string.Empty;

        if (string.IsNullOrEmpty(name) || name.Length > StoreAppConstants.MAX_NAME_LENGTH)
        {
            error = ErrorMessages.NAME_NULL_OR_LONG;
            return (null, error);
        }
        
        var category = new Category(id, name, description, enabled, lastUpdated);
        return (category, error);
    }
}