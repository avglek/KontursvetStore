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
        var UserEntities = await _context.Users.AsNoTracking().ToListAsync();

        var users = UserEntities
            .Select(p => User.Create(
                    p.Id,
                    p.Updated,
                    p.Enabled,
                    p.Name,
                    p.Email,
                    p.Surname,
                    (UserRole)p.Role,
                    p.Password,
                    p.Address,
                    p.Phone
                ).User)
            .ToList();;
        
        return users;
    }

    public async Task<User> GetById(Guid id)
    {
        var ue = await _context.Users.FirstOrDefaultAsync(t => t.Id == id);

        return ue == null ? null : User.Create(
            ue.Id,
            ue.Updated,
            ue.Enabled,
            ue.Name,
            ue.Email,
            ue.Surname,
            (UserRole)ue.Role,
            ue.Password,
            ue.Address,
            ue.Phone
            ).User;
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