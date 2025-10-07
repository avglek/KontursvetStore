using System.Collections;
using CSharpFunctionalExtensions;
using KontursvetStore.Core.Constants;

namespace KontursvetStore.Core.Models;

public class Product: BaseModel
{
    private Product(
        Guid id,
        Guid categoryId,
        DateTime lastUpdate, 
        bool enabled, 
        string name, 
        string code,
        string description,
        string shortDescription,
        string photo,
        string[] otherPhoto,
        int? price,
        int? quantity,
        List<Order>? orders
        ):base(id, enabled, lastUpdate)
    {
        CategoryId = categoryId;
        Name = name;
        Code = code;
        Description = description;
        ShortDescription = shortDescription;
        Photo = photo;
        OtherPhoto = otherPhoto;
        Price = price;
        Quantity = quantity;
        Orders = orders;
    }
    
    /// <summary>
    /// Наименование продуста
    /// </summary>
    public string Name { get;}

    /// <summary>
    /// Код продукта
    /// </summary>
    public string? Code { get; } = null;
    /// <summary>
    /// Описание продукта
    /// </summary>
    public string? Description { get;} = null;
    /// <summary>
    /// Короткое описание
    /// </summary>
    public string? ShortDescription { get;} = null;
    /// <summary>
    /// Основное фото 
    /// </summary>
    public string? Photo { get;} = null;
    /// <summary>
    /// Дополнительные фото
    /// </summary>
    public string[] OtherPhoto { get;} = null;
    /// <summary>
    /// Цена 
    /// </summary>
    public int? Price { get;}  = null;
    /// <summary>
    /// Колличество
    /// </summary>
    public int? Quantity { get;}  = null;
    
    public Guid CategoryId { get;}
    
    public IList<Order> Orders { get;} = [];
    
    public static Result<Product> Create(
        Guid id, Guid categoryId, DateTime lastUpdate, bool enabled, string name, string? code, string? description,
        string? shortDescription, string? photo, string[]? otherPhoto, int? price, int? quantity,List<Order> orders
    )
    {
        if (string.IsNullOrEmpty(name))
        {
            return Result.Failure<Product>(ErrorMessages.NAME_NULL_OR_LONG);
        }
        
        var product = new Product(id, categoryId,lastUpdate, enabled, name, code, description, shortDescription, photo, otherPhoto,
            price, quantity,orders = [] );
        
        return Result.Success(product);
    }

}