using KontursvetStore.Core.Models;

namespace KontursvetStore.Core.Abstractions;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product> GetById(Guid id);
    Task<Guid> Create(Product Product);
    Task<int> Update(Product Product);
    Task<int> Delete(Guid id);
}