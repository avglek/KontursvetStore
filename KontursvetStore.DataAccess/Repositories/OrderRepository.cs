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
        var OrderEntities = await _context.Orders.AsNoTracking().ToListAsync();

        var orders = OrderEntities
            .Select(p => Order.Create(
                    p.Id,
                    p.Updated,
                    p.Enabled,
                    p.Code,
                    p.Amount,
                    p.Address,
                    (PaidSystem)p.PaymentMethod,
                    p.IsPaid,
                    (OrderStatus)p.Status,
                    p.DateOfOrder,
                    p.Comment
                ).Order)
            .ToList();;
        
        return orders;
    }

    public async Task<Order> GetById(Guid id)
    {
        var oe = await _context.Orders.FirstOrDefaultAsync(t => t.Id == id);

        return oe == null ? null : Order.Create(
            oe.Id,
            oe.Updated,
            oe.Enabled,
            oe.Code,
            oe.Amount,
            oe.Address,
            (PaidSystem)oe.PaymentMethod,
            oe.IsPaid,
            (OrderStatus)oe.Status,
            oe.DateOfOrder,
            oe.Comment
            ).Order;
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
}