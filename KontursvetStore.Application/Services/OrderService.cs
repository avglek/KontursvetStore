using KontursvetStore.Core.Abstractions;
using KontursvetStore.Core.Models;

namespace KontursvetStore.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    
    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Order>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<Order> GetById(Guid id)
    {
        return await _repository.GetById(id);
    }

    public async Task<Guid> Create(Order order)
    {
        return  await _repository.Create(order); 
    }

    public async Task<int> Update(Order order)
    {
        return await _repository.Update(order);
    }
    
    public async Task<int> Delete(Guid id)
    {
        return await _repository.Delete(id);
    }
}