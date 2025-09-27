using KontursvetStore.Core.Abstractions;
using KontursvetStore.Core.Models;

namespace KontursvetStore.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    
    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<User> GetById(Guid id)
    {
        return await _repository.GetById(id);
    }

    public async Task<Guid> Create(User User)
    {
        return  await _repository.Create(User); 
    }

    public async Task<int> Update(User User)
    {
        return await _repository.Update(User);
    }
    
    public async Task<int> Delete(Guid id)
    {
        return await _repository.Delete(id);
    }
}