using KontursvetStore.Core.Constants;

namespace KontursvetStore.Core.Models;

public class User: BaseModel
{
    private User(Guid id, DateTime lastUpdate, bool enabled, string name, string email, string surName, 
        UserRole role, string password, string address, string phone)
        :base(id, enabled, lastUpdate)
    {
        Email = email;
        Name = name;
        Surname = surName;
        Role = role;
        Password = password;
        Address = address;
        Phone = phone;
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

    public static (User User, string Error) Create(Guid id, DateTime lastUpdate, bool enabled, string name, string email, string surName, 
        UserRole role, string password, string address, string phone)
    {
        if (string.IsNullOrEmpty(name) || name.Length > StoreAppConstants.MAX_NAME_LENGTH)
        {
            return (null, ErrorMessages.NAME_NULL_OR_LONG);
        }

        if (string.IsNullOrEmpty(email) || email.Length > StoreAppConstants.MAX_NAME_LENGTH)
        {
            return (null, ErrorMessages.EMAIL_NULL_OR_LONG);
        }

        var user = new User(id, lastUpdate, enabled, name, email, surName, role, password, address, phone);

        return (user, null);
    }
}