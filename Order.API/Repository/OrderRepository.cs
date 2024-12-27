using Microsoft.EntityFrameworkCore;
using Order.API.Data;

namespace Order.API.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext context;

        public OrderRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<Entities.Order> CreateOrderAsync(Entities.Order entity)
        {
            context.Add(entity);
            await context.SaveChangesAsync();
            return entity;

        }

        public async Task<bool> DeleteOrderByIdAsync(Entities.Order entity)
        {
            try
            {
                context.Orders.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public async Task<Entities.Order?> GetOrderByIdAsync(Guid orderId)
        {
           return await context.Orders.Where(c => c.Id == orderId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Entities.Order>> GetOrdersAsync()
        {
            return await context.Orders.ToListAsync();
        }

        public async Task<Entities.Order> UpdateOrderAsync(Entities.Order entity)
        {
            try
            {
                context.Orders.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {

                return new Entities.Order();
            }
           
        }
    }
}
