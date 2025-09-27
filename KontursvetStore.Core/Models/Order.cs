using KontursvetStore.Core.Constants;

namespace KontursvetStore.Core.Models;

public class Order : BaseModel
{
    private Order(
        Guid id,  DateTime lastUpdated, bool enabled, string code, int amount, 
        string address, PaidSystem paymentMethod, bool isPaid, OrderStatus status, DateTime dateOfOrder, string comment)
        :base(id, enabled, lastUpdated)
    {
        
        Code = code;
        Amount = amount;
        Address = address;
        PaymentMethod = paymentMethod;
        IsPaid = isPaid;
        Status = status;
        DateOfOrder = dateOfOrder;
        Comment = comment;
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
    public DateTime DateOfOrder { get;}
    /// <summary>
    /// Комментарий
    /// </summary>
    public string Comment { get;}

    public static (Order Order, string Error) Create(Guid id, DateTime lastUpdated, bool enabled, string code,
        int amount, string address, PaidSystem paymentMethod, bool isPaid, OrderStatus status, DateTime dateOfOrder, string comment)
    {
        if (string.IsNullOrEmpty(code))
        {
            var error = "Поле не долно быть пустым";
            return (null, error);
        }

        var order = new Order(id, lastUpdated, enabled, code, amount, address, paymentMethod, isPaid, status,
                dateOfOrder, comment);

            return (order, null);
    }
}