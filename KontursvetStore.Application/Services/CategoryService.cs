using KontursvetStore.Application.Helpers;
using KontursvetStore.Core.Abstractions;
using KontursvetStore.Core.Models;
using Serilog;

namespace KontursvetStore.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly ILogger _logger;
    
    public CategoryService(ICategoryRepository repository,ILogger logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<Category>> GetAll()
    {
        _logger.Information("Получение всех категорий");
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

    public async Task<int> Update(Category category)
    {
        return await _repository.Update(category);
    }
    
    public async Task<int> Delete(Guid id)
    {
        return await _repository.Delete(id);
    }
}