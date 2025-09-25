using KontursvetStore.Core.Models;

namespace KontursvetStore.Core.Abstractions;

public interface ICategoryRepository
{
    Task<List<Category>> GetAll();
    Task<Category> GetById(Guid id);
    Task<Guid> Create(Category category);
    Task<Guid> Update(Category category);
    Task<Guid> Delete(Guid id);
}