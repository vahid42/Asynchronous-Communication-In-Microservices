using Order.API.Entities;
using Order.API.Repository;

namespace Order.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository repository;

        public OrderService(IOrderRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Entities.Order> CreateOrderAsync(Entities.Order order)
        {
            return await repository.CreateOrderAsync(order);
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            var item = await repository.GetOrderByIdAsync(orderId);
            if (item != null)
            {
                await repository.DeleteOrderByIdAsync(item);
                return true;
            }

            return false;
        }

        public async Task<Entities.Order> GetOrderByIdAsync(Guid orderId)
        {
            var order = await repository.GetOrderByIdAsync(orderId);
            if (order != null) return order;

            return new Entities.Order();
        }

        public async Task<IEnumerable<Entities.Order>> GetOrdersAsync()
        {
            return await repository.GetOrdersAsync();
        }

        public async Task<Entities.Order> UpdateOrderAsync(Entities.Order order)
        {
            var item = await repository.GetOrderByIdAsync(order.Id);
            if (item != null)
            {
                return await repository.UpdateOrderAsync(order);

            }

            return new Entities.Order();
        }
    }
}
