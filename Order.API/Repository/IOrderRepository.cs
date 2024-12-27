namespace Order.API.Repository

{
    public interface IOrderRepository
    {
        Task<Entities.Order> CreateOrderAsync(Entities.Order entity);
        Task<Entities.Order> UpdateOrderAsync(Entities.Order entity);
        Task<Entities.Order?> GetOrderByIdAsync(Guid orderId);
        Task<bool> DeleteOrderByIdAsync(Entities.Order entity);
        Task<IEnumerable<Entities.Order>> GetOrdersAsync();
    }
}
