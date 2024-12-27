namespace Order.API.Services
{
    public interface IOrderService
    {
        Task<Entities.Order> UpdateOrderAsync(Entities.Order order);
        Task<IEnumerable<Entities.Order>> GetOrdersAsync();
        Task<Entities.Order> CreateOrderAsync(Entities.Order order);
        Task<Entities.Order> GetOrderByIdAsync(Guid orderId);
        Task<bool> DeleteOrderAsync(Guid orderId);

    }
}
