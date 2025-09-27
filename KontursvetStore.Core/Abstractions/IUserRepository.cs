using KontursvetStore.Core.Models;

namespace KontursvetStore.Core.Abstractions;

public interface IUserRepository
{
    Task<List<User>> GetAll();
    Task<User> GetById(Guid id);
    Task<Guid> Create(User User);
    Task<int> Update(User User);
    Task<int> Delete(Guid id);
}