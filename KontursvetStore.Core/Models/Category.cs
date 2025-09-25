namespace KontursvetStore.Core.Models;

public class Category
{
    private Category(Guid id, string name,string description,bool enabled)
    {   
        Id = id;
        Name = name;
        Description = description;
        Enabled = enabled;
    }
    
    public Guid Id { get;}
    public string Name { get;}
    public string Description { get;}
    public bool Enabled { get;}

    public static (Category Category,string Error) Create(Guid id, string name, string description, bool enabled)
    {
        var error = string.Empty;

        if (string.IsNullOrEmpty(name) || name.Length > StoreAppConstants.MAX_NAME_LENGTH)
        {
            error = $"Name can not be empty or longer then {StoreAppConstants.MAX_NAME_LENGTH} symbols";
            return (null, error);
        }
        
        var category = new Category(id, name, description, enabled);
        return (category, error);
    }
}