using KontursvetStore.Core.Models;

namespace KontursvetStore.Core.Abstractions;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAll();
    Task<Category> GetById(Guid id);
    Task<Guid> Create(Category category);
    Task<int> Update(Category category);
    Task<int> Delete(Guid id);
}