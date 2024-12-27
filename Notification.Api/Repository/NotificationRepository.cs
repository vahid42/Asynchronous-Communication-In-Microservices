using Microsoft.EntityFrameworkCore;
using Notification.API.Repository;
using Notification.API.Data;
using Notification.API.Entities;

namespace Notification.API.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext context;

        public NotificationRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<Entities.Notification> CreateNotificationAsync(Entities.Notification entity)
        {
            context.Add(entity);
            await context.SaveChangesAsync();
            return entity;

        }

        public async Task<Entities.Notification?> GetNotificationByIdAsync(Guid notificationId)
        {
            return await context.Notifications.Where(c => c.Id == notificationId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Entities.Notification>> GetNotificationsAsync()
        {
            return await context.Notifications.ToListAsync();
        }

    
    }
}
