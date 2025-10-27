using KontursvetStore.Core.Abstractions;
using KontursvetStore.Core.Constants;
using KontursvetStore.Core.Models;
using KontursvetStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace KontursvetStore.DataAccess.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly StoreDbContext _context;
    
    public OrderRepository(StoreDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetAll()
    {
        var OrderEntities = await _context.Orders
            .Include( o => o.User)
            .Include(o => o.Products)
            .AsNoTracking()
            .ToListAsync();
    
        var orders = OrderEntities
            .Select(oe => Order.Create(
                    id: oe.Id,
                    userId: oe.UserId,
                    lastUpdated: oe.Updated,
                    enabled: oe.Enabled,
                    code: oe.Code,
                    amount: oe.Amount,
                    address: oe.Address,
                    paymentMethod: (PaidSystem)oe.PaymentMethod,
                    isPaid: oe.IsPaid,
                    status: (OrderStatus)oe.Status,
                    dateOfOrder: oe.DateOfOrder,
                    comment: oe.Comment,
                    products: oe.Products.Select( pe => Product.Create(
                        id: pe.Id,
                        categoryId: pe.CategoryId,
                        lastUpdate: pe.Updated,
                        enabled: pe.Enabled,
                        name: pe.Name,
                        code: pe.Code,
                        description: pe.Description,
                        shortDescription: pe.ShortDescription,
                        photo: pe.Photo,
                        otherPhoto: pe.OtherPhoto.Split(";"),
                        price: pe.Price,
                        quantity: pe.Quantity,
                        orders: [],
                        category: null
                        ).Value).ToList(),
                    user: User.Create(
                        id: oe.User.Id,
                        lastUpdate: oe.User.Updated,
                        enabled: oe.User.Enabled,
                        name: oe.User.Name,
                        email: oe.User.Email,
                        surName: oe.User.Surname,
                        role: (UserRole)oe.User.Role,
                        password: oe.User.Password,
                        address: oe.User.Address,
                        phone: oe.User.Phone
                        ).Value
                    ).Value
            ).ToList();
        
        return orders;
    }
    
    public async Task<Order?> GetById(Guid id)
    {
        var oe = await _context.Orders
            .Include(o => o.Products)
            .Include(o => o.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);
    
        return oe == null ? null : Order.Create(
            id: oe.Id,
            userId: oe.UserId,
            lastUpdated: oe.Updated,
            enabled: oe.Enabled,
            code: oe.Code,
            amount: oe.Amount,
            address: oe.Address,
            paymentMethod: (PaidSystem)oe.PaymentMethod,
            isPaid: oe.IsPaid,
            status: (OrderStatus)oe.Status,
            dateOfOrder: oe.DateOfOrder,
            comment: oe.Comment,
            products: oe.Products.Select( pe => Product.Create(
                id: pe.Id,
                categoryId: pe.CategoryId,
                lastUpdate: pe.Updated,
                enabled: pe.Enabled,
                name: pe.Name,
                code: pe.Code,
                description: pe.Description,
                shortDescription: pe.ShortDescription,
                photo: pe.Photo,
                otherPhoto: pe.OtherPhoto.Split(";"),
                price: pe.Price,
                quantity: pe.Quantity,
                orders: [],
                category: null
            ).Value).ToList(),
            user: User.Create(
                id: oe.User.Id,
                lastUpdate: oe.User.Updated,
                enabled: oe.User.Enabled,
                name: oe.User.Name,
                email: oe.User.Email,
                surName: oe.User.Surname,
                role: (UserRole)oe.User.Role,
                password: oe.User.Password,
                address: oe.User.Address,
                phone: oe.User.Phone
                ).Value
            ).Value;
    }

    public async Task<Guid> Create(Order Order)
    {
        var OrderEntity = new OrderEntity()
        {
            Id = Order.Id,
            Code = Order.Code,
            Amount = Order.Amount,
            Address = Order.Address,
            Comment = Order.Comment,
            DateOfOrder = Order.DateOfOrder,
            IsPaid = Order.IsPaid,
            PaymentMethod = (int)Order.PaymentMethod,
            Status = (int)Order.Status,
            Enabled = Order.Enabled,
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Products = []
        };
    
        var response = await _context.Orders.AddAsync(OrderEntity);
        await _context.SaveChangesAsync();
        
        return response.Entity.Id;
    }

    public async Task<int> Update(Order Order)
    {
        var rows = await _context.Orders
            .Where(p => p.Id == Order.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Code, Order.Code)
                .SetProperty(p => p.Amount, Order.Amount)
                .SetProperty(p => p.IsPaid, Order.IsPaid)
                .SetProperty(p => p.Comment, Order.Comment)
                .SetProperty(p => p.DateOfOrder, Order.DateOfOrder)
                .SetProperty(p => p.IsPaid, Order.IsPaid)
                .SetProperty(p => p.Address, Order.Address)
                .SetProperty(p => p.PaymentMethod, (int)Order.PaymentMethod)
                .SetProperty(p => p.Status, (int)Order.Status)
                .SetProperty(p => p.Enabled, p => Order.Enabled)
                .SetProperty(p => p.Updated, p => DateTime.UtcNow)
            );
        
        return rows;
    }

    public async Task<int> Delete(Guid id)
    {
        var rows = await _context.Orders
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();
        
        return rows;
    }

    public async Task<Order?> AddProduct(Product Product)
    {
        return null;
    }

    public async Task<Order?> UpdateProduct(Product Product)
    {
        return null;
    }

    public async Task<Order?> DeleteProduct(Guid id)
    {
        return null;
    }
    
}