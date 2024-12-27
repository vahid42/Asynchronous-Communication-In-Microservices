using order = Order.API.Entities.Order;
namespace Order.API.Repository

{
    public interface IOrderRepository
    {
        Task<order> CreateOrderAsync(order entity);
        Task<order> UpdateOrderAsync(order entity);
        Task<order?> GetOrderByIdAsync(Guid orderId);
        Task<bool> DeleteOrderByIdAsync(order entity);
        Task<IEnumerable<order>> GetOrdersAsync();
    }
}
