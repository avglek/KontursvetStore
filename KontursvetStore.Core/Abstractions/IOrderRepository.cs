using KontursvetStore.Core.Models;

namespace KontursvetStore.Core.Abstractions;

public interface IOrderRepository
{
    Task<List<Order>> GetAll();
    Task<Order> GetById(Guid id);
    Task<Guid> Create(Order Order);
    Task<int> Update(Order Order);
    Task<int> Delete(Guid id);
}