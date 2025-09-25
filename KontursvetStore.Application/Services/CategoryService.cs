using KontursvetStore.Core.Abstractions;
using KontursvetStore.Core.Models;

namespace KontursvetStore.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    
    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Category>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<Category> GetById(Guid id)
    {
        return await _repository.GetById(id);
    }

    public async Task<Guid> Create(Category category)
    {
        return  await _repository.Create(category); 
    }

    public async Task<Guid> Update(Category category)
    {
        return await _repository.Update(category);
    }
    
    public async Task Delete(Guid id)
    {
        await _repository.Delete(id);
    }
}