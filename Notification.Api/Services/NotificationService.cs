using Notification.API.Entities;
using Notification.API.Repository;

namespace Notification.API.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository repository;

        public NotificationService(INotificationRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Entities.Notification> CreateNotificationAsync(Entities.Notification notification)
        {
            return await repository.CreateNotificationAsync(notification);
        }

        public async Task<Entities.Notification> GetNotificationByIdAsync(Guid notificationId)
        {
            return await repository.GetNotificationByIdAsync(notificationId);
        }

        public async Task<IEnumerable<Entities.Notification>> GetNotificationsAsync()
        {
            return await repository.GetNotificationsAsync();
        }

    }
}
