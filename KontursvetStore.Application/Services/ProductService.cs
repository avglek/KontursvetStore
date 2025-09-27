using KontursvetStore.Core.Abstractions;
using KontursvetStore.Core.Models;

namespace KontursvetStore.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    
    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<Product> GetById(Guid id)
    {
        return await _repository.GetById(id);
    }

    public async Task<Guid> Create(Product Product)
    {
        return  await _repository.Create(Product); 
    }

    public async Task<int> Update(Product Product)
    {
        return await _repository.Update(Product);
    }
    
    public async Task<int> Delete(Guid id)
    {
        return await _repository.Delete(id);
    } 
}