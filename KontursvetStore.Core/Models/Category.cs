namespace KontursvetStore.Core.Models;

public class Category : BaseModel
{
    private Category(Guid id, string name,string description,bool enabled, DateTime lastUpdated)
        :base(id,enabled,lastUpdated)
    {   
        Name = name;
        Description = description;
    }
    
    public string Name { get;}
    public string Description { get;}
    
    public static (Category Category,string Error) Create(Guid id, string name, string description, bool enabled, DateTime lastUpdated)
    {
        var error = string.Empty;

        if (string.IsNullOrEmpty(name) || name.Length > StoreAppConstants.MAX_NAME_LENGTH)
        {
            error = $"Name can not be empty or longer then {StoreAppConstants.MAX_NAME_LENGTH} symbols";
            return (null, error);
        }
        
        var category = new Category(id, name, description, enabled, lastUpdated);
        return (category, error);
    }
}