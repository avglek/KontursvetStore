using KontursvetStore.Core.Models;

namespace KontursvetStore.Core.Abstractions;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetAll();
    Task<Order> GetById(Guid id);
    Task<Guid> Create(Order order);
    Task<int> Update(Order order);
    Task<int> Delete(Guid id);
}