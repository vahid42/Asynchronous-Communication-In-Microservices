namespace Notification.API.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<Entities.Notification>> GetNotificationsAsync();
        Task<Entities.Notification> CreateNotificationAsync(Entities.Notification notification);
        Task<Entities.Notification> GetNotificationByIdAsync(Guid notificationId);

    }
}
