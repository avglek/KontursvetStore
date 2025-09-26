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
    public string Name { get; set; }
    /// <summary>
    /// Код продукта
    /// </summary>
    public string Code { get; set; }
    /// <summary>
    /// Описание продукта
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// Короткое описание
    /// </summary>
    public string ShortDescription { get; set; }
    /// <summary>
    /// Основное фото 
    /// </summary>
    public string Photo { get; set; }
    /// <summary>
    /// Дополнительные фото
    /// </summary>
    public string[] OtherPhoto { get; set; }
    /// <summary>
    /// Цена 
    /// </summary>
    public int Price { get; set; }
    /// <summary>
    /// Колличество
    /// </summary>
    public int Quantity { get; set; }

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
            price, quantity);
        
        return (product, null);
    }
}