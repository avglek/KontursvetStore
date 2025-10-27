using KontursvetStore.Core.Abstractions;
using KontursvetStore.Core.Constants;
using KontursvetStore.Core.Models;
using KontursvetStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace KontursvetStore.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly StoreDbContext _context;
    
    public UserRepository(StoreDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAll()
    {
        var UserEntities = await _context.Users.Include(u => u.Orders).AsNoTracking().ToListAsync();
    
        var users = UserEntities
            .Select(p => User.Create(
                    id: p.Id,
                    lastUpdate: p.Updated,
                    enabled: p.Enabled,
                    name: p.Name,
                    email: p.Email,
                    surName: p.Surname,
                    role: (UserRole)p.Role,
                    password: p.Password,
                    address: p.Address,
                    phone: p.Phone,
                    orders: p.Orders.Select(oe => Order.Create(
                            id: oe.Id,
                            userId:  oe.UserId,
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
                            products: null,
                            user: null
                            ).Value
                    ).ToList()
                ).Value).ToList();
        
        return users;
    }

    public async Task<User> GetById(Guid id)
    {
        var ue = await _context.Users
            .Include(u=>u.Orders)
            .FirstOrDefaultAsync(t => t.Id == id);

        return ue == null
            ? null
            : User.Create(
                id: ue.Id,
                lastUpdate: ue.Updated,
                enabled: ue.Enabled,
                name: ue.Name,
                email: ue.Email,
                surName: ue.Surname,
                role: (UserRole)ue.Role,
                password: ue.Password,
                address: ue.Address,
                phone: ue.Phone,
                orders: ue.Orders.Select(oe => Order.Create(
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
                        products: null,
                        user: null
                    ).Value
                ).ToList()
            ).Value;
    }

    public async Task<Guid> Create(User User)
    {
        var UserEntity = new UserEntity()
        {
            Id = User.Id,
            Name = User.Name,
            Email = User.Email,
            Surname = User.Surname,
            Role = (int)User.Role,
            Password = User.Password,
            Address = User.Address,
            Phone = User.Phone,
            Enabled = User.Enabled,
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
        };

        var response = await _context.Users.AddAsync(UserEntity);
        await _context.SaveChangesAsync();
        
        return response.Entity.Id;
    }

    public async Task<int> Update(User User)
    {
        var rows = await _context.Users
            .Where(p => p.Id == User.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Name, b => User.Name)
                .SetProperty(p => p.Email, b => User.Email)
                .SetProperty(p => p.Surname, b => User.Surname)
                .SetProperty(p => p.Password, b => User.Password)
                .SetProperty(p => p.Address, b => User.Address)
                .SetProperty(p => p.Phone, b => User.Phone)
                .SetProperty(p=> p.Role,p => (int)User.Role)
                .SetProperty(p => p.Enabled, p => User.Enabled)
                .SetProperty(p => p.Updated, p => DateTime.UtcNow)
                // .SetProperty(p => p.Orders, p => User.Orders
                //     .Select( o => new OrderEntity()
                //     {
                //         Id = o.Id,
                //         UserId = o.UserId,
                //         Amount = o.Amount,
                //         Address = o.Address,
                //         Code = o.Code,
                //         Comment = o.Comment,
                //         Updated = o.LastUpdated,
                //         Enabled = o.Enabled,
                //         DateOfOrder = o.DateOfOrder
                //     })
                //)
            );
        
        return rows;
    }

    public async Task<int> Delete(Guid id)
    {
        var rows = await _context.Users
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();
        
        return rows;
    }
}