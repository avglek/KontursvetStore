using CSharpFunctionalExtensions;
using KontursvetStore.Core.Constants;

namespace KontursvetStore.Core.Models;

public class User: BaseModel
{
    private User(Guid id, DateTime lastUpdate, bool enabled, string name, string email, string surName, 
        UserRole role, string password, string address, string phone,List<Order> orders)
        :base(id, enabled, lastUpdate)
    {
        Email = email;
        Name = name;
        Surname = surName;
        Role = role;
        Password = password;
        Address = address;
        Phone = phone;
        Orders = orders; 
    }

    /// <summary>
    /// Почта
    /// </summary>
    public string Email { get; }
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// Фамилия
    /// </summary>
    public string Surname { get; }
    /// <summary>
    /// Роль в БД
    /// </summary>
    public UserRole Role { get; }
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; }
    /// <summary>
    /// Адрес
    /// </summary>
    public string Address { get; }
    /// <summary>
    /// Телефон для связи
    /// </summary>
    public string Phone { get; }
    
    public IList<Order> Orders { get; } = new List<Order>();

    public static Result<User> Create(Guid id, DateTime lastUpdate, bool enabled, string name, 
        string email, UserRole role, string password, string surName = null, 
        string address = null, string phone = null, List<Order> orders = null)
    {
        if (string.IsNullOrEmpty(name) || name.Length > StoreAppConstants.MAX_NAME_LENGTH)
        {
            return Result.Failure<User>(ErrorMessages.NAME_NULL_OR_LONG);
        }

        if (string.IsNullOrEmpty(email) || email.Length > StoreAppConstants.MAX_NAME_LENGTH)
        {
            return Result.Failure<User>(ErrorMessages.EMAIL_NULL_OR_LONG);
        }

        var user = new User(id, lastUpdate, enabled, name, email, surName, role, password, address, phone,orders);

        return Result.Success(user);
    }
}