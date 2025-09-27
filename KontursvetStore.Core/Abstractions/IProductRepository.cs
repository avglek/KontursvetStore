using KontursvetStore.Core.Models;

namespace KontursvetStore.Core.Abstractions;

public interface IProductRepository
{
    Task<List<Product>> GetAll();
    Task<Product> GetById(Guid id);
    Task<Guid> Create(Product product);
    Task<int> Update(Product product);
    Task<int> Delete(Guid id);
}