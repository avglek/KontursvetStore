using KontursvetStore.Core.Models;

namespace KontursvetStore.Core.Abstractions;

public interface IUserService
{
    Task<IEnumerable<User>> GetAll();
    Task<User> GetById(Guid id);
    Task<Guid> Create(User User);
    Task<int> Update(User User);
    Task<int> Delete(Guid id);
}