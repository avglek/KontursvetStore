using KontursvetStore.Core.Constants;

namespace KontursvetStore.Core.Models;

public class Product: BaseModel
{
    private Product(
        Guid id, 
        DateTime lastUpdate, 
        bool enabled, 
        string name, 
        string code,
        string description,
        string shortDescription,
        string photo,
        string[] otherPhoto,
        int price,
        int quantity
        ):base(id, enabled, lastUpdate)
    {
        Name = name;
        Code = code;
        Description = description;
        ShortDescription = shortDescription;
        Photo = photo;
        OtherPhoto = otherPhoto;
        Price = price;
        Quantity = quantity;
    }
    
    /// <summary>
    /// Наименование продуста
    /// </summary>
    public string Name { get;}
    /// <summary>
    /// Код продукта
    /// </summary>
    public string Code { get;}
    /// <summary>
    /// Описание продукта
    /// </summary>
    public string Description { get;}
    /// <summary>
    /// Короткое описание
    /// </summary>
    public string ShortDescription { get;}
    /// <summary>
    /// Основное фото 
    /// </summary>
    public string Photo { get;}
    /// <summary>
    /// Дополнительные фото
    /// </summary>
    public string[] OtherPhoto { get;}
    /// <summary>
    /// Цена 
    /// </summary>
    public int Price { get;}
    /// <summary>
    /// Колличество
    /// </summary>
    public int Quantity { get;}
    
    public static (Product Product, string Error) Create(
        Guid id, DateTime lastUpdate, bool enabled, string name, string code, string description,
        string shortDescription, string photo, string[] otherPhoto, int price, int quantity
    )
    {
        if (string.IsNullOrEmpty(name))
        {
            return (null, ErrorMessages.NAME_NULL_OR_LONG);
        }

        var product = new Product(id, lastUpdate, enabled, name, code, description, shortDescription, photo, otherPhoto,
            price, quantity );
        
        return (product, null);
    }
}