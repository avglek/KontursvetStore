using KontursvetStore.Core.Constants;
using KontursvetStore.Core.Models;
using KontursvetStore.DataAccess.Entities;

namespace KontursvetStore.DataAccess.Repositories;

public static class Transforms
{
    // public static Product ProductFromEntity(ProductEntity pe)
    // {
    //     return Product.Create(
    //         pe.Id,
    //         pe.CategoryId,
    //         pe.Updated,
    //         pe.Enabled,
    //         pe.Name,
    //         pe.Code,
    //         pe.Description,
    //         pe.ShortDescription,
    //         pe.Photo,
    //         pe.OtherPhoto.Split(";"),
    //         pe.Price,
    //         pe.Quantity,
    //         null
    //     ).Product;
    // }
    //
    // public static Order OrderFromEntity(OrderEntity oe)
    // {
    //     return Order.Create(
    //         id: oe.Id,
    //         userId: oe.UserId,
    //         lastUpdated: oe.Updated,
    //         enabled: oe.Enabled,
    //         code: oe.Code,
    //         amount: oe.Amount,
    //         address: oe.Address,
    //         paymentMethod:(PaidSystem)oe.PaymentMethod,
    //         isPaid: oe.IsPaid,
    //         status: (OrderStatus)oe.Status,
    //         comment: oe.Comment,
    //         dateOfOrder:  oe.DateOfOrder,
    //         products: null).Order;
    // }
}