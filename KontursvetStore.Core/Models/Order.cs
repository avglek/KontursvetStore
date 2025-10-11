using CSharpFunctionalExtensions;
using KontursvetStore.Core.Constants;

namespace KontursvetStore.Core.Models;

public class Order : BaseModel
{
    private Order(
        Guid id,Guid userId,  DateTime lastUpdated, bool enabled, string code, int amount, 
        string address, PaidSystem paymentMethod, bool isPaid, OrderStatus status, 
        DateTime? dateOfOrder, string comment, List<Product>  products, User user)
        :base(id, enabled, lastUpdated)
    {
        UserId = userId;
        Code = code;
        Amount = amount;
        Address = address;
        PaymentMethod = paymentMethod;
        IsPaid = isPaid;
        Status = status;
        DateOfOrder = dateOfOrder;
        Comment = comment;
        Products = products;
        User = user;
    }
    

    /// <summary>
    /// Код заказа
    /// </summary>
    public string Code { get;}
    /// <summary>
    /// Сумма
    /// </summary>
    public int Amount { get;}
    /// <summary>
    /// Адрес доставки
    /// </summary>
    public string Address { get;}
    /// <summary>
    /// Способ оплаты
    /// </summary>
    public PaidSystem PaymentMethod { get;}
    /// <summary>
    /// Признак оплаты
    /// </summary>
    public bool IsPaid { get;}
    /// <summary>
    /// Статус заказа
    /// </summary>
    public OrderStatus Status { get;}
    /// <summary>
    /// Дата оформления заказа
    /// </summary>
    public DateTime? DateOfOrder { get;}
    /// <summary>
    /// Комментарий
    /// </summary>
    public string Comment { get;}
    public Guid UserId { get;}
    public User User { get;}
    public IList<Product>  Products { get;} = [];

    public static Result<Order> Create(Guid id,Guid userId, DateTime lastUpdated, bool enabled, string code,
        int amount, string address, PaidSystem paymentMethod, bool isPaid, OrderStatus status, 
        DateTime? dateOfOrder, string comment, List<Product> products, User user)
    {
        if (string.IsNullOrEmpty(code))
        {
            var error = "Поле не долно быть пустым";
            return Result.Failure<Order>(error);
        }

        var order = new Order(id, userId,lastUpdated, enabled, code, amount, address, paymentMethod, isPaid, status,
                dateOfOrder, comment, products, user);

            return Result.Success(order);
    }
}